using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;

namespace TheGiverOnMars.Components.Item.Definitions
{
    public class SteelBar : SellableItem
    {
        public SteelBar()
        {
            // SellableItem Attributes
            BaseSellPrice = 200;
            BaseBuyPrice = 400;

            // BaseItem Attributes-
            Name = "Steel Bar";
            TileId = 35;
            Attributes = null;
            IsStackable = true;
        }
    }
}
