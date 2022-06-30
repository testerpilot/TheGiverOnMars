using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGiverOnMars.Components;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Dictionaries;

namespace TheGiverOnMars.Managers
{
    public class CraftingManager : Component
    {
        public Color ColorTilesOnMainGui = Color.LightGray * 0.7f;
        public Color ColorSelectedTile = Color.DimGray * 0.7f;
        public Color ColorTilesDisabled = Color.Gray * 0.7f;
        public Color ColorTilesReqUnsatisfied = Color.IndianRed * 0.7f;

        public SpriteTile[] TilesOnMainGui;
        public SpriteTile[] CraftRecipeGui;
        public SpriteTile ArrowTile = TileManager.GetTileFromID(39);

        public Tile PlayerTile;

        public int SelectedTile = 0;

        public int MaxRows;
        public int LastColumn;

        public bool Toggled = false;
        public bool BlockOpen = false;

        public const int GuiOffset = (12 * 68) / 2;
        public const int CraftRecipeOffset = GuiOffset / 2;
        public float PlayerOffsetX, PlayerOffsetY;

        public List<ContractInstance> Recipes { get; set; } = CraftingRecipesDictionary.Dictionary.Values.Select(x => new ContractInstance(x)).ToList();
        public List<List<int>> IndexesOfRecipesReqsUnsatified = new List<List<int>>();
        public List<bool> IsRecipesSatisfied = new List<bool>();

        public Inventory Inventory;

        public CraftingManager(Tile playerTile, Inventory inventory)
        {
            PlayerTile = playerTile;
            Inventory = inventory;

            SetupInventoryTiles(TileManager.GetTileFromID(20));
        }

