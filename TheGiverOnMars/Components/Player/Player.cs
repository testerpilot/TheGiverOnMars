using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.Map;
using TheGiverOnMars.Components.PlacedObject;
using TheGiverOnMars.Managers;

namespace TheGiverOnMars.Objects
{

    public class Player
    {
        public enum Direction { N, S, E, W };

        public CollisionTile Tile;
        public AnimatedSprite AnimatedSprite;
        public Vector2? NextSpawnPoint = null;
        public string CurrentAnimation = "idleSouth";
        public Direction CurrentDirection = Direction.S;

        public InventoryManager InventoryManager;

        public Player(Texture2D collisionTexture, SpriteSheet spriteSheet)
        {
            Tile = new CollisionTile(collisionTexture);
            Tile.Speed = 4;
            Tile.Position.X = 100;
            Tile.Position.Y = 100;
            Tile.OffsetX = -32;
            Tile.OffsetY = -10;
            Tile.CollisionHeight = 25;
            Tile.CollisionWidth = 25;

            MapManager.CurrentMap.CollisionRects.Add(Tile);

            InventoryManager = new InventoryManager(new Inventory(), Tile);

            AnimatedSprite = new AnimatedSprite(spriteSheet);
            AnimatedSprite.Play(CurrentAnimation);
        }

        public Player(Texture2D collisionTexture, SpriteSheet spriteSheet, PlayerSaveData playerData)
        {
            Tile = new CollisionTile(collisionTexture);
            Tile.Speed = 4;
            Tile.Position.X = playerData.PosX;
            Tile.Position.Y = playerData.PosY;

            InventoryManager = new InventoryManager(new Inventory(playerData.Inventory), Tile);
            AnimatedSprite = new AnimatedSprite(spriteSheet);
            AnimatedSprite.Play(CurrentAnimation);
        }

        public PlayerSaveData Save()
        {
            var data = new PlayerSaveData()
            {
                PosX = Tile.Position.X,
                PosY = Tile.Position.Y,
                Inventory = InventoryManager.Inventory.Save()
            };

            return data;
        }

        public void Update(GameTime gameTime, Map map)
        {
            Move(gameTime);
            CheckTransitions(map.TransitionTiles);
            CheckPlacedObjects(map.PlacedObjects);
            CheckPlaceableItem();
            CheckDroppedItems();

            if (!InventoryManager.IsInventoryOpen && 
                !InventoryManager.CraftingManager.Toggled &&
                Constants.Input.Use.IsJustPressed())
            {
                if (InventoryManager.IsOnPlaceableItem && !InventoryManager.PlaceObjectBlocked)
                {
                    ((PlaceableItem)InventoryManager.Inventory.Spaces[InventoryManager.SelectedTile].ItemInterfaced).Place(InventoryManager.TemporaryPlacedObjectInstance.Tile.PositionOnMap);

                    InventoryManager.Inventory.RemoveItemFromSpace(InventoryManager.SelectedTile);
                }

                if (InventoryManager.Inventory.Spaces[InventoryManager.SelectedTile].HasValue &&
                    InventoryManager.Inventory.Spaces[InventoryManager.SelectedTile].ItemInterfaced.GetType().IsSubclassOf(typeof(ActionItem)))
                {
                    ((ActionItem)InventoryManager.Inventory.Spaces[InventoryManager.SelectedTile].ItemInterfaced).OnUse(this);
                }
            }

            foreach (var currentTile in map.CollisionRects)
            {
                if (currentTile == Tile)
                    continue;

                if ((Tile.Velocity.X > 0 && Tile.IsTouchingLeft(currentTile)) ||
                    (Tile.Velocity.X < 0 & Tile.IsTouchingRight(currentTile)))
                {
                    Tile.Velocity.X = 0;
                }

                if ((Tile.Velocity.Y > 0 && Tile.IsTouchingTop(currentTile)) ||
                    (Tile.Velocity.Y < 0 & Tile.IsTouchingBottom(currentTile)))
                {
                    Tile.Velocity.Y = 0;
                }
            }

            Tile.Position += Tile.Velocity;

            Tile.Velocity = Vector2.Zero;

            InventoryManager.Update(gameTime);
        }

