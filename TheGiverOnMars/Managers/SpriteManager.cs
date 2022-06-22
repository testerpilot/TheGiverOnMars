using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using TheGiverOnMars.Dictionaries;

namespace TheGiverOnMars.Managers
{
    public static class SpriteManager
    {
        public static Dictionary<int, Texture2D> SpriteDict = new Dictionary<int, Texture2D>();

        public static void LoadSprites()
        {
            foreach (var keyValuePair in SpriteNameDictionary.Dictionary)
            {
                SpriteDict.Add(keyValuePair.Key, Constants.Content.Load<Texture2D>(keyValuePair.Value));
            }
        }

        public static Texture2D GetSpriteFromDict(int id) => SpriteDict.GetValueOrDefault(id);
    }
}
