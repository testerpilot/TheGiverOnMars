using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;

namespace TheGiverOnMars.Components.Item.Definitions
{
    public class GoldOre : SellableItem
    {
        public GoldOre()
        {
            // SellableItem Attributes
            BaseSellPrice = 20;
            BaseBuyPrice = 40;

            // BaseItem Attributes
            Name = "Gold Ore";
            TileId = 32;
            Attributes = null;
            IsStackable = true;
        }
    }

    public class GoldBar : SellableItem
    {
        public GoldBar()
        {
            // SellableItem Attributes
            BaseSellPrice = 200;
            BaseBuyPrice = 400;

            // BaseItem Attributes-
            Name = "Gold Bar";
            TileId = 33;
            Attributes = null;
            IsStackable = true;
        }
    }
}
