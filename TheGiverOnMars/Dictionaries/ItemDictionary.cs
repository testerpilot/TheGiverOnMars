using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.Item.Definitions;

namespace TheGiverOnMars.Dictionaries
{
    public static class ItemDictionary
    {
        public static Dictionary<string, BaseItem> Dictionary = new Dictionary<string, BaseItem>()
        {
            { "IronOre", new IronOre() },
            { "IronBar", new IronBar() },
            { "CopperBar", new CopperBar() },
            { "CopperOre", new CopperOre() },
            { "TinOre", new TinOre() },
            { "TinBar", new TinBar() },
            { "GoldOre", new GoldOre() },
            { "GoldBar", new GoldBar() },
            { "BronzeBar", new BronzeBar() },
            { "SteelBar", new SteelBar() },
            { "Pickaxe", new Pickaxe() }
        };
    }
}
