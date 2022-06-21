using System;
using System.Collections.Generic;
using System.Text;
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
    }

    public class ItemInventorySpace : InventorySpace
    {
        public ItemInstance Item { get; set; }

        public ItemInventorySpace(ItemInstance item) : base(true)
        {
            Item = item;
        }
    }

    public class StackInventorySpace : InventorySpace
    {
        public ItemStack ItemStack { get; set; }

        public StackInventorySpace(ItemStack itemStack) : base(true)
        {
            ItemStack = itemStack;
        }
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
    }
}
