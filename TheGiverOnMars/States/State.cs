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

        public Color BackgroundColor;

        #endregion

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public State(Color backgroundColor)
        {
            BackgroundColor = backgroundColor;
        }

        public abstract void Update(GameTime gameTime);

        #endregion
    }
}
