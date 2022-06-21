using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Components.PlacedObject.Definitions
{
    public class Chest : InteractablePlacedObject
    {
        public Inventory Inventory;

        public Chest()
        {
            Name = "Chest";
            TileID = 10;

            Inventory = new Inventory();
        }

        public Chest(List<InventorySpace> inventorySpaces)
        {
            Name = "Chest";
            TileID = 10;

            Inventory = new Inventory(inventorySpaces);
        }

        public override void Interact(Player player)
        {
            player.InventoryManager.InventoryInteractingWith = Inventory;
            player.InventoryManager.IsInventoryOpen = true;
        }
    }
}