        private void CheckPlaceableItem()
        {
            if (InventoryManager.IsOnPlaceableItem)
            {
                var position = Tile.PositionOnMap;

                if (CurrentDirection == Direction.W)
                {
                    position.X -= 1;
                }
                if (CurrentDirection == Direction.E)
                {
                    position.X += 1;
                }
                if (CurrentDirection == Direction.N)
                {
                    position.Y -= 1;
                }
                if (CurrentDirection == Direction.S)
                {
                    position.Y += 1;
                }

                InventoryManager.TemporaryPlacedObjectInstance.Tile.PositionOnMap = position;
            }
        }

        private void Move(GameTime gameTime)
        {
            if (!InventoryManager.IsInventoryOpen && 
                !InventoryManager.CraftingManager.Toggled)
            {
                var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
                var walkSpeed = deltaSeconds * 128;
                var keyboardState = Keyboard.GetState();

                var previousAnimation = CurrentAnimation;

                if (CurrentAnimation.Contains("walk"))
                {
                    CurrentAnimation = CurrentAnimation.Replace("walk", "idle");
                }

                if (Constants.Input.DirectionalControls.InputLeftIsDown())
                {
                    Tile.Velocity.X = -Tile.Speed;
                    CurrentAnimation = "walkWest";
                    CurrentDirection = Direction.W;
                }

                else if (Constants.Input.DirectionalControls.InputRightIsDown())
                {
                    Tile.Velocity.X = Tile.Speed;
                    CurrentAnimation = "walkEast";
                    CurrentDirection = Direction.E;
                }

                if (Constants.Input.DirectionalControls.InputUpIsDown())
                {
                    Tile.Velocity.Y = -Tile.Speed;
                    CurrentAnimation = "walkNorth";
                    CurrentDirection = Direction.N;
                }

                else if (Constants.Input.DirectionalControls.InputDownIsDown())
                {
                    Tile.Velocity.Y = Tile.Speed;
                    CurrentAnimation = "walkSouth";
                    CurrentDirection = Direction.S;
                }

                if (Tile.Speed != 0)
                {
                    AnimatedSprite.Play(CurrentAnimation);
                    AnimatedSprite.Update(deltaSeconds);
                }
            }
        }

        private void CheckTransitions(List<TransitionTile> transitionTiles)
        {
            foreach (var currentTile in transitionTiles)
            {
                if (Constants.Input.Interact.IsDown() && currentTile.IsInProximity(Tile) && !InventoryManager.IsInventoryOpen)
                {
                    Constants.SceneManager.TransitionToMap(currentTile.MapTransitionTo);
                    NextSpawnPoint = currentTile.SpawnPointOnLoad;
                    Tile.Speed = 0;
                    InventoryManager.BlockOpen = true;
                }
            }

            if (Constants.SceneManager.CurrentState == State.None && Constants.SceneManager.CurrentSubstate == State.None)
            {
                Tile.Speed = 4;
                InventoryManager.BlockOpen = false;
            }

            // Load player data in this block
            if (Constants.SceneManager.CurrentSubstate == State.WaitingOnPlayerLoad)
            {
                Tile.Position = (Vector2) NextSpawnPoint;
                NextSpawnPoint = null;
                Constants.SceneManager.CurrentSubstate = State.PlayerLoadDone;
            }
        }

        private void CheckPlacedObjects(List<PlacedObjectInstance> placedObjects)
        {
            foreach (var currentObject in placedObjects)
            {
                if (Constants.Input.Interact.IsDown() && currentObject.Tile.IsInProximity(Tile) && !InventoryManager.IsInventoryOpen)
                {
                    if (currentObject.PlacedObject.GetType().Equals(typeof(InteractablePlacedObject)) || 
                        currentObject.PlacedObject.GetType().IsSubclassOf(typeof(InteractablePlacedObject)))
                    {
                        ((InteractablePlacedObject)currentObject.PlacedObject).Interact(this);
                    }
                }
            }
        }

        private void CheckDroppedItems()
        {
            foreach (var droppedItem in MapManager.CurrentMap.DroppedItems)
            {
                if (droppedItem.Instance.InventorySprite.IsInProximity(Tile) && InventoryManager.Inventory.AddDroppedItem(droppedItem))
                {
                    MapManager.CurrentMap.ItemsForRemoval.Add(droppedItem);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AnimatedSprite, Tile.Position);
        }
    }
}
