﻿using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.PlacedObject;
using TheGiverOnMars.Components.PlacedObject.Definitions;

namespace TheGiverOnMars.Dictionaries
{
    public static class PlacedObjectDictionary
    {
        public static Dictionary<int, PlacedObject> Dictionary = new Dictionary<int, PlacedObject>()
        {
            { 0, new Chest() },
            { 1, new CopperNode() }
        };
    }
}