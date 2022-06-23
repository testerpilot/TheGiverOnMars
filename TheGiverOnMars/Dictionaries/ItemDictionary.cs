using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.Item.Definitions;

namespace TheGiverOnMars.Dictionaries
{
    public static class ItemDictionary
    {
        public static Dictionary<int, Item> Dictionary = new Dictionary<int, Item>()
        {
            { 1, new IronBar() },
            { 2, new CopperBar() },
            { 3, new CopperOre() },
            { 4, new Pickaxe() }
        };
    }
}
