using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TheGiverOnMars.Dictionaries;

namespace TheGiverOnMars.Components.PlacedObject
{
    public class CopperNode : PlacedObjectWithDrop
    {
        public CopperNode()
        {
            TileID = 25;
            Health = 4;
            Name = "Copper Node";
            ItemIdAndQuantity.Add((ItemDictionary.Dictionary[3], 2));
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
