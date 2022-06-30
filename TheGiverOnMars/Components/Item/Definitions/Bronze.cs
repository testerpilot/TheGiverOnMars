using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;

namespace TheGiverOnMars.Components.Item.Definitions
{
    public class BronzeBar : SellableItem
    {
        public BronzeBar()
        {
            // SellableItem Attributes
            BaseSellPrice = 200;
            BaseBuyPrice = 400;

            // BaseItem Attributes-
            Name = "Bronze Bar";
            TileId = 34;
            Attributes = null;
            IsStackable = true;
        }
    }
}
