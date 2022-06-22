using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Components.Item.Base
{
    public class InventorySpace
    {
        public bool HasValue { get; set; }

        public InventorySpace(bool hasValue)
        {
            HasValue = hasValue;
        }

        protected InventorySpace()
        { 
        }

        [JsonIgnore]
        public SpriteTile SpriteTile
        {
            get
            {
                if (GetType() == typeof(ItemInventorySpace))
                {
                    return ((ItemInventorySpace)this).Item.InventorySprite;
                }
                else
                {
                    return ((StackInventorySpace)this).ItemStack.Item.InventorySprite;
                }
            }
        }

        [JsonIgnore]
        public Item ItemInterfaced
        {
            get
            {
                if (GetType() == typeof(ItemInventorySpace))
                {
                    return ((ItemInventorySpace)this).Item.Item;
                }
                else
                {
                    return ((StackInventorySpace)this).ItemStack.Item.Item;
                }
            }
        }

        public InventorySpace DeepCopy()
        {
            if (!HasValue)
            {
                return new InventorySpace(false);
            }
            else if (GetType() == typeof(ItemInventorySpace))
            {
                return new ItemInventorySpace(new ItemInstance(ItemInterfaced));
            }
            else
            {
                return new StackInventorySpace(new ItemStack(ItemInterfaced, ((StackInventorySpace)this).ItemStack.Count));
            }
        }

        public virtual string Serialize() => JsonSerializer.Serialize(this);
    }

    public class ItemInventorySpace : InventorySpace
    {
        public ItemInstance Item { get; set; }

        public ItemInventorySpace(ItemInstance item) : base(true)
        {
            Item = item;
        }

        private ItemInventorySpace() : base()
        { 
        }

        public override string Serialize() => JsonSerializer.Serialize(this);
    }

    public class StackInventorySpace : InventorySpace
    {
        public ItemStack ItemStack { get; set; }

        public StackInventorySpace(ItemStack itemStack) : base(true)
        {
            ItemStack = itemStack;
        }

        private StackInventorySpace() : base()
        { 
        }

        public override string Serialize() => JsonSerializer.Serialize(this);
    }

    public class Inventory
    {
        public const int Size = 30;
        public InventorySpace[] Spaces { get; set; }

        public Inventory()
        {
            Spaces = new InventorySpace[30];

            for (int i = 0; i < 30; i++)
            {
                Spaces[i] = new InventorySpace(false);
            }
        }

        public Inventory(List<InventorySpace> inventorySpaces)
        {
            Spaces = new InventorySpace[30];

            for (int i = 0; i < inventorySpaces.Count; i++)
            {
                Spaces[i] = inventorySpaces[i];
            }

            for (int i = inventorySpaces.Count; i < 30; i++)
            {
                Spaces[i] = new InventorySpace(false);
            }
        }

        public Inventory(List<string> jsonSpaces)
        {
            Spaces = new InventorySpace[30];

            for (int i = 0; i < jsonSpaces.Count; i++)
            {
                if (jsonSpaces[i].Contains("ItemStack"))
                {
                    Spaces[i] = JsonSerializer.Deserialize<StackInventorySpace>(jsonSpaces[i]);
                }
                else
                {
                    Spaces[i] = new InventorySpace(false);
                }
            }

            for (int i = jsonSpaces.Count; i < 30; i++)
            {
                Spaces[i] = new InventorySpace(false);
            }
        }

        public List<string> Save() => Spaces.Select(x => x.Serialize()).ToList();
    }
}
