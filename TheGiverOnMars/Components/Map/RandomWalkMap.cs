using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Dictionaries;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Components.Map
{ 
    public static class RandomWalkMap
    {
        public static MapDictionaryEntry GenerateMap(Point maxMapSize, int numWalkers = 1, int iterations = 50)
        {
            // Initialize base map
            var map = new List<int[]>();

            // Initialize generated placedObjects
            var placedObjects = new List<MapDictionaryEntry.PlacedObjectEntry>();

            for (int j = 0; j < maxMapSize.Y; j++)
            {
                var array = new int[maxMapSize.X];
                Array.Fill(array, 29);

                map.Add(array);
            }

            // Calculate random starting point
            var startingPoint = new Point(Constants.Random.Next(maxMapSize.X), Constants.Random.Next(maxMapSize.Y));

            // Initialize walkers
            var walkers = new List<RandomWalker>();

            for (int i = 0; i < numWalkers; i++)
            {
                walkers.Add(new RandomWalker(startingPoint));
            }

            // Iterate walkers
            for (int i = 0; i < iterations; i++)
            {
                foreach (var walker in walkers)
                {
                    walker.Walk(ref map, ref placedObjects);
                }
            }

            return new MapDictionaryEntry()
            {
                MapName = "temp",
                Data = map,
                PlacedObjectEntries = placedObjects
            };
        }
    }
}
 