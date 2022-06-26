using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using TheGiverOnMars.Components;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.Item.Definitions;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Objects;
using static TheGiverOnMars.Components.InputWrapper;

namespace TheGiverOnMars.States
{
    public class GameState : State
    {
        private Player _player;
        private Camera _camera;

        public GameState(GameStateSave save = null)
          : base(Color.Black)
        {
            SpriteManager.LoadSprites();
            TileManager.LoadTiles();

            Constants.Input = new InputWrapper()
            {
                Up = new KeyWrapper(Keys.Up),
                Down = new KeyWrapper(Keys.Down),
                Left = new KeyWrapper(Keys.Left),
                Right = new KeyWrapper(Keys.Right),
                ToggleInventory = new KeyWrapper(Keys.Tab),
                ShiftLeftInventory = new KeyWrapper(Keys.Q),
                ShiftRightInventory = new KeyWrapper(Keys.E),
                Interact = new KeyWrapper(Keys.C),
                Use = new KeyWrapper(Keys.V),
                Save = new KeyWrapper(Keys.Z)
            };

            if (save != null)
            {
                MapManager.LoadSave(save.MapData);
                _player = new Player(SpriteManager.GetSpriteFromDict(2), Constants.Content.Load<SpriteSheet>("Player/motw.sf", new JsonContentLoader()), save.PlayerData);
            }
            else
            {
                MapManager.LoadMap("test");
                _player = new Player(SpriteManager.GetSpriteFromDict(2), Constants.Content.Load<SpriteSheet>("Player/motw.sf", new JsonContentLoader()));
            }

            _camera = new Camera();
        }

        public void Save()
        {
            var gameSave = new GameStateSave()
            {
                PlayerData = _player.Save(),
                MapData = MapManager.GetTempMapSaves()
            };

            string serializedText = JsonSerializer.Serialize(gameSave);
            string path = Directory.GetCurrentDirectory() + "/test.sav";

            File.WriteAllText(path, serializedText);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: _camera.Transform);

            MapManager.CurrentMap.Draw(spriteBatch);

            _player.Draw(spriteBatch);

            Constants.SceneManager.Draw(spriteBatch, _player.Tile.Position);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Constants.NewKeyState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Constants.Game.Exit();

            // TODO: Add your update logic here

            if (Constants.Input.Save.IsJustPressed())
                Save();

            MapManager.CurrentMap.Update(gameTime);

            _player.Update(gameTime, MapManager.CurrentMap);
            _camera.Follow(_player.Tile);
            Constants.SceneManager.Update(gameTime, Constants.SpriteBatch, _player.Tile.Position);

            Constants.CurrKeyState = Constants.NewKeyState;
        }
    }
}