        public void SetupInventoryTiles(SpriteTile baseTile)
        {
            TilesOnMainGui = new SpriteTile[72];
            CraftRecipeGui = new SpriteTile[6];

            for (int i = 0; i < 72; i++)
            {
                var tempBaseTile = baseTile.DeepCopy();
                tempBaseTile.Position = new Vector2();
                TilesOnMainGui[i] = tempBaseTile;
            }

            for (int i = 0; i < 6; i++)
            {
                var tempBaseTile = baseTile.DeepCopy();
                tempBaseTile.Position = new Vector2();
                CraftRecipeGui[i] = tempBaseTile;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Toggled)
            {
                for (int j = 0; j < 6; j++)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        var tilePos = new Vector2(PlayerOffsetX + (i * 68) - GuiOffset, PlayerOffsetY - (68 * j));

                        if ((i + (12 * j)) < Recipes.Count)
                        {
                            if (i + (12 * j) == SelectedTile)
                            {
                                TilesOnMainGui[i + (12 * j)].Draw(spriteBatch, tilePos, ColorSelectedTile);
                            }
                            else
                            {
                                TilesOnMainGui[i + (12 * j)].Draw(spriteBatch, tilePos, ColorTilesOnMainGui);
                            }

                            if (!IsRecipesSatisfied[i + (12 * j)])
                            {
                                Recipes[i + (12 * j)].Promise.FirstOrDefault().Item.InventorySprite.Draw(spriteBatch, tilePos, Color.White * 0.5f);
                            }
                            else
                            {
                                Recipes[i + (12 * j)].Promise.FirstOrDefault().Item.InventorySprite.Draw(spriteBatch, tilePos, Color.White);
                            }

                            if (Recipes[i + (12 * j)].Promise.FirstOrDefault().Count > 1)
                            {
                                spriteBatch.DrawString(Constants.InventoryStackFont, Recipes[i + (12 * j)].Promise.FirstOrDefault().Count.ToString(), new Vector2(tilePos.X + 45, tilePos.Y + 48), Color.Black);
                            }
                        }
                        else
                        {
                            TilesOnMainGui[i + (12 * j)].Draw(spriteBatch, tilePos, ColorTilesDisabled);
                        }
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    var flippedTilePos = 3 - i;

                    // Render crafting materials right to left
                    var tilePos = new Vector2(PlayerOffsetX + (flippedTilePos * 68) - CraftRecipeOffset, PlayerOffsetY + 136);

                    if (IndexesOfRecipesReqsUnsatified[SelectedTile].Contains(i))
                    {
                        CraftRecipeGui[flippedTilePos].Draw(spriteBatch, tilePos, ColorTilesReqUnsatisfied);
                    }
                    else
                    {
                        CraftRecipeGui[flippedTilePos].Draw(spriteBatch, tilePos, ColorTilesOnMainGui);
                    }

                    if (i < Recipes[SelectedTile].Requirement.Count)
                    {
                        Recipes[SelectedTile].Requirement[i].Item.InventorySprite.Draw(spriteBatch, tilePos, Color.White);
                        
                        if (Recipes[SelectedTile].Requirement[i].Count > 1)
                        {
                            spriteBatch.DrawString(Constants.InventoryStackFont, Recipes[SelectedTile].Requirement[i].Count.ToString(), new Vector2(tilePos.X + 45, tilePos.Y + 48), Color.Black);
                        }
                    }
                }

                for (int i = 4; i < 6; i++)
                {
                    var tilePos = new Vector2(PlayerOffsetX + (i * 68) - CraftRecipeOffset, PlayerOffsetY + 136);

                    if (!IsRecipesSatisfied[SelectedTile])
                    {
                        TilesOnMainGui[i].Draw(spriteBatch, tilePos, ColorTilesReqUnsatisfied);
                    }
                    else
                    {
                        TilesOnMainGui[i].Draw(spriteBatch, tilePos, ColorTilesOnMainGui);
                    }

                    if (i == 4)
                    {
                        ArrowTile.Draw(spriteBatch, tilePos, Color.White);
                    }

                    if (i == 5)
                    {
                        Recipes[SelectedTile].Promise.FirstOrDefault().Item.InventorySprite.Draw(spriteBatch, tilePos, Color.White);

                        if (Recipes[SelectedTile].Promise.FirstOrDefault().Count > 1)
                        {
                            spriteBatch.DrawString(Constants.InventoryStackFont, Recipes[SelectedTile].Promise.FirstOrDefault().Count.ToString(), new Vector2(tilePos.X + 45, tilePos.Y + 48), Color.Black);
                        }
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Update position
            PlayerOffsetX = PlayerTile.Position.X;
            PlayerOffsetY = PlayerTile.Position.Y - 122;

            // Update maxRows
            MaxRows = (Recipes.Count - 1) / 12;
            LastColumn = (Recipes.Count - 1) % 12;

            if (Constants.Input.ToggleCrafting.IsJustPressed())
            {
                Toggled = !Toggled;

                if (Toggled)
                {
                    IndexesOfRecipesReqsUnsatified.Clear();
                    IsRecipesSatisfied.Clear();

                    foreach (var recipe in Recipes)
                    {
                        var isSatisfied = recipe.DoesInventorySatisfy(Inventory, out List<int> indexes);

                        IndexesOfRecipesReqsUnsatified.Add(indexes);
                        IsRecipesSatisfied.Add(isSatisfied);
                    }
                }
            }

            if (Toggled)
            {
                int tempSelectedTile = SelectedTile;

                if (Constants.Input.Use.IsJustPressed() && IsRecipesSatisfied[SelectedTile])
                {
                    Recipes[SelectedTile].FulfillContract(ref Inventory, PlayerTile);

                    IndexesOfRecipesReqsUnsatified.Clear();
                    IsRecipesSatisfied.Clear();

                    foreach (var recipe in Recipes)
                    {
                        var isSatisfied = recipe.DoesInventorySatisfy(Inventory, out List<int> indexes);

                        IndexesOfRecipesReqsUnsatified.Add(indexes);
                        IsRecipesSatisfied.Add(isSatisfied);
                    }
                }

                if (Constants.Input.DirectionalControls.Up.IsJustPressed() || Constants.StickButtonState.Up)
                {
                    tempSelectedTile += 12;

                    if (tempSelectedTile > Recipes.Count - 1)
                    {
                        SelectedTile %= 12;
                    }
                    else
                    {
                        SelectedTile = tempSelectedTile;
                    }
                }

                if (Constants.Input.DirectionalControls.Down.IsJustPressed() || Constants.StickButtonState.Down)
                {
                    tempSelectedTile -= 12;

                    if (tempSelectedTile < 0)
                    {
                        var mod = tempSelectedTile % 12;

                        if (mod > LastColumn)
                        {
                            SelectedTile = ((MaxRows - 1) * 12) + mod;
                        }
                        else
                        {
                            SelectedTile = (MaxRows * 12) + mod;
                        }
                    }
                    else
                    {
                        SelectedTile = tempSelectedTile;
                    }
                }

                if (Constants.Input.DirectionalControls.Right.IsJustPressed() || Constants.StickButtonState.Right)
                {
                    var mod = SelectedTile % 12;
                    var row = SelectedTile / 12;

                    if ((row == MaxRows && mod == LastColumn) || mod == 11)
                    { 
                        SelectedTile = row * 12;
                    }
                    else
                    {
                        SelectedTile += 1;
                    }
                }

                if (Constants.Input.DirectionalControls.Left.IsJustPressed() || Constants.StickButtonState.Left)
                {
                    var mod = SelectedTile % 12;
                    var row = SelectedTile / 12;

                    if (mod != 0)
                    {
                        SelectedTile -= 1;
                    }
                    else
                    {
                        if (row == MaxRows)
                        {
                            SelectedTile = LastColumn + (row * 12);
                        }
                        else
                        {
                            SelectedTile += 11;
                        }
                    }
                }

                if (Constants.Input.ToggleInventory.IsJustPressed())
                {
                    Toggled = false;
                }
            }
        }
    }
}
