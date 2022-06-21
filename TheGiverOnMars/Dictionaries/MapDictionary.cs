﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.Item.Definitions;
using TheGiverOnMars.Components.PlacedObject;
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
            public Vector2 Position;
        }

        public class ChestObjectEntry : PlacedObjectEntry
        {
            public List<InventorySpace> Data;
        }

        public string MapName;
        public List<int[]> Data = new List<int[]>();
        public List<MapTransitionEntry> TransitionEntries = new List<MapTransitionEntry>();
        public List<PlacedObjectEntry> PlacedObjectEntries = new List<PlacedObjectEntry>();

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
                            new StackInventorySpace(new ItemStack(new IronOre(), 5))
                        }
                    },
                    new MapDictionaryEntry.ChestObjectEntry()
                    {
                        Position = new Vector2(4, 4),
                        Data = new List<InventorySpace>()
                        {
                            new StackInventorySpace(new ItemStack(new IronBar(), 5))
                        }
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