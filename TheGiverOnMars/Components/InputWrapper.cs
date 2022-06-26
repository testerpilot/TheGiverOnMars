using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGiverOnMars.Components
{
    public class InputWrapper
    {
        public class KeyWrapper
        {
            public Keys Key;

            public KeyWrapper(Keys key)
            {
                Key = key;
            }

            public bool IsJustPressed() => Constants.NewKeyState.IsKeyDown(Key) & !Constants.CurrKeyState.IsKeyDown(Key);

            public bool IsDown() => Constants.NewKeyState.IsKeyDown(Key);
        }

        public KeyWrapper Up;
        public KeyWrapper Down;
        public KeyWrapper Left;
        public KeyWrapper Right;

        public KeyWrapper Interact;
        public KeyWrapper Use;
        public KeyWrapper ToggleInventory;
        public KeyWrapper ShiftLeftInventory;
        public KeyWrapper ShiftRightInventory;
        public KeyWrapper Save;
    }
}
