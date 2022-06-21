using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components;
using TheGiverOnMars.Managers;

namespace TheGiverOnMars.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(TheGiverOnMars game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content, Color.IndianRed)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            UniversalContentManager.InventoryStackFont = buttonFont;

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((_game._graphics.PreferredBackBufferWidth / 2) - 100, 200),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var loadGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((_game._graphics.PreferredBackBufferWidth / 2) - 100, 250),
                Text = "Load Game",
            };

            loadGameButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((_game._graphics.PreferredBackBufferWidth / 2) - 100, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
              {
                newGameButton,
                loadGameButton,
                quitGameButton,
              };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            _game._sceneManager.Draw(spriteBatch, new Vector2(_game._graphics.PreferredBackBufferWidth / 2, _game._graphics.PreferredBackBufferHeight / 2));

            spriteBatch.End();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            _game._sceneManager.CurrentState = Managers.State.LoadState;
            _game._sceneManager.CurrentSubstate = Managers.State.Fade;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game._sceneManager.CurrentState = Managers.State.LoadState;
            _game._sceneManager.CurrentSubstate = Managers.State.Fade;
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            _game._sceneManager.Update(gameTime, _game._spriteBatch, new Vector2(300, 300));

            foreach (var component in _components)
                component.Update(gameTime);

            if (_game._sceneManager.CurrentSubstate == Managers.State.Waiting && _game._sceneManager.CurrentState == Managers.State.LoadState)
            {
                _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
                _game._sceneManager.CurrentSubstate = Managers.State.PlayerLoadDone;
            }
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
