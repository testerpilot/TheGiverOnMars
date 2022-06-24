using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Objects;

using Items = TheGiverOnMars.Components.Item.Definitions;

namespace TheGiverOnMars.Components.PlacedObject.Definitions
{
    public class Furnace : InteractablePlacedObjectWithDrop
    {
        List<SimpleContract> Contracts = new List<SimpleContract>()
        {
            new SimpleContract()
            { 
                Requirement = (new Items.CopperOre(), 3),
                Promise = (new Items.CopperBar(), 1)
            }
        };

        public Furnace()
        {
            Name = "Furnace";
            TileID = 28;
        }

        public override List<string> BreakableWith() =>
            new List<string>()
            {
                "Pickaxe"
            };

        public override void Interact(Player player)
        {
            var selectedSpace = player.InventoryManager.Inventory.Spaces[player.InventoryManager.SelectedTile];

            if (selectedSpace.HasValue)
            {
                var appliedContract = Contracts.FirstOrDefault(x => x.Requirement.Item1.Name.Equals(selectedSpace.ItemInterfaced.Name));

                if (appliedContract == null)
                {
                    return;
                }

                if (selectedSpace.ItemInterfaced.IsStackable)
                {
                    var stack = ((StackInventorySpace)selectedSpace).ItemStack;

                    if (stack.Count < appliedContract.Requirement.Item2)
                    {
                        return;
                    }

                    stack.Count -= appliedContract.Requirement.Item2;

                    if (stack.Count > 0)
                    {
                        player.InventoryManager.Inventory.Spaces[player.InventoryManager.SelectedTile] = new StackInventorySpace(stack);
                    }
                    else
                    {
                        player.InventoryManager.Inventory.Spaces[player.InventoryManager.SelectedTile] = new InventorySpace(false);
                    }
                }
                else
                {
                    player.InventoryManager.Inventory.Spaces[player.InventoryManager.SelectedTile] = new InventorySpace(false);
                }

                MapManager.CurrentMap.Spawn(appliedContract.Promise.Item1, player.Tile.Position, appliedContract.Promise.Item2);
            }
        }

        public override List<(Item.Base.Item, int)> ItemIdAndQuantityOnDrop()
        {
            return null;        
        }
    }
}
