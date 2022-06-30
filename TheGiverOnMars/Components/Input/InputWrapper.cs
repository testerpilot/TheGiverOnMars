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
            public Buttons Button;

            public KeyWrapper(Keys key, Buttons button)
            {
                Key = key;
                Button = button;
            }

            public bool IsJustPressed() => (Constants.NewKeyState.IsKeyDown(Key) & !Constants.CurrKeyState.IsKeyDown(Key)) ||
                                           (Constants.NewGamePadState.IsButtonDown(Button) & !Constants.CurrGamePadState.IsButtonDown(Button));
            public bool IsDown() => Constants.NewKeyState.IsKeyDown(Key) || Constants.NewGamePadState.IsButtonDown(Button);
        }


        public class DirectionalControlsWrapper
        {
            public class StickButtonState
            {
                public bool Up, Down, Left, Right;
            };

            public KeyWrapper Up;
            public KeyWrapper Down;
            public KeyWrapper Left;
            public KeyWrapper Right;

            public int SlowInputTimer = 0;

            public bool InputUpIsDown() => Up.IsDown() || Constants.NewGamePadState.ThumbSticks.Left.Y > 0.1f;
            public bool InputDownIsDown() => Down.IsDown() || Constants.NewGamePadState.ThumbSticks.Left.Y < -0.1f;
            public bool InputLeftIsDown() => Left.IsDown() || Constants.NewGamePadState.ThumbSticks.Left.X < -0.1f;
            public bool InputRightIsDown() => Right.IsDown() || Constants.NewGamePadState.ThumbSticks.Left.X > 0.1f;

            public StickButtonState InputIsJustPressed()
            {
                bool timerProducesInput = false;

                bool stickInputUp = Constants.NewGamePadState.ThumbSticks.Left.Y > 0.1f;
                bool stickInputDown = Constants.NewGamePadState.ThumbSticks.Left.Y < -0.1f;
                bool stickInputLeft = Constants.NewGamePadState.ThumbSticks.Left.X < -0.1f;
                bool stickInputRight = Constants.NewGamePadState.ThumbSticks.Left.X > 0.1f;

                if (!(stickInputUp || stickInputDown || stickInputLeft || stickInputRight))
                {
                    SlowInputTimer = 0;

                    return new StickButtonState()
                    {
                        Up = false,
                        Down = false,
                        Left = false,
                        Right = false
                    };
                }
                if (SlowInputTimer == 0 || SlowInputTimer == 10)
                {
                    timerProducesInput = true;
                    SlowInputTimer += 1;
                }
                else if (SlowInputTimer == 20)
                {
                    SlowInputTimer = 0;
                }
                else
                {
                    SlowInputTimer += 1;
                }

                return new StickButtonState()
                { 
                    Up = Up.IsJustPressed() || (timerProducesInput && stickInputUp),
                    Down = Down.IsJustPressed() || (timerProducesInput && stickInputDown),
                    Left = Left.IsJustPressed() || (timerProducesInput && stickInputLeft),
                    Right = Right.IsJustPressed() || (timerProducesInput && stickInputRight)
                };
            }
        }

        public DirectionalControlsWrapper DirectionalControls;

        public KeyWrapper Interact;
        public KeyWrapper Use;
        public KeyWrapper ToggleInventory;
        public KeyWrapper ToggleCrafting;
        public KeyWrapper ShiftLeftInventory;
        public KeyWrapper ShiftRightInventory;
        public KeyWrapper Save;
    }
}
