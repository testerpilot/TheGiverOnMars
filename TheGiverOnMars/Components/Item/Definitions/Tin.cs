using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;

namespace TheGiverOnMars.Components.Item.Definitions
{
    public class TinOre : SellableItem
    {
        public TinOre()
        {
            // SellableItem Attributes
            BaseSellPrice = 20;
            BaseBuyPrice = 40;

            // BaseItem Attributes
            Name = "Tin Ore";
            TileId = 30;
            Attributes = null;
            IsStackable = true;
        }
    }

    public class TinBar : SellableItem
    {
        public TinBar()
        {
            // SellableItem Attributes
            BaseSellPrice = 200;
            BaseBuyPrice = 400;

            // BaseItem Attributes-
            Name = "Tin Bar";
            TileId = 31;
            Attributes = null;
            IsStackable = true;
        }
    }
}
