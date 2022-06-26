using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;

namespace TheGiverOnMars.Components.Item.Definitions
{
    public class ChestItem : PlaceableItem
    {
        public ChestItem()
        {
            // Placeableitem Attributes
            PlacedObjecttId = 0;

            // BaseItem Attributes
            Name = "Chest";
            TileId = 26;
            Attributes = null;
            IsStackable = true;
        }
    }
}
