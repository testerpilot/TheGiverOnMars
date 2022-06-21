using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGiverOnMars.Objects
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Tile target)
        {
            var position = Matrix.CreateTranslation(
              -target.Position.X - (target.Rectangle.Width / 2),
              -target.Position.Y - (target.Rectangle.Height / 2),
              0);

            var offset = Matrix.CreateTranslation(
                TheGiverOnMars.ScreenWidth / 2,
                TheGiverOnMars.ScreenHeight / 2,
                0);

            Transform = position * offset;
        }
    }
}
