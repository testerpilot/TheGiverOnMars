﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.PlacedObject;
using TheGiverOnMars.Dictionaries;
using TheGiverOnMars.Objects;

using Items = TheGiverOnMars.Components.Item.Definitions;

namespace TheGiverOnMars.Managers
{
    /// <summary>
    /// Class for managing the player's GUI interaction with their inventory
    /// </summary>
    public class InventoryManager : Component
    {
        public Inventory Inventory;

        public SpriteTile[] TilesOnMainGui;
        public SpriteTile[] ItemTilesOnMainGui;

        public Color ColorTilesOnMainGui = Color.LightGray * 0.7f;
        public Color ColorSelectedTile = Color.DimGray * 0.7f;
        public Color ColorSelectedItem = Color.White * 0.5f;
        public Color ColorSelectedItemText = Color.Black * 0.5f;

        public Tile PlayerTile;
        public float TempSpeed;

        public int SelectedTile = 0;
        public int? SelectedTileForMove = null;

        public bool IsSelectedTileInOtherInv = false;
        public bool? IsSelectedTileForMoveInOtherInv = null;

        public bool IsOnPlaceableItem = false;
        public bool PlaceObjectBlocked = false;
        public PlacedObjectInstance TemporaryPlacedObjectInstance = null;


        public List<Keys> AssociatedKeys = new List<Keys>()
        {
            Keys.D1,
            Keys.D2,
            Keys.D3,
            Keys.D4,
            Keys.D5,
            Keys.D6,
            Keys.D7,
            Keys.D8,
            Keys.D9,
            Keys.D0
        };

        public bool IsInventoryOpen = false;
        public bool BlockOpen = false;

        public Inventory InventoryInteractingWith = null;

        // Used for positioning
        public const int GuiOffset = (10 * 68) / 2;
        public float PlayerOffsetX, PlayerOffsetY;

        public CraftingManager CraftingManager;

        public InventoryManager(Inventory inventory, Tile playerTile)
        {
            Inventory = inventory;
            PlayerTile = playerTile;

            CraftingManager = new CraftingManager(playerTile, inventory);

            SetupInventoryTiles(TileManager.GetTileFromID(20));
        }

        public void SetupInventoryTiles(SpriteTile baseTile)
        {
            TilesOnMainGui = new SpriteTile[30];
            ItemTilesOnMainGui = new SpriteTile[30];

            for (int i = 0; i < 30; i++)
            {
                var tempBaseTile = baseTile.DeepCopy();
                tempBaseTile.Position = new Vector2();
                TilesOnMainGui[i] = tempBaseTile;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsInventoryOpen || CraftingManager.Toggled)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var tilePos = new Vector2(PlayerOffsetX + (i * 68) - GuiOffset, PlayerOffsetY - (68 * j));

                        if (i + (10 * j) == SelectedTile && !IsSelectedTileInOtherInv && !CraftingManager.Toggled)
                        {
                            TilesOnMainGui[i + (10 * j)].Draw(spriteBatch, tilePos, ColorSelectedTile);
                        }
                        else
                        {
                            TilesOnMainGui[i + (10 * j)].Draw(spriteBatch, tilePos, ColorTilesOnMainGui);
                        }

                        if (Inventory.Spaces[i + (10 * j)].HasValue)
                        {
                            var currentSpace = Inventory.Spaces[i + (10 * j)];

                            if (currentSpace.GetType() == typeof(ItemInventorySpace))
                            {
                                var itemInvSpace = (ItemInventorySpace)currentSpace;

                                if ((i + (10 * j)) != SelectedTileForMove)
                                {
                                    itemInvSpace.Item.InventorySprite.Draw(spriteBatch, tilePos, Color.White);
                                }
                                else
                                {
                                    itemInvSpace.Item.InventorySprite.Draw(spriteBatch, tilePos, ColorSelectedItem);
                                }
                            }
                            else if (currentSpace.GetType() == typeof(StackInventorySpace))
                            {
                                var stackInvSpace = (StackInventorySpace)currentSpace;

                                if ((i + (10 * j)) != SelectedTileForMove)
                                {
                                    stackInvSpace.ItemStack.Item.InventorySprite.Draw(spriteBatch, tilePos, Color.White);
                                    spriteBatch.DrawString(Constants.InventoryStackFont, stackInvSpace.ItemStack.Count.ToString(), new Vector2(tilePos.X + 45, tilePos.Y + 48), Color.Black);
                                }
                                else
                                {
                                    stackInvSpace.ItemStack.Item.InventorySprite.Draw(spriteBatch, tilePos, ColorSelectedItem);
                                    spriteBatch.DrawString(Constants.InventoryStackFont, stackInvSpace.ItemStack.Count.ToString(), new Vector2(tilePos.X + 45, tilePos.Y + 48), ColorSelectedItemText);
                                }
                            }
                        }
                    }
                }

