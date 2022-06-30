using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using Penumbra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using TheGiverOnMars.Components;
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
            Constants.Penumbra = new Penumbra.PenumbraComponent(Constants.Game);
            Constants.Penumbra.Initialize();

            Constants.Penumbra.Lights.Add(new PointLight()
            {
                Position = new Vector2(500, 500),
                Enabled = true,
                Color = Color.DeepSkyBlue,
                Radius = 200,
                Scale = new Vector2(1000, 1000),
                Intensity = 0.5f
            });

            Constants.Penumbra.Lights.Add(new PointLight()
            {
                Position = new Vector2(50, 500),
                Enabled = true,
                Color = Color.DarkCyan,
                Radius = 200,
                Scale = new Vector2(1000, 1000),
                Intensity = 0.5f
            });

            Constants.Penumbra.Lights.Add(new PointLight()
            {
                Position = new Vector2(950, 500),
                Enabled = true,
                Color = Color.OrangeRed,
                Radius = 200,
                Scale = new Vector2(1000, 1000),
                Intensity = 0.5f
            });

            Constants.Penumbra.Lights.Add(new PointLight()
            {
                Position = new Vector2(500, 250),
                Enabled = true,
                Color = Color.Violet,
                Radius = 200,
                Scale = new Vector2(1000, 1000),
                Intensity = 0.5f
            });

            Constants.Penumbra.Lights.Add(new PointLight()
            {
                Position = new Vector2(500, 900),
                Enabled = true,
                Color = Color.ForestGreen,
                Radius = 200,
                Scale = new Vector2(1000, 1000),
                Intensity = 1f
            });

            Constants.Input = new InputWrapper()
            {
                DirectionalControls = new DirectionalControlsWrapper()
                {
                    Up = new KeyWrapper(Keys.Up, Buttons.DPadUp),
                    Down = new KeyWrapper(Keys.Down, Buttons.DPadDown),
                    Left = new KeyWrapper(Keys.Left, Buttons.DPadLeft),
                    Right = new KeyWrapper(Keys.Right, Buttons.DPadRight)
                },
                ToggleInventory = new KeyWrapper(Keys.Tab, Buttons.Y),
                ToggleCrafting = new KeyWrapper(Keys.R, Buttons.B),
                ShiftLeftInventory = new KeyWrapper(Keys.Q, Buttons.LeftShoulder),
                ShiftRightInventory = new KeyWrapper(Keys.E, Buttons.RightShoulder),
                Interact = new KeyWrapper(Keys.C, Buttons.X),
                Use = new KeyWrapper(Keys.V, Buttons.A),
                Save = new KeyWrapper(Keys.Z, Buttons.Start)
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
            Constants.Penumbra.BeginDraw();

            Constants.Penumbra.Transform = _camera.Transform;

            spriteBatch.Begin(transformMatrix: _camera.Transform);

            MapManager.CurrentMap.Draw(spriteBatch);

            _player.Draw(spriteBatch);

            Constants.SceneManager.Draw(spriteBatch, _player.Tile.Position);

            spriteBatch.End();

            Constants.Penumbra.Draw(gameTime);

            spriteBatch.Begin(transformMatrix: _camera.Transform);

            _player.InventoryManager.Draw(null, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Constants.Penumbra.Update(gameTime);

            Constants.NewKeyState = Keyboard.GetState();
            Constants.NewGamePadState = GamePad.GetState(PlayerIndex.One);
            Constants.StickButtonState = Constants.Input.DirectionalControls.InputIsJustPressed();

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
            Constants.CurrGamePadState = Constants.NewGamePadState;
        }
    }
}
