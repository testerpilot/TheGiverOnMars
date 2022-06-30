using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGiverOnMars.Dictionaries;

namespace TheGiverOnMars.Components.Map
{
    public class RandomWalker
    {
        private enum Direction { Up, Down, Left, Right }

        private Point LastCoordinate { get; set; }
        private Point CurrentCoordinate { get; set; }
        private Random Random { get; } = new Random();

        public RandomWalker(Point startingPoint)
        {
            LastCoordinate = new Point(-1, -1);
            CurrentCoordinate = new Point(0, 0);
        }

        public void Walk(ref List<int[]> map, ref List<MapDictionaryEntry.PlacedObjectEntry> placedObjects)
        {
            while (true)
            {
                var nextCoordinate = CurrentCoordinate;
                Direction nextDirection = (Direction)Random.Next(4);

                if (nextDirection == Direction.Up)
                {
                    nextCoordinate.Y -= 1;
                }
                else if (nextDirection == Direction.Down)
                {
                    nextCoordinate.Y += 1;
                }
                else if (nextDirection == Direction.Left)
                {
                    nextCoordinate.X -= 1;
                }
                else if (nextDirection == Direction.Right)
                {
                    nextCoordinate.X += 1;
                }

                if (nextCoordinate.Y >= 0 && nextCoordinate.Y < map.Count &&
                    nextCoordinate.X >= 0 && nextCoordinate.X < map.FirstOrDefault().Length &&
                    LastCoordinate != nextCoordinate)
                {
                    map[CurrentCoordinate.Y][CurrentCoordinate.X] = 5;
                    RandomlyGeneratePlacedObject(ref placedObjects, CurrentCoordinate.ToVector2());

                    LastCoordinate = CurrentCoordinate;
                    CurrentCoordinate = nextCoordinate;
                    break;
                }
            }
        }

        private void RandomlyGeneratePlacedObject(ref List<MapDictionaryEntry.PlacedObjectEntry> placedObjects, Vector2 position)
        {
            if ((position.X == 0 && position.Y == 0) || placedObjects.Any(p => p.Position == position))
            {
                return;
            }

            var randomNumber = Constants.Random.Next(1001);
            var placedObject = new MapDictionaryEntry.PlacedObjectEntry()
            {
                Position = position
            };

            if (randomNumber < 50)
            {
                placedObject.ObjectId = 1;
            }
            else if (randomNumber < 75)
            {
                placedObject.ObjectId = 3;
            }
            else if (randomNumber < 100)
            {
                placedObject.ObjectId = 5;
            }
            else if (randomNumber < 105)
            {
                placedObject.ObjectId = 4;
            }
            else
            {
                return;
            }

            placedObjects.Add(placedObject);
        }
    }
}
