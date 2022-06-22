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
using TheGiverOnMars.Components.PlacedObject;
using TheGiverOnMars.Dictionaries;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Utilities;

namespace TheGiverOnMars.Objects
{
    public class Player
    {
        public CollisionTile Tile;
        public AnimatedSprite AnimatedSprite;
        public Vector2? NextSpawnPoint = null;
        public string CurrentAnimation = "idleSouth";

        public InventoryManager InventoryManager;

        public Player(Texture2D collisionTexture, SpriteSheet spriteSheet)
        {
            Tile = new CollisionTile(collisionTexture);
            Tile.Speed = 4;
            Tile.Position.X = 100;
            Tile.Position.Y = 100;

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

        private void Move(GameTime gameTime)
        {
            if (!InventoryManager.IsInventoryOpen)
            {
                var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
                var walkSpeed = deltaSeconds * 128;
                var keyboardState = Keyboard.GetState();

                var previousAnimation = CurrentAnimation;

                if (CurrentAnimation.Contains("walk"))
                {
                    CurrentAnimation = CurrentAnimation.Replace("walk", "idle");
                }

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    Tile.Velocity.X = -Tile.Speed;
                    CurrentAnimation = "walkWest";
                }

                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    Tile.Velocity.X = Tile.Speed;
                    CurrentAnimation = "walkEast";
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    Tile.Velocity.Y = -Tile.Speed;
                    CurrentAnimation = "walkNorth";
                }

                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    Tile.Velocity.Y = Tile.Speed;
                    CurrentAnimation = "walkSouth";
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
                if (Keyboard.GetState().IsKeyDown(Keys.C) && currentTile.IsInProximity(Tile) && !InventoryManager.IsInventoryOpen)
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
                if (Keyboard.GetState().IsKeyDown(Keys.C) && currentObject.Tile.IsInProximity(Tile) && !InventoryManager.IsInventoryOpen)
                {
                    if (currentObject.PlacedObject.GetType().BaseType == typeof(InteractablePlacedObject))
                    {
                        ((InteractablePlacedObject)currentObject.PlacedObject).Interact(this);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AnimatedSprite, Tile.Position);
            InventoryManager.Draw(null, spriteBatch);
        }
    }
}
