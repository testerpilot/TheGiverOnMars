using System;
using System.Collections.Generic;
using System.Text;

namespace TheGiverOnMars.Components.PlacedObject
{
    public class CopperNode : PlacedObjectWithDrop
    {
        public CopperNode()
        {
            TileID = 25;
            Name = "Copper Node";
            ItemIDOnDrop = 3;
            NumItem = 2;
        }
    }
}
