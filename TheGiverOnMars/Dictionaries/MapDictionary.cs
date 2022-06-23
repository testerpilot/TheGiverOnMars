using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.Item.Definitions;
using TheGiverOnMars.Components.PlacedObject;
using TheGiverOnMars.Components.PlacedObject.Definitions;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Dictionaries
{
    public class MapDictionaryEntry
    {
        public class MapTransitionEntry
        {
            public int TransitionID;
            public string MapTransitionTo;
            public int RenderAs;
            public Vector2 SpawnPoint;
        }

        public class PlacedObjectEntry
        {
            public float PosX { get; set; }
            public float PosY { get; set; }
            public int ObjectId { get; set; }

            [JsonIgnore]
            public Vector2 Position
            {
                get
                {
                    return new Vector2(PosX, PosY);
                }
                set
                {
                    PosX = value.X;
                    PosY = value.Y;
                }
            }

            public PlacedObjectEntry()
            {
            }
        }

        public class ChestObjectEntry : PlacedObjectEntry
        {
            private List<InventorySpace> DataPrivate { get; set; }

            [JsonIgnore]
            public List<InventorySpace> Data
            {
                get => DataPrivate;
                set
                {
                    DataPrivate = value;
                    JsonData = DataPrivate.Select(x => x.Serialize()).ToList();
                }
            }

            public List<string> JsonData { get; set; }

            public ChestObjectEntry()
            {
                ObjectId = 0;
            }

            public ChestObjectEntry(Vector2 position, Chest chest)
            {
                ObjectId = 0;
                Data = chest.Inventory.Spaces.ToList();
                Position = position;
            }
        }

        public string MapName { get; set; }
        public List<int[]> Data { get; set; } = new List<int[]>();
        public List<MapTransitionEntry> TransitionEntries { get; set; } = new List<MapTransitionEntry>();
        public List<PlacedObjectEntry> PlacedObjectEntries { get; set; } = new List<PlacedObjectEntry>();

        public MapDictionaryEntry()
        { 
        }
    }

    public static class MapDictionary
    {
        public static List<MapDictionaryEntry> Entries = new List<MapDictionaryEntry>()
        {
            new MapDictionaryEntry()
            {
                MapName = "test",
                Data =  new List<int[]>()
                 {
                     new int[] { 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8 },
                     new int[] { 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5 },
                     new int[] { 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6 },
                     new int[] { 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7 },
                     new int[] { 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8 },
                     new int[] { 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5 },
                     new int[] { 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6 },
                     new int[] { 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7 },
                     new int[] { 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, -1, 8 },
                     new int[] { 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5 },
                     new int[] { 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6 },
                     new int[] { 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7 },
                     new int[] { 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8 },
                     new int[] { 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5 },
                     new int[] { 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6 },
                     new int[] { 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7, 8, 5, 6, 7 },
                 },
                TransitionEntries = new List<MapDictionaryEntry.MapTransitionEntry>()
                {
                    new MapDictionaryEntry.MapTransitionEntry()
                    {
                        TransitionID = -1,
                        MapTransitionTo = "test2",
                        RenderAs = 3,
                        SpawnPoint = new Vector2(2, 2)
                    }
                },
                PlacedObjectEntries = new List<MapDictionaryEntry.PlacedObjectEntry>()
                {
                    new MapDictionaryEntry.ChestObjectEntry()
                    {
                        Position = new Vector2(3, 5),
                        Data = new List<InventorySpace>()
                        {
                            new StackInventorySpace(new ItemStack(new IronOre(), 5)),
                            new ItemInventorySpace(new ItemInstance(new Pickaxe()))
                        }
                    },
                    new MapDictionaryEntry.ChestObjectEntry()
                    {
                        Position = new Vector2(4, 4),
                        Data = new List<InventorySpace>()
                        {
                            new StackInventorySpace(new ItemStack(new IronBar(), 5))
                        }
                    },
                    new MapDictionaryEntry.PlacedObjectEntry()
                    {
                        Position = new Vector2(8, 4),
                        ObjectId = 1
                    },                    
                    new MapDictionaryEntry.PlacedObjectEntry()
                    {
                        Position = new Vector2(10, 4),
                        ObjectId = 1
                    },
                    new MapDictionaryEntry.PlacedObjectEntry()
                    {
                        Position = new Vector2(8, 6),
                        ObjectId = 1
                    }
                }
            },
            new MapDictionaryEntry()
            {
                MapName = "test2",
                Data =  new List<int[]>()
                 {
                     new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 1, 3, 0, 0, 0, 0, 0, -1, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 3, 1, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                 },
                TransitionEntries = new List<MapDictionaryEntry.MapTransitionEntry>()
                {
                    new MapDictionaryEntry.MapTransitionEntry()
                    {
                        TransitionID = -1,
                        MapTransitionTo = "test3",
                        RenderAs = 3,
                        SpawnPoint = new Vector2(4, 13)
                    }
                }
            },
            new MapDictionaryEntry()
            {
                MapName = "test3",
                Data =  new List<int[]>()
                 {
                     new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, -1, 9, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 10, 3, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 3, 10, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 9, -2, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10 },
                     new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }
                 },
                TransitionEntries = new List<MapDictionaryEntry.MapTransitionEntry>()
                {
                    new MapDictionaryEntry.MapTransitionEntry()
                    {
                        TransitionID = -1,
                        MapTransitionTo = "test",
                        RenderAs = 3,
                        SpawnPoint = new Vector2(2, 2)
                    },
                    new MapDictionaryEntry.MapTransitionEntry()
                    {
                        TransitionID = -2,
                        MapTransitionTo = "test2",
                        RenderAs = 3,
                        SpawnPoint = new Vector2(2, 2)
                    }
                }
            }
        };

        public static Dictionary<string, List<int[]>> Dictionary = new Dictionary<string, List<int[]>>()
        {
            {
                 "test",
                 new List<int[]>()
                 {
                     new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 2, 3, 0, 0, 0, 0, 0, 4, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 2, 3, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                     new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                 }
            }
        };
    }
}
