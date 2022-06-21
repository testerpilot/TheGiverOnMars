using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Managers
{
    public enum State { None, LoadMap, LoadState, Fade, FadeReverse, Waiting, WaitingOnPlayerLoad, PlayerLoadDone };

    public class SceneManager
    {
        private Texture2D TransitionColor = null;
        private int ScreenWidth;
        private int ScreenHeight;
        private int FadeOpacity = 0;
        private int OpacityPerSecond = 255 / 30;

        public State CurrentState;
        public State CurrentSubstate;

        public string MapToLoad = null;

        public SceneManager(GraphicsDeviceManager graphics)
        {
            TransitionColor = new Texture2D(graphics.GraphicsDevice, 1, 1);
            TransitionColor.SetData(new Color[] { Color.Black });

            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;

            CurrentState = State.None;
            CurrentSubstate = State.None;
        }

        public void TransitionToMap(string map)
        {
            MapToLoad = map;
            CurrentState = State.LoadMap;
            CurrentSubstate = State.Fade;
        }

        public void Update(GameTime gameTime, SpriteBatch spriteBatch, Vector2 playerPosition)
        {
            if (CurrentSubstate == State.Fade)
            {
                FadeOpacity += 1 * OpacityPerSecond;

                if (FadeOpacity >= 255)
                {
                    CurrentSubstate = State.Waiting;
                }
            }

            if (CurrentSubstate == State.FadeReverse)
            {
                FadeOpacity -= 1 * OpacityPerSecond;

                if (FadeOpacity <= 0)
                {
                    CurrentSubstate = State.None;
                    CurrentState = State.None;
                }
            }

            if (CurrentSubstate == State.Waiting)
            {
                if (CurrentState == State.LoadMap && MapToLoad != null)
                {
                    MapManager.LoadMap(MapToLoad);
                    MapToLoad = null;
                    CurrentSubstate = State.WaitingOnPlayerLoad;
                }
            }

            if (CurrentSubstate == State.PlayerLoadDone)
            {
                CurrentSubstate = State.FadeReverse;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (CurrentSubstate != State.None)
            {
                float xPos = position.X - (ScreenWidth / 2);
                float yPos = position.Y - (ScreenHeight / 2);

                spriteBatch.Draw(TransitionColor, new Vector2(xPos, yPos), null, new Color(0, 0, 0, FadeOpacity), 0f, Vector2.Zero, new Vector2(ScreenWidth * 1.1f, ScreenHeight * 1.1f), SpriteEffects.None, 0);
            }
        }
    }
}
