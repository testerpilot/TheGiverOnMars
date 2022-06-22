using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.PlacedObject;
using TheGiverOnMars.Components.PlacedObject.Definitions;
using TheGiverOnMars.Dictionaries;
using TheGiverOnMars.Managers;

namespace TheGiverOnMars.Objects
{
    public class Map
    {
        public List<List<Tile>> TileMap = new List<List<Tile>>();
        public List<CollisionTile> CollisionRects = new List<CollisionTile>();
        public List<TransitionTile> TransitionTiles = new List<TransitionTile>();
        public List<PlacedObjectInstance> PlacedObjects = new List<PlacedObjectInstance>();
        public string Name { get; set; }

        public Map(MapDictionaryEntry mapData)
        {
            Name = mapData.MapName;

            for (int j = 0; j < mapData.Data.Count; j++)
            {
                var tileRow = mapData.Data[j];
                var tileRowTransform = new List<Tile>();

                for (int i = 0; i < tileRow.Length; i++)
                {
                    var position = new Vector2(i, j) * 64;

                    // Process transition 
                    if (tileRow[i] < 0)
                    {
                        var transitionEntry = mapData.TransitionEntries.FirstOrDefault(ex => ex.TransitionID == tileRow[i]);
                        var transitionTile = (TransitionTile) TileManager.GetTileFromID(transitionEntry.RenderAs).DeepCopy(isTransitionTile: true);
                        transitionTile.MapTransitionTo = transitionEntry.MapTransitionTo;
                        transitionTile.Position = position;
                        transitionTile.SpawnPointOnLoad = new Vector2(transitionEntry.SpawnPoint.X * 64, transitionEntry.SpawnPoint.Y * 64);
                        TransitionTiles.Add(transitionTile);
                        tileRowTransform.Add(transitionTile);
                        CollisionRects.Add(transitionTile);
                    }
                    else
                    { 
                        var tile = TileManager.GetTileFromID(tileRow[i]).DeepCopy();
                        tile.Position = position;

                        tileRowTransform.Add((Tile) tile);

                        if (tile.GetType() == typeof(CollisionTile))
                        {
                            CollisionRects.Add((CollisionTile)tile);
                        }
                    }
                }

                TileMap.Add(tileRowTransform);
            }

            // Load placed objects
            foreach (var placedObject in mapData.PlacedObjectEntries)
            {
                PlacedObjectInstance instance;

                if (placedObject.GetType() == typeof(MapDictionaryEntry.ChestObjectEntry))
                {
                    var chestData = (MapDictionaryEntry.ChestObjectEntry)placedObject;

                    if (chestData.Data == null)
                    {
                        instance = new PlacedObjectInstance(new Chest(new Inventory(chestData.JsonData).Spaces.ToList()));
                    }
                    else
                    {
                        instance = new PlacedObjectInstance(new Chest(chestData.Data));
                    }

                    instance.Tile.Position = placedObject.Position * 64;

                    PlacedObjects.Add(instance);

                    if (instance.Tile.GetType() == typeof(CollisionTile))
                    {
                        CollisionRects.Add((CollisionTile)instance.Tile);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tileRow in TileMap)
            {
                foreach (var tile in tileRow)
                {
                    tile.Draw(spriteBatch);
                }
            }

            foreach (var placedObject in PlacedObjects)
            {
                placedObject.Tile.Draw(spriteBatch);
            }
        }
    }
}
