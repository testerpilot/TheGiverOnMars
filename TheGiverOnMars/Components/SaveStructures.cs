using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Dictionaries;

namespace TheGiverOnMars.Components
{
    public class MapEntrySaveData
    {
        public string Name { get; set; }
        public List<MapDictionaryEntry.ChestObjectEntry> ChestEntries { get; set; }
    }

    public class MapSaveData
    {
        public string CurrentMap { get; set; }
        public List<MapEntrySaveData> MapSaves { get; set; }
    }

    public class PlayerSaveData
    {
        public float PosX { get; set; }
        public float PosY { get; set; }
        public List<string> Inventory { get; set; }
    }

    public class GameStateSave
    {
        public PlayerSaveData PlayerData { get; set; }
        public MapSaveData MapData { get; set; }
    }
}