                // If player is interacting with another inventory 
                // (say like a chest or NPC), render it.
                if (InventoryInteractingWith != null)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            var tilePos = new Vector2(PlayerOffsetX + (i * 68) - GuiOffset, PlayerOffsetY - (68 * j) - 700);

                            if (i + (10 * j) == SelectedTile && IsSelectedTileInOtherInv)
                            {
                                TilesOnMainGui[i + (10 * j)].Draw(spriteBatch, tilePos, ColorSelectedTile);
                            }
                            else
                            {
                                TilesOnMainGui[i + (10 * j)].Draw(spriteBatch, tilePos, ColorTilesOnMainGui);
                            }

                            if (InventoryInteractingWith.Spaces[i + (10 * j)].HasValue)
                            {
                                var currentSpace = InventoryInteractingWith.Spaces[i + (10 * j)];

                                if (currentSpace.GetType() == typeof(ItemInventorySpace))
                                {
                                    var itemInvSpace = (ItemInventorySpace)currentSpace;

                                    if ((i + (10 * j)) != SelectedTileForMove)
                                    {
                                        itemInvSpace.Item.InventorySprite.Draw(spriteBatch, tilePos, Color.White);
                                    }
                                    else
                                    {
                                        itemInvSpace.Item.InventorySprite.Draw(spriteBatch, tilePos, ColorSelectedItem);
                                    }
                                }
                                else if (currentSpace.GetType() == typeof(StackInventorySpace))
                                {
                                    var stackInvSpace = (StackInventorySpace)currentSpace;

                                    if ((i + (10 * j)) != SelectedTileForMove)
                                    {
                                        stackInvSpace.ItemStack.Item.InventorySprite.Draw(spriteBatch, tilePos, Color.White);
                                        spriteBatch.DrawString(Constants.InventoryStackFont, stackInvSpace.ItemStack.Count.ToString(), new Vector2(tilePos.X + 45, tilePos.Y + 48), Color.Black);
                                    }
                                    else
                                    {
                                        stackInvSpace.ItemStack.Item.InventorySprite.Draw(spriteBatch, tilePos, ColorSelectedItem);
                                        spriteBatch.DrawString(Constants.InventoryStackFont, stackInvSpace.ItemStack.Count.ToString(), new Vector2(tilePos.X + 45, tilePos.Y + 48), ColorSelectedItemText);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                    for (int i = 0; i < 10; i++)
                    {
                        if (i == SelectedTile)
                        {
                            TilesOnMainGui[i].Draw(spriteBatch, ColorSelectedTile);
                        }
                        else
                        {
                            TilesOnMainGui[i].Draw(spriteBatch, ColorTilesOnMainGui);
                        }

                        if (Inventory.Spaces[i].HasValue)
                        {
                            Inventory.Spaces[i].SpriteTile.Draw(spriteBatch, Color.White);

                            if (Inventory.Spaces[i].GetType() == typeof(StackInventorySpace))
                            {
                                var stackInvSpace = (StackInventorySpace)Inventory.Spaces[i];
                                spriteBatch.DrawString(Constants.InventoryStackFont, stackInvSpace.ItemStack.Count.ToString(), new Vector2(TilesOnMainGui[i].Position.X + 45, TilesOnMainGui[i].Position.Y + 48), Color.Black);
                            }
                        }
                    }
            }

            if (IsOnPlaceableItem)
            {
                Color color;

                if (MapManager.CurrentMap.CollisionRects.Exists(x => x.Position == TemporaryPlacedObjectInstance.Tile.Position))
                {
                    color = Color.Red;
                    PlaceObjectBlocked = true;
                }
                else
                {
                    color = Color.LightBlue;
                    PlaceObjectBlocked = false;
                }

                TemporaryPlacedObjectInstance.Tile.Draw(spriteBatch, color * 0.6f);
            }

            CraftingManager.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            // Update position
            PlayerOffsetX = PlayerTile.Position.X;
            PlayerOffsetY = PlayerTile.Position.Y + 418;

            for (int i = 0; i < AssociatedKeys.Count; i++)
            {
                TilesOnMainGui[i].Position = new Vector2(PlayerOffsetX + (i * 68) - GuiOffset, PlayerOffsetY);

                if (Inventory.Spaces[i].HasValue)
                {
                    Inventory.Spaces[i].SpriteTile.Position = new Vector2(PlayerOffsetX + (i * 68) - GuiOffset, PlayerOffsetY);
                }

                if (Constants.NewKeyState.IsKeyDown(AssociatedKeys[i]) & !Constants.CurrKeyState.IsKeyDown(AssociatedKeys[i]))
                {
                    SelectedTile = i;
                }
            }

            // Update SelectedTile
            if (!IsInventoryOpen)
            {
                if (SelectedTile > 9)
                {
                    SelectedTile %= 10;
                }

                if (Constants.Input.ShiftRightInventory.IsJustPressed())
                {
                    if (SelectedTile == 9)
                    {
                        SelectedTile = 0;
                    }
                    else
                    {
                        SelectedTile++;
                    }
                }

                else if (Constants.Input.ShiftLeftInventory.IsJustPressed())
                {
                    if (SelectedTile == 0)
                    {
                        SelectedTile = 9;
                    }
                    else
                    {
                        SelectedTile--;
                    }
                }
            }
            else if (IsInventoryOpen)
            {
                if (Constants.Input.DirectionalControls.Up.IsJustPressed() || Constants.StickButtonState.Up)
                {
                    if (SelectedTile < 20)
                    {
                        SelectedTile += 10;
                    }
                    else
                    {
                        SelectedTile %= 10;

                        if (InventoryInteractingWith != null)
                        {
                            IsSelectedTileInOtherInv = !IsSelectedTileInOtherInv;
                        }
                    }
                }
                if (Constants.Input.DirectionalControls.Down.IsJustPressed() || Constants.StickButtonState.Down)
                {
                    if (SelectedTile >= 10)
                    {
                        SelectedTile -= 10;
                    }
                    else
                    {
                        SelectedTile = 20 + (SelectedTile % 10);

                        if (InventoryInteractingWith != null)
                        {
                            IsSelectedTileInOtherInv = !IsSelectedTileInOtherInv;
                        }
                    }
                }
                if (Constants.Input.DirectionalControls.Left.IsJustPressed() || Constants.StickButtonState.Left || Constants.Input.ShiftLeftInventory.IsJustPressed())
                {
                    var mod = SelectedTile % 10;

                    if (mod != 0)
                    {
                        SelectedTile -= 1;
                    }
                    else
                    {
                        SelectedTile += 9;
                    }
                }
                if (Constants.Input.DirectionalControls.Right.IsJustPressed() || Constants.StickButtonState.Right || Constants.Input.ShiftRightInventory.IsJustPressed())
                {
                    var mod = SelectedTile % 10;

                    if (mod != 9)
                    {
                        SelectedTile += 1;
                    }
                    else
                    {
                        SelectedTile -= 9;
                    }
                }

                if (Constants.Input.Interact.IsJustPressed())
                {
                    if (SelectedTileForMove == null && (Inventory.Spaces[SelectedTile].HasValue || (IsSelectedTileInOtherInv && InventoryInteractingWith.Spaces[SelectedTile].HasValue)))
                    {
                        SelectedTileForMove = SelectedTile;

                        if (InventoryInteractingWith != null)
                        {
                            IsSelectedTileForMoveInOtherInv = IsSelectedTileInOtherInv;
                        }
                    }
                    else if (SelectedTileForMove == SelectedTile && (IsSelectedTileInOtherInv == IsSelectedTileForMoveInOtherInv))
                    {
                        SelectedTileForMove = null;

                        if (InventoryInteractingWith != null)
                        {
                            IsSelectedTileForMoveInOtherInv = null;
                        }
                    }
                    else if (SelectedTileForMove != null)
                    {
                        if ((!IsSelectedTileInOtherInv && !Inventory.Spaces[SelectedTile].HasValue) || (IsSelectedTileInOtherInv && !InventoryInteractingWith.Spaces[SelectedTile].HasValue))
                        {
                            // Player to player
                            if (InventoryInteractingWith == null || (!IsSelectedTileInOtherInv && (bool)!IsSelectedTileForMoveInOtherInv))
                            {
                                Inventory.Spaces[SelectedTile] = Inventory.Spaces[(int)SelectedTileForMove].DeepCopy();
                                Inventory.Spaces[(int)SelectedTileForMove] = new InventorySpace(false);
                                SelectedTileForMove = null;
                            }
                            // Interacting inventory to interacting inventory
                            else if (IsSelectedTileInOtherInv && (bool)IsSelectedTileForMoveInOtherInv)
                            {
                                InventoryInteractingWith.Spaces[SelectedTile] = InventoryInteractingWith.Spaces[(int)SelectedTileForMove].DeepCopy();
                                InventoryInteractingWith.Spaces[(int)SelectedTileForMove] = new InventorySpace(false);
                                SelectedTileForMove = null;
                                IsSelectedTileForMoveInOtherInv = null;
                            }
                            // Player to interacting inventory
                            else if (!IsSelectedTileInOtherInv && (bool)IsSelectedTileForMoveInOtherInv)
                            {
                                Inventory.Spaces[SelectedTile] = InventoryInteractingWith.Spaces[(int)SelectedTileForMove].DeepCopy();
                                InventoryInteractingWith.Spaces[(int)SelectedTileForMove] = new InventorySpace(false);
                                SelectedTileForMove = null;
                                IsSelectedTileForMoveInOtherInv = null;
                            }
                            // Interacting inventory to player
                            else if (IsSelectedTileInOtherInv && (bool)!IsSelectedTileForMoveInOtherInv)
                            {
                                InventoryInteractingWith.Spaces[SelectedTile] = Inventory.Spaces[(int)SelectedTileForMove].DeepCopy();
                                Inventory.Spaces[(int)SelectedTileForMove] = new InventorySpace(false);
                                SelectedTileForMove = null;
                                IsSelectedTileForMoveInOtherInv = null;
                            }
                        }
                        else
                        {
                            if (!IsSelectedTileInOtherInv && (bool)!IsSelectedTileForMoveInOtherInv &&
                                (Inventory.Spaces[SelectedTile].GetType() == typeof(StackInventorySpace) &&
                                Inventory.Spaces[(int)SelectedTileForMove].GetType() == typeof(StackInventorySpace) &&
                                Inventory.Spaces[SelectedTile].ItemInterfaced.GetType() == Inventory.Spaces[(int)SelectedTileForMove].ItemInterfaced.GetType()))
                            {
                                ((StackInventorySpace)Inventory.Spaces[SelectedTile]).ItemStack.Stack(((StackInventorySpace)Inventory.Spaces[(int)SelectedTileForMove]).ItemStack);
                                Inventory.Spaces[(int)SelectedTileForMove] = new InventorySpace(false);
                                SelectedTileForMove = null;

                            }
                            else if (IsSelectedTileInOtherInv && (bool)IsSelectedTileForMoveInOtherInv &&
                                (InventoryInteractingWith.Spaces[SelectedTile].GetType() == typeof(StackInventorySpace) &&
                                InventoryInteractingWith.Spaces[(int)SelectedTileForMove].GetType() == typeof(StackInventorySpace) &&
                                InventoryInteractingWith.Spaces[SelectedTile].ItemInterfaced.GetType() == InventoryInteractingWith.Spaces[(int)SelectedTileForMove].ItemInterfaced.GetType()))
                            {
                                ((StackInventorySpace)InventoryInteractingWith.Spaces[SelectedTile]).ItemStack.Stack(((StackInventorySpace)InventoryInteractingWith.Spaces[(int)SelectedTileForMove]).ItemStack);
                                InventoryInteractingWith.Spaces[(int)SelectedTileForMove] = new InventorySpace(false);
                                SelectedTileForMove = null;
                            }
                            else if (!IsSelectedTileInOtherInv && (bool)IsSelectedTileForMoveInOtherInv &&
                                (Inventory.Spaces[SelectedTile].GetType() == typeof(StackInventorySpace) &&
                                InventoryInteractingWith.Spaces[(int)SelectedTileForMove].GetType() == typeof(StackInventorySpace) &&
                                Inventory.Spaces[SelectedTile].ItemInterfaced.GetType() == InventoryInteractingWith.Spaces[(int)SelectedTileForMove].ItemInterfaced.GetType()))
                            {
                                ((StackInventorySpace)Inventory.Spaces[SelectedTile]).ItemStack.Stack(((StackInventorySpace)InventoryInteractingWith.Spaces[(int)SelectedTileForMove]).ItemStack);
                                InventoryInteractingWith.Spaces[(int)SelectedTileForMove] = new InventorySpace(false);
                                SelectedTileForMove = null;
                            }
                            else if (IsSelectedTileInOtherInv && (bool)!IsSelectedTileForMoveInOtherInv &&
                                (InventoryInteractingWith.Spaces[SelectedTile].GetType() == typeof(StackInventorySpace) &&
                                Inventory.Spaces[(int)SelectedTileForMove].GetType() == typeof(StackInventorySpace) &&
                                InventoryInteractingWith.Spaces[SelectedTile].ItemInterfaced.GetType() == Inventory.Spaces[(int)SelectedTileForMove].ItemInterfaced.GetType()))
                            {
                                ((StackInventorySpace)InventoryInteractingWith.Spaces[SelectedTile]).ItemStack.Stack(((StackInventorySpace)Inventory.Spaces[(int)SelectedTileForMove]).ItemStack);
                                Inventory.Spaces[(int)SelectedTileForMove] = new InventorySpace(false);
                                SelectedTileForMove = null;
                            }
                        }
                    }
                }
            }

            if (!IsInventoryOpen && Inventory.Spaces[SelectedTile].HasValue &&
                Inventory.Spaces[SelectedTile].ItemInterfaced.GetType().IsSubclassOf(typeof(PlaceableItem)))
            {
                IsOnPlaceableItem = true;

                if (TemporaryPlacedObjectInstance == null)
                {
                    var placeableItem = (PlaceableItem)Inventory.Spaces[SelectedTile].ItemInterfaced;
                    TemporaryPlacedObjectInstance = new PlacedObjectInstance(PlacedObjectDictionary.Dictionary[placeableItem.PlacedObjecttId]);
                }
            }
            else
            {
                IsOnPlaceableItem = false;
                TemporaryPlacedObjectInstance = null;
            }

            CraftingManager.Update(gameTime);

            // Open and close Inventory GUI
            if (!BlockOpen && Constants.Input.ToggleInventory.IsJustPressed())
            {
                if (IsInventoryOpen)
                {
                    InventoryInteractingWith = null;
                    SelectedTileForMove = null;
                    IsSelectedTileForMoveInOtherInv = null;
                    IsSelectedTileInOtherInv = false;
                }

                IsInventoryOpen = !IsInventoryOpen;
            }

            if (CraftingManager.Toggled)
            {
                if (IsInventoryOpen)
                {
                    InventoryInteractingWith = null;
                    SelectedTileForMove = null;
                    IsSelectedTileForMoveInOtherInv = null;
                    IsSelectedTileInOtherInv = false;
                }

                IsInventoryOpen = false;
            }
        }
    }
}
