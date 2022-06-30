using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TheGiverOnMars.Dictionaries;

namespace TheGiverOnMars.Components.PlacedObject.Definitions
{
    public class TinNode : PlacedObjectWithDrop
    {
        public TinNode()
        {
            TileID = 36;
            Health = 4;
            Name = "Tin Node";
            ItemIdAndQuantity.Add((ItemDictionary.Dictionary["TinOre"], 2));
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
