﻿using Microsoft.Xna.Framework;
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

namespace TheGiverOnMars.Components.Map
{
    public class Map
    {
        public List<List<Tile>> TileMap = new List<List<Tile>>();
        public List<CollisionTile> CollisionRects = new List<CollisionTile>();
        public List<TransitionTile> TransitionTiles = new List<TransitionTile>();
        public List<PlacedObjectInstance> PlacedObjects = new List<PlacedObjectInstance>();
        public List<DroppedItemInstance> DroppedItems = new List<DroppedItemInstance>();

        public PlacedObjectInstance PlacedObjectForRemoval = null;
        public List<DroppedItemInstance> ItemsForRemoval = new List<DroppedItemInstance>();

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
                }
                else
                {
                    instance = new PlacedObjectInstance(PlacedObjectDictionary.Dictionary[placedObject.ObjectId].DeepCopy());

                    if (placedObject.Data_ != null)
                    {
                        instance.PlacedObject.Parse(placedObject.Data_);
                    }
                }

                instance.Tile.Position = placedObject.Position * 64;

                PlacedObjects.Add(instance);

                if (instance.Tile.GetType() == typeof(CollisionTile))
                {
                    CollisionRects.Add((CollisionTile)instance.Tile);
                }
            }
        }

        public void Spawn(BaseItem item, Vector2 position, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                DroppedItems.Add(new DroppedItemInstance(new ItemInstance(item), position));
            }
        }

        public void Spawn(int placedObjectId, Vector2 position)
        {
            var instance = new PlacedObjectInstance(PlacedObjectDictionary.Dictionary[placedObjectId]);
            instance.Tile.PositionOnMap = position;

            PlacedObjects.Add(instance);
            CollisionRects.Add((CollisionTile)instance.Tile);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var placedObject in PlacedObjects)
            {
                placedObject.Update(gameTime);
            }

            if (PlacedObjectForRemoval != null)
            {
                PlacedObjects.Remove(PlacedObjectForRemoval);
                PlacedObjectForRemoval = null;
            }

            foreach (var item in ItemsForRemoval)
            {
                DroppedItems.Remove(item);
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

            foreach (var droppedItem in DroppedItems)
            {
                droppedItem.Instance.InventorySprite.Draw(spriteBatch, Color.White);
            }
        }
    }
}
