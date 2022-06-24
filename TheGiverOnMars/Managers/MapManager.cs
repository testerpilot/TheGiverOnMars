using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using TheGiverOnMars.Components;
using TheGiverOnMars.Components.PlacedObject;
using TheGiverOnMars.Components.PlacedObject.Definitions;
using TheGiverOnMars.Dictionaries;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Managers
{
    public static class MapManager
    { 
        public static Map CurrentMap;
        public static List<MapDictionaryEntry> TempMapDictionary = new List<MapDictionaryEntry>();

        public static void LoadMap(string name)
        {
            var index = TempMapDictionary.FindIndex(x => x.MapName.Equals(name));

            if (index != -1)
            {
                CurrentMap = new Map(TempMapDictionary[index]);
            }
            else
            {
                var entry = MapDictionary.Entries.FirstOrDefault(map => map.MapName.Equals(name));
                TempMapDictionary.Add(entry);
                CurrentMap = new Map(entry);
            }
        }

        public static void UpdateTempMap()
        {
            var index = TempMapDictionary.FindIndex(x => x.MapName.Equals(CurrentMap.Name));

            if (index != -1)
            {
                TempMapDictionary[index].PlacedObjectEntries = new List<MapDictionaryEntry.PlacedObjectEntry>();
            }
            else
            {
                var entry = MapDictionary.Entries.FirstOrDefault(map => map.MapName.Equals(CurrentMap.Name));
                TempMapDictionary.Add(entry);
                index = TempMapDictionary.FindIndex(x => x.MapName.Equals(CurrentMap.Name));
            }

            foreach (var instance in CurrentMap.PlacedObjects)
            {
                if (instance.PlacedObject.GetType() == typeof(Chest))
                {
                    TempMapDictionary[index].PlacedObjectEntries.Add(new MapDictionaryEntry.ChestObjectEntry(instance.Tile.Position / 64, instance.PlacedObject as Chest));
                }
                else
                {
                    TempMapDictionary[index].PlacedObjectEntries.Add(new MapDictionaryEntry.PlacedObjectEntry()
                    {
                        Position = instance.Tile.Position / 64,
                        ObjectId = PlacedObjectDictionary.Dictionary.FirstOrDefault(x => x.Value.Name == instance.PlacedObject.Name).Key
                    });
                }
            }
        }

        public static MapSaveData GetTempMapSaves()
        {
            UpdateTempMap();

            var transformedData = new MapSaveData()
            {
                CurrentMap = CurrentMap.Name,
                MapSaves = TempMapDictionary.Select(x => new MapEntrySaveData()
                {
                    Name = x.MapName,
                    ChestEntries = x.PlacedObjectEntries.Where(x => x.GetType().Equals(typeof(MapDictionaryEntry.ChestObjectEntry))).Select(x => x as MapDictionaryEntry.ChestObjectEntry).ToList(),
                    ObjectEntries = x.PlacedObjectEntries.Where(x => !x.GetType().Equals(typeof(MapDictionaryEntry.ChestObjectEntry))).ToList()
                }).ToList()
            };

            return transformedData;
        }

        public static void LoadSave(MapSaveData save)
        {
            foreach (var mapSave in save.MapSaves)
            {
                var entry = GetMapEntry(mapSave.Name);
                entry.PlacedObjectEntries = mapSave.ChestEntries.Select(x => x as MapDictionaryEntry.PlacedObjectEntry).ToList();
                entry.PlacedObjectEntries.AddRange(mapSave.ObjectEntries);
                TempMapDictionary.Add(entry);
            }

            LoadMap(save.CurrentMap);
        }

        //public static void LoadTempMapSaves(List<>)

        /*public static void LoadMap(Map.MapSaveData mapData)
        {
            var map = GetMap(mapData.Name);
        }*/

        private static MapDictionaryEntry GetMapEntry(string name) => MapDictionary.Entries.FirstOrDefault(map => map.MapName.Equals(name));

        private static Map GetMap(string name) => new Map(MapDictionary.Entries.FirstOrDefault(map => map.MapName.Equals(name)));
    }
}
