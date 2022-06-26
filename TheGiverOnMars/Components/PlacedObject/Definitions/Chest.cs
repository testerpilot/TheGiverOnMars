using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Objects;
using BaseItem = TheGiverOnMars.Components.Item.Base;

namespace TheGiverOnMars.Components.PlacedObject.Definitions
{
    public class Chest : InteractablePlacedObjectWithDrop
    {
        public Inventory Inventory;
        public int _health = 4;

        public Chest()
        {
            Name = "Chest";
            TileID = 26;

            Inventory = new Inventory();
        }

        public Chest(List<InventorySpace> inventorySpaces)
        {
            Name = "Chest";
            TileID = 26;

            Inventory = new Inventory(inventorySpaces);
        }

        public override void Interact(Player player)
        {
            player.InventoryManager.InventoryInteractingWith = Inventory;
            player.InventoryManager.IsInventoryOpen = true;
        }

        public override List<(BaseItem.Item, int)> ItemIdAndQuantityOnDrop()
        {
            List<(BaseItem.Item, int)> tupleList = new List<(BaseItem.Item, int)>();

            // TODO: ADD CHEST OBJECT

            foreach (var space in Inventory.Spaces)
            {
                if (space.HasValue)
                {
                    if (space.ItemInterfaced.IsStackable)
                    {
                        tupleList.Add((space.ItemInterfaced, ((StackInventorySpace)space).ItemStack.Count));
                    }
                    else
                    {
                        tupleList.Add((space.ItemInterfaced, 1));
                    }
                }
            }

            return tupleList;
        }

        public override Dictionary<string, int> BreakableWith() =>
            new Dictionary<string, int>()
            {
                { "Pickaxe", 2 }
            };

        public override void SubtractHealth(int damage)
        {
            _health -= damage;
        }

        public override int Health() => _health;

        public override void Update(GameTime gameTime, SpriteTile tile)
        {
        }

        public override PlacedObject DeepCopy()
        {
            return new Chest();
        }
    }
}
