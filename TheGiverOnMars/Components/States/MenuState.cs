using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using TheGiverOnMars.Components;
using TheGiverOnMars.Managers;

namespace TheGiverOnMars.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        private GameStateSave saveToLoad = null;

        public MenuState()
          : base(Color.IndianRed)
        {
            var buttonTexture = Constants.Content.Load<Texture2D>("Controls/Button");
            Constants.InventoryStackFont = Constants.Content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, Constants.InventoryStackFont)
            {
                Position = new Vector2((Constants.Graphics.PreferredBackBufferWidth / 2) - 100, 200),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var loadGameButton = new Button(buttonTexture, Constants.InventoryStackFont)
            {
                Position = new Vector2((Constants.Graphics.PreferredBackBufferWidth / 2) - 100, 250),
                Text = "Load Game",
            };

            loadGameButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(buttonTexture, Constants.InventoryStackFont)
            {
                Position = new Vector2((Constants.Graphics.PreferredBackBufferWidth / 2) - 100, 300),
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

            Constants.SceneManager.Draw(spriteBatch, new Vector2(Constants.Graphics.PreferredBackBufferWidth / 2, Constants.Graphics.PreferredBackBufferHeight / 2));

            spriteBatch.End();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            Constants.SceneManager.CurrentState = Managers.State.LoadState;
            Constants.SceneManager.CurrentSubstate = Managers.State.Fade;

            string json = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/test.sav");
            saveToLoad = JsonSerializer.Deserialize<GameStateSave>(json);
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Constants.SceneManager.CurrentState = Managers.State.LoadState;
            Constants.SceneManager.CurrentSubstate = Managers.State.Fade;
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            Constants.SceneManager.Update(gameTime, Constants.SpriteBatch, new Vector2(300, 300));

            foreach (var component in _components)
                component.Update(gameTime);

            if (Constants.SceneManager.CurrentSubstate == Managers.State.Waiting && Constants.SceneManager.CurrentState == Managers.State.LoadState)
            {
                Constants.Game.ChangeState(new GameState(saveToLoad));
                Constants.SceneManager.CurrentSubstate = Managers.State.PlayerLoadDone;
            }
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            Constants.Game.Exit();
        }
    }
}
