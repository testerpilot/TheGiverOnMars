using System;
using System.Collections.Generic;
using System.Text;

namespace TheGiverOnMars.Dictionaries
{
    public class TileDictEntry
    {
        public int TileID, SpriteID;
        public bool HasCollision;
        public string TileName;
        public bool IsStatic;

        public TileDictEntry(int tileID, int spriteID, bool hasCollision, string tileName, bool isStatic = false, Dictionary<string, string> attributes = null)
        {
            TileID = tileID;
            SpriteID = spriteID;
            HasCollision = hasCollision;
            TileName = tileName;
            IsStatic = isStatic;
        }
    };

    public static class TileNameDictionary
    {
        // (ID of Tile, ID of Sprite, Collision)
        public static List<TileDictEntry> Dictionary = new List<TileDictEntry>
        {

            new TileDictEntry(0, 1, false, "Grass"),
            new TileDictEntry(1, 1, true, "GrassCol"),
            new TileDictEntry(2, 0, false, "Dirt"),
            new TileDictEntry(3, 0, true, "DirtCol"),
            new TileDictEntry(4, 2, true, "Transition"),
            new TileDictEntry(5, 3, false, "MarsGound1"),
            new TileDictEntry(6, 4, false, "MarsGound2"),
            new TileDictEntry(7, 5, false, "MarsGound3"),
            new TileDictEntry(8, 6, false, "MarsGound4"),
            new TileDictEntry(9, 2, false, "Blue"),
            new TileDictEntry(10, 2, true, "BlueCol"),
            new TileDictEntry(20, 7, false, "InvOnMainGui", isStatic: true),
            new TileDictEntry(21, 10, false, "IronOre", isStatic: true),
            new TileDictEntry(22, 11, false, "IronBar", isStatic: true),
            new TileDictEntry(23, 12, false, "CopperOre", isStatic: true),
            new TileDictEntry(24, 13, false, "CopperBar", isStatic: true),
            new TileDictEntry(25, 14, true, "CopperNode"),
            new TileDictEntry(26, 15, true, "Chest"),
            new TileDictEntry(27, 16, false, "Pickaxe")
        };
    }
}
