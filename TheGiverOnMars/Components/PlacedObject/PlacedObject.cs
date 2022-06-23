using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Components.PlacedObject
{
    /// <summary>
    /// Class for interfacing with objects on the map
    /// </summary>
    public class PlacedObject
    {
        public int TileID;
        public string Name;
    }

    public class PlacedObjectWithDrop : PlacedObject
    {
        public int ItemIDOnDrop;
        public int NumItem;
    }

    public class PlacedObjectInstance
    {
        public PlacedObject PlacedObject;
        public SpriteTile Tile;

        public PlacedObjectInstance(PlacedObject placedObject)
        {
            PlacedObject = placedObject;
            Tile = TileManager.GetTileFromID(placedObject.TileID).DeepCopy();
        }

        public void Break()
        {
            MapManager.CurrentMap.PlacedObjectForRemoval = this;

            if (Tile.GetType().Equals(typeof(CollisionTile)))
            {
                MapManager.CurrentMap.CollisionRects.Remove((CollisionTile)Tile);
            }

            if (PlacedObject.GetType().IsSubclassOf(typeof(PlacedObjectWithDrop)))
            {
                var objectWithDrop = (PlacedObjectWithDrop)PlacedObject;
                MapManager.CurrentMap.Spawn(objectWithDrop.ItemIDOnDrop, Tile.Position, objectWithDrop.NumItem);
            }
        }
    }

    public abstract class InteractablePlacedObject : PlacedObject
    {
        public abstract void Interact(Player player);
    }
}
