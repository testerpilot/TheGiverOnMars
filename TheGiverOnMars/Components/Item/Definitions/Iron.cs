using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;

namespace TheGiverOnMars.Components.Item.Definitions
{
    public class IronOre : SellableItem
    {
        public IronOre()
        {
            // SellableItem Attributes
            BaseSellPrice = 20;
            BaseBuyPrice = 40;

            // BaseItem Attributes
            Name = "Iron Ore";
            TileId = 21;
            Attributes = null;
            IsStackable = true;
        }
    }

    public class IronBar : SellableItem
    {
        public IronBar()
        {
            // SellableItem Attributes
            BaseSellPrice = 200;
            BaseBuyPrice = 400;

            // BaseItem Attributes-
            Name = "Iron Bar";
            TileId = 22;
            Attributes = null;
            IsStackable = true;
        }
    }
}
