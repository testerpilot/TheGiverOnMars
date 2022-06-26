using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components;
using TheGiverOnMars.Managers;

namespace TheGiverOnMars
{
    public static class Constants
    {
        public static GraphicsDeviceManager Graphics { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static ContentManager Content { get; set; }
        public static TheGiverOnMars Game { get; set; }

        public static int ScreenHeight;
        public static int ScreenWidth;

        public static States.State CurrentState;
        public static States.State NextState;

        public static SceneManager SceneManager;

        public static SpriteFont InventoryStackFont { get; set; }

        public static KeyboardState CurrKeyState { get; set; }
        public static KeyboardState NewKeyState { get; set; }
        public static InputWrapper Input { get; set; }
    }
}
