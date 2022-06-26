using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;

namespace TheGiverOnMars.Components.Item.Definitions
{
    public class FurnaceItem : PlaceableItem
    {
        public FurnaceItem()
        {
            // Placeableitem Attributes
            PlacedObjecttId = 2;

            // BaseItem Attributes
            Name = "Furnace";
            TileId = 28;
            Attributes = null;
            IsStackable = true;
        }
    }
}
