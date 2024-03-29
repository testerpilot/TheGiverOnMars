﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
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
        public static GameTime GameTime { get; set; }
        public static TheGiverOnMars Game { get; set; }

        public static int ScreenHeight;
        public static int ScreenWidth;

        public static States.State CurrentState;
        public static States.State NextState;

        public static SceneManager SceneManager;

        public static SpriteFont InventoryStackFont { get; set; }

        public static KeyboardState CurrKeyState { get; set; }
        public static KeyboardState NewKeyState { get; set; }

        public static GamePadState CurrGamePadState { get; set; }
        public static GamePadState NewGamePadState { get; set; }

        public static InputWrapper Input { get; set; }
        public static InputWrapper.DirectionalControlsWrapper.StickButtonState StickButtonState { get; set; }

        public static PenumbraComponent Penumbra { get; set; }

        public static Random Random { get; set; } = new Random();
    }
}
