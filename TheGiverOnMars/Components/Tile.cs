using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Utilities;

namespace TheGiverOnMars.Objects
{
    public class SpriteTile
    {
        public Texture2D _texture;
        public Vector2 Position;

        public int OffsetX = 0, OffsetY = 0;
        public int CollisionWidth = 64, CollisionHeight = 54;

        public Point Center
        {
            get
            {
                return new Point((int) Position.X + (_texture.Width / 2) + OffsetX, (int) Position.Y + (_texture.Height / 2) + OffsetY);
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Center.X - (CollisionWidth / 2), Center.Y - (CollisionHeight / 2), CollisionWidth, CollisionHeight);
            }
        }

        public SpriteTile(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(_texture, Position, color);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(_texture, position, color);
        }

        public bool IsInProximity(SpriteTile sprite)
        {
            var distance = FormulaHelper.DistanceBetweenTwoPoints(Rectangle.Center, sprite.Rectangle.Center);
            return distance < 80;
        }

        public SpriteTile DeepCopy(bool isTransitionTile = false, bool isStaticTile = false)
        {
            if (GetType() == typeof(CollisionTile))
            {
                if (isTransitionTile)
                {
                    return new TransitionTile(_texture)
                    {
                        Position = Position,
                        Velocity = new Vector2()
                    };
                }
                else
                {
                    return new CollisionTile(_texture)
                    {
                        Position = Position,
                        Velocity = new Vector2(),
                    };
                }
            }
            else
            {
                if (!isStaticTile)
                {
                    return new Tile(_texture)
                    {
                        Position = Position,
                        Velocity = new Vector2()
                    };
                }
                else
                {
                    return new SpriteTile(_texture)
                    {
                        Position = Position
                    };
                }
            }
        }
    }

    public class Tile : SpriteTile
    {
        public Vector2 Velocity;
        public float Speed;

        public Tile(Texture2D texture) : base(texture)
        { 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(_texture, Position, color);
        }
    }

    public class CollisionTile : Tile
    {
        public CollisionTile(Texture2D texture) : base(texture)
        {
        }

        public bool IsTouchingLeft(CollisionTile sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        public bool IsTouchingRight(CollisionTile sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        public bool IsTouchingTop(CollisionTile sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        public bool IsTouchingBottom(CollisionTile sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }
    }

    public class TransitionTile : CollisionTile
    {
        public string MapTransitionTo;
        public Vector2? SpawnPointOnLoad;

        public TransitionTile(Texture2D texture, Vector2? spawnPoint = null, string mapTransitionTo = null) : base(texture)
        {
            MapTransitionTo = mapTransitionTo;
            SpawnPointOnLoad = spawnPoint;
        }
    }
}
