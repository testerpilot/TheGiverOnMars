using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;

namespace TheGiverOnMars.Components.Item.Definitions
{
    public class CopperOre : SellableItem
    {
        public CopperOre()
        {
            // SellableItem Attributes
            BaseSellPrice = 20;
            BaseBuyPrice = 40;

            // BaseItem Attributes
            Name = "Copper Ore";
            TileId = 23;
            Attributes = null;
            IsStackable = true;
        }
    }

    public class CopperBar : SellableItem
    {
        public CopperBar()
        {
            // SellableItem Attributes
            BaseSellPrice = 200;
            BaseBuyPrice = 400;

            // BaseItem Attributes
            TileId = 24;
            Attributes = null;
            IsStackable = true;
            Name = "Copper Bar";
        }
    }
}
