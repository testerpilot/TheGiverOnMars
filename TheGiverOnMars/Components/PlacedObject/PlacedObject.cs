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

    public class PlacedObjectInstance
    {
        public PlacedObject PlacedObject;
        public SpriteTile Tile;

        public PlacedObjectInstance(PlacedObject placedObject)
        {
            PlacedObject = placedObject;
            Tile = TileManager.GetTileFromID(placedObject.TileID).DeepCopy();
        }
    }

    public abstract class InteractablePlacedObject : PlacedObject
    {
        public abstract void Interact(Player player);
    }
}
