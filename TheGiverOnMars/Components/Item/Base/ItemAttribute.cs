using System;
using System.Collections.Generic;
using System.Text;

namespace TheGiverOnMars.Components.Item.Base
{
    /// <summary>
    /// Class for base attributes
    /// </summary>
    public class ItemAttribute
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Strength { get; set; }

        public ItemAttribute(string id, string name, int strength)
        {
            Id = id;
            Name = name;
            Strength = strength;
        }
    }
}
