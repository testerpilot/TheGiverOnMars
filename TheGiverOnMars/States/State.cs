using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGiverOnMars.States
{
    public abstract class State
    {
        #region Fields

        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected TheGiverOnMars _game;

        public Color BackgroundColor;

        #endregion

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public State(TheGiverOnMars game, GraphicsDevice graphicsDevice, ContentManager content, Color backgroundColor)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;

            BackgroundColor = backgroundColor;
        }

        public abstract void Update(GameTime gameTime);

        #endregion
    }
}
