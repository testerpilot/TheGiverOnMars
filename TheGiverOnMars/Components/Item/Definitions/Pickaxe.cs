using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.PlacedObject;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Objects;

namespace TheGiverOnMars.Components.Item.Definitions
{
    public class Pickaxe : ActionItem
    {
        public Pickaxe()
        {
            Name = "Pickaxe";
            TileId = 27;
            Attributes = null;
            IsStackable = false;
        }

        public override void OnUse(Player player)
        {
            foreach (var currentObject in MapManager.CurrentMap.PlacedObjects.Where(x => x.Tile.IsInProximity(player.Tile) &&
                (x.PlacedObject.GetType().IsSubclassOf(typeof(BreakablePlacedObject)) ||
                 x.PlacedObject.GetType().IsSubclassOf(typeof(BreakableInteractablePlacedObject)))))
            {
                if (currentObject.PlacedObject.GetType().IsSubclassOf(typeof(BreakablePlacedObject)))
                {
                    var breakableObject = (BreakablePlacedObject)currentObject.PlacedObject;

                    if (breakableObject.BreakableWith.Contains(Name))
                    {
                        currentObject.Break();
                    }
                }
                else
                {
                    var breakableObject = (BreakableInteractablePlacedObject)currentObject.PlacedObject;

                    if (breakableObject.BreakableWith().Contains(Name))
                    {
                        currentObject.Break();
                    }
                }
            }
        }
    }
}
