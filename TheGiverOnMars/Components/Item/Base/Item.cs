using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Components.Item.Base
{
    /// <summary>
    /// Class for base item data
    /// </summary>
    public class Item
    {
        public string Name { get; set; }
        public int TileId { get; set; }
        public List<ItemAttribute> Attributes { get; set; }
        public bool IsStackable { get; set; }
    }

    /// <summary>
    /// Class for item that is buyable/sellable
    /// </summary>
    public class SellableItem : Item
    {
        public int BaseSellPrice { get; set; }
        public int BaseBuyPrice { get; set; }
    }

    /// <summary>
    /// Class for item that can be used for some action (such as a key, sword, or potion)
    /// </summary>
    public abstract class ActionItem : Item
    {
        public abstract void OnUse();
    }

    /// <summary>
    /// Class for item that is consumed on use (hence inheriting ActionItem)
    /// </summary>
    public abstract class ConsumableItem : ActionItem
    {
        public int BaseNumberOfUses { get; set; }
        public int CurrentNumberOfUses { get; set; }
        public abstract void OnDispose();
    }

    /// <summary>
    /// Instance of the item, that can resolve attributes to player
    /// </summary>
    public class ItemInstance
    {
        public Item Item { get; set; }

        private SpriteTile InventorySpritePrivate;

        [JsonIgnore]
        public SpriteTile InventorySprite
        {
            get
            {
                if (InventorySpritePrivate == null)
                {
                    InventorySpritePrivate = TileManager.GetTileFromID(Item.TileId).DeepCopy(isStaticTile: true);
                }

                return InventorySpritePrivate;
            }
            set
            {
                InventorySpritePrivate = value;
            }
        }

        public ItemInstance(Item item)
        {
            Item = item;
            InventorySpritePrivate = TileManager.GetTileFromID(item.TileId).DeepCopy(isStaticTile: true);
        }

        private ItemInstance()
        { 
        }
    }

    public class ItemStack
    {
        public ItemInstance Item { get; set; }
        public int Count { get; set; }

        public ItemStack(ItemInstance item, int count)
        {
            Item = item;
            Count = count;
        }

        public ItemStack(Item item, int count)
        {
            Item = new ItemInstance(item);
            Count = count;
        }

        private ItemStack()
        { 
        }

        public void Dispose()
        { 
        
        }

        public bool IsStackable(ItemStack stack) =>
            Item.GetType() == stack.Item.GetType();

        public bool IsUnstackable(Item item, int count)
        {
            if (Item.Item.GetType() == item.GetType() && count <= Count)
            {
                return true;
            }

            return false;
        }

        public void Stack(ItemStack stack)
        {
            if (IsStackable(stack))
            {
                Count += stack.Count;
                stack.Dispose();
            }
        }

        public void Stack(ItemStack stack, int count)
        {
            if (IsStackable(stack))
            {
                if (count <= stack.Count)
                {
                    Count += count;
                    stack.Count -= count;

                    if (stack.Count == 0)
                    {
                        stack.Dispose();
                    }
                }
                else
                {
                    Count += stack.Count;
                    stack.Dispose();
                }
            }
        }

        public ItemStack Unstack(Item item, int count)
        {
            if (IsUnstackable(item, count))
            {
                Count -= count;

                return new ItemStack(item, count);
            }

            return null;
        }
    }
}
