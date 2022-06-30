using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TheGiverOnMars.Dictionaries;

namespace TheGiverOnMars.Components.PlacedObject.Definitions
{
    public class GoldNode : PlacedObjectWithDrop
    {
        public GoldNode()
        {
            TileID = 37;
            Health = 6;
            Name = "Iron Node";
            ItemIdAndQuantity.Add((ItemDictionary.Dictionary["GoldOre"], 2));
            BreakableWith =
            new Dictionary<string, int>()
            {
                { "Pickaxe", 2 }
            };
        }

        public override string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
