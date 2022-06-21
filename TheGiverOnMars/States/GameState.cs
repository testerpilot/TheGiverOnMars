using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.States
{
    public class GameState : State
    {
        private SpriteBatch _spriteBatch;
        private SpriteManager _spriteManager;
        private Player _player;
        private Camera _camera;

        public GameState(TheGiverOnMars game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content, Color.Black)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _spriteManager = new SpriteManager(content);

            TileManager.LoadTiles(_spriteManager);
            MapManager.LoadMap("test");

            _player = new Player(_spriteManager.GetSpriteFromDict(2), _game.Content.Load<SpriteSheet>("Player/motw.sf", new JsonContentLoader()));
            _camera = new Camera();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            MapManager.CurrentMap.Draw(_spriteBatch);

            _player.Draw(_spriteBatch);

            _game._sceneManager.Draw(_spriteBatch, _player.Tile.Position);

            _spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.Exit();

            // TODO: Add your update logic here

            _player.Update(gameTime, ref _game._sceneManager, MapManager.CurrentMap);
            _camera.Follow(_player.Tile);
            _game._sceneManager.Update(gameTime, _spriteBatch, _player.Tile.Position);
        }
    }
}
