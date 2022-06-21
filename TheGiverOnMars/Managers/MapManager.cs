using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGiverOnMars.Dictionaries;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Managers
{
    public static class MapManager
    {
        public static Map CurrentMap;

        public static void LoadMap(string name)
        {
            CurrentMap = GetMap(name);
        }

        private static Map GetMap(string name) => new Map(MapDictionary.Entries.FirstOrDefault(map => map.MapName.Equals(name)));
    }
}
