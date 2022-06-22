using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Dictionaries;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Managers
{
    public static class TileManager
    {
        public static Dictionary<int, SpriteTile> TileDict = new Dictionary<int, SpriteTile>();

        public static void LoadTiles()
        {
            foreach (var entry in TileNameDictionary.Dictionary)
            {
                if (entry.HasCollision)
                {
                    TileDict.Add(entry.TileID, new CollisionTile(SpriteManager.GetSpriteFromDict(entry.SpriteID)));
                }
                else if (!entry.IsStatic)
                {
                    TileDict.Add(entry.TileID, new Tile(SpriteManager.GetSpriteFromDict(entry.SpriteID)));
                }
                else
                {
                    TileDict.Add(entry.TileID, new SpriteTile(SpriteManager.GetSpriteFromDict(entry.SpriteID)));
                }
            }
        }

        public static SpriteTile GetTileFromID(int id) => TileDict.GetValueOrDefault(id);
    }
}
