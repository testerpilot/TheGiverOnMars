using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGiverOnMars.Managers;

namespace TheGiverOnMars.Components.Item.Base
{
    public class ContractElement
    {
        public BaseItem Item { get; set; }
        public int Quantity { get; set; }
    }

    public class SimpleContract
    { 
        public ContractElement Requirement { get; set; }
        public ContractElement Promise { get; set; }
    }

    public class TimedSimpleContract : SimpleContract
    {
        public int SecondsToFulfill { get; set; }
    }

    /// <summary>
    /// Class for converting certains items into other item.
    /// Used by crafting and interactable objects.
    /// </summary>
    public class Contract
    {
        public List<ContractElement> Requirement { get; set; }
        public List<ContractElement> Promise { get; set; }
    }

    public class ContractInstance
    { 
        public List<ItemStack> Requirement { get; set; }
        public List<ItemStack> Promise { get; set; }

        public ContractInstance(Contract contract)
        {
            Requirement = contract.Requirement.Select(x => new ItemStack(x.Item, x.Quantity)).ToList();
            Promise = contract.Promise.Select(x => new ItemStack(x.Item, x.Quantity)).ToList();
        }

        public bool DoesInventorySatisfy(Inventory inventory, out List<int> indexesUnsatified)
        {
            indexesUnsatified = new List<int>();

            foreach (var requirement in Requirement)
            {
                var matchedSpace = inventory.Spaces
                    .Where(x => x.HasValue)?
                    .FirstOrDefault(x => x.ItemInterfaced.Name.Equals(requirement.Item.Item.Name)) 
                    ?? null;

                if (matchedSpace == null)
                {
                    indexesUnsatified.Add(Requirement.IndexOf(requirement));
                }
                else if (matchedSpace != null && requirement.Item.Item.IsStackable)
                {
                    var stackSpace = (StackInventorySpace) matchedSpace;

                    if (stackSpace.ItemStack.Count < requirement.Count)
                    {
                        indexesUnsatified.Add(Requirement.IndexOf(requirement));
                    }
                }
            }

            if (indexesUnsatified.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Should only be called if fulfillable
        public void FulfillContract(ref Inventory inventory, SpriteTile playerTile)
        {
            foreach (var requirement in Requirement)
            {
                var matchedSpace = inventory.Spaces
                    .Where(x => x.HasValue)?
                    .FirstOrDefault(x => x.ItemInterfaced.Name.Equals(requirement.Item.Item.Name))
                    ?? null;

                if (matchedSpace == null)
                {
                    throw new Exception("Contract is unfulfillable!");
                }

                else if (!requirement.Item.Item.IsStackable)
                {
                    inventory.Spaces[Array.IndexOf(inventory.Spaces, matchedSpace)] = new InventorySpace(false);
                }

                else if (matchedSpace != null && requirement.Item.Item.IsStackable)
                {
                    ((StackInventorySpace)matchedSpace).ItemStack.Count -= requirement.Count;

                    if (((StackInventorySpace)matchedSpace).ItemStack.Count <= 0)
                    {
                        inventory.Spaces[Array.IndexOf(inventory.Spaces, matchedSpace)] = new InventorySpace(false);
                    }
                }
            }

            foreach (var promise in Promise)
            {
                MapManager.CurrentMap.Spawn(promise.Item.Item, playerTile.Position, promise.Count);
            }
        }
    }

    public class TimeContract : Contract
    { 
    
    }
}
