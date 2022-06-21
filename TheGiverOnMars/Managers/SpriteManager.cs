using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using TheGiverOnMars.Dictionaries;

namespace TheGiverOnMars.Managers
{
    public class SpriteManager
    {
        public Dictionary<int, Texture2D> SpriteDict = new Dictionary<int, Texture2D>();

        public SpriteManager(ContentManager content)
        {
            foreach (var keyValuePair in SpriteNameDictionary.Dictionary)
            {
                SpriteDict.Add(keyValuePair.Key, content.Load<Texture2D>(keyValuePair.Value));
            }
        }

        public Texture2D GetSpriteFromDict(int id) => SpriteDict.GetValueOrDefault(id);
    }
}
