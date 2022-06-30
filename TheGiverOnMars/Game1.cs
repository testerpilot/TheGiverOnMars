using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System.Collections.Generic;
using System.Text.Json;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.Item.Definitions;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Objects;
using TheGiverOnMars.States;

namespace TheGiverOnMars
{
    public class TheGiverOnMars : Game
    {
        public TheGiverOnMars()
        {
            Constants.Graphics = new GraphicsDeviceManager(this);
            Constants.Game = this;
            Constants.Content = Content;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeState(States.State state)
        {
            Constants.NextState = state;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Constants.Graphics.PreferredBackBufferWidth = 1600;
            Constants.Graphics.PreferredBackBufferHeight = 950;
            Constants.Graphics.ApplyChanges();

            Constants.ScreenHeight = Constants.Graphics.PreferredBackBufferHeight;
            Constants.ScreenWidth = Constants.Graphics.PreferredBackBufferWidth;

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Constants.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Constants.CurrentState = new MenuState();
            Constants.SceneManager = new SceneManager();
        }

        protected override void Update(GameTime gameTime)
        {
            Constants.GameTime = gameTime;

            if (Constants.NextState != null)
            {
                Constants.CurrentState = Constants.NextState;

                Constants.NextState = null;
            }

            Constants.CurrentState.Update(gameTime);

            Constants.CurrentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Constants.CurrentState.BackgroundColor);

            Constants.CurrentState.Draw(gameTime, Constants.SpriteBatch);

            base.Draw(gameTime);
        }
    }
}
