using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Objects;
using BaseItem = TheGiverOnMars.Components.Item.Base;

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

    public class BreakablePlacedObject : PlacedObject
    {
        public List<string> BreakableWith = new List<string>();
    }

    public class PlacedObjectWithDrop : BreakablePlacedObject
    {
        public List<(BaseItem.Item, int)> ItemIdAndQuantity = new List<(BaseItem.Item, int)>();
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
            if (PlacedObject.GetType().IsSubclassOf(typeof(BreakablePlacedObject)) || PlacedObject.GetType().IsSubclassOf(typeof(BreakableInteractablePlacedObject)))
            {
                MapManager.CurrentMap.PlacedObjectForRemoval = this;

                if (Tile.GetType().Equals(typeof(CollisionTile)))
                {
                    MapManager.CurrentMap.CollisionRects.Remove((CollisionTile)Tile);
                }

                if (PlacedObject.GetType().IsSubclassOf(typeof(PlacedObjectWithDrop)))
                {
                    var objectWithDrop = (PlacedObjectWithDrop)PlacedObject;

                    foreach (var tuple in objectWithDrop.ItemIdAndQuantity)
                    {
                        MapManager.CurrentMap.Spawn(tuple.Item1, Tile.Position, tuple.Item2);
                    }
                }
                else if (PlacedObject.GetType().IsSubclassOf(typeof(InteractablePlacedObjectWithDrop)))
                {
                    var objectWithDrop = (InteractablePlacedObjectWithDrop)PlacedObject;

                    if (objectWithDrop.ItemIdAndQuantityOnDrop() != null)
                    {
                        foreach (var tuple in objectWithDrop.ItemIdAndQuantityOnDrop())
                        {
                            MapManager.CurrentMap.Spawn(tuple.Item1, Tile.Position, tuple.Item2);
                        }
                    }
                }
            }
        }
    }

    public abstract class InteractablePlacedObject : PlacedObject
    {
        public abstract void Interact(Player player);
    }

    public abstract class BreakableInteractablePlacedObject : InteractablePlacedObject
    {
        public abstract List<string> BreakableWith();
    }

    public abstract class InteractablePlacedObjectWithDrop : BreakableInteractablePlacedObject
    {
        public abstract List<(BaseItem.Item, int)> ItemIdAndQuantityOnDrop();
    }
}