using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Objects;
using BaseItem = TheGiverOnMars.Components.Item.Base;

namespace TheGiverOnMars.Components.PlacedObject
{
    /// <summary>
    /// Class for interfacing with objects on the map
    /// </summary>
    public class PlacedObject
    {
        public int TileID { get; set; }
        public string Name { get; set; }

        public virtual PlacedObject DeepCopy()
        {
            var temp = new PlacedObject();

            temp.TileID = TileID;
            temp.Name = Name;

            return temp;
        }

        public virtual string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public virtual void Parse(string data)
        {
            var temp = JsonSerializer.Deserialize<PlacedObject>(data);

            TileID = temp.TileID;
            Name = temp.Name;
        }
    }

    public class BreakablePlacedObject : PlacedObject
    {
        public Dictionary<string, int> BreakableWith { get; set; } = new Dictionary<string, int>();
        public int Health { get; set; }

        public override PlacedObject DeepCopy()
        {
            var temp = new BreakablePlacedObject
            {
                Health = Health,
                BreakableWith = BreakableWith,
                Name = Name,
                TileID = TileID
            };

            return temp;
        }

        public override string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public override void Parse(string data)
        {
            var temp = JsonSerializer.Deserialize<BreakablePlacedObject>(data);

            TileID = temp.TileID;
            Name = temp.Name;
            Health = temp.Health;
            BreakableWith = temp.BreakableWith;
        }
    }

    public class PlacedObjectWithDrop : BreakablePlacedObject
    {
        public List<(BaseItem.BaseItem, int)> ItemIdAndQuantity { get; set; } = new List<(BaseItem.BaseItem, int)>();

        public override PlacedObject DeepCopy()
        {
            var temp = new PlacedObjectWithDrop();

            temp.ItemIdAndQuantity = ItemIdAndQuantity;
            temp.Health = Health;
            temp.BreakableWith = BreakableWith;
            temp.Name = Name;
            temp.TileID = TileID;

            return temp;
        }

        public override string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public override void Parse(string data)
        {
            var temp = JsonSerializer.Deserialize<PlacedObjectWithDrop>(data);

            ItemIdAndQuantity = temp.ItemIdAndQuantity;
            TileID = temp.TileID;
            Name = temp.Name;
            Health = temp.Health;
            BreakableWith = temp.BreakableWith;
        }
    }

    public abstract class InteractablePlacedObject : PlacedObject
    {
        public abstract void Interact(Player player);

        // Object may want to update texture based on state
        public abstract void Update(GameTime gameTime, SpriteTile tile);

        public override abstract PlacedObject DeepCopy();
    }

    public abstract class BreakableInteractablePlacedObject : InteractablePlacedObject
    {
        public abstract Dictionary<string, int> BreakableWith();
        public abstract void SubtractHealth(int damage);
        public abstract int Health();
    }

    public abstract class InteractablePlacedObjectWithDrop : BreakableInteractablePlacedObject
    {
        public abstract List<(BaseItem.BaseItem, int)> ItemIdAndQuantityOnDrop();
    }

    public class PlacedObjectInstance
    {
        public PlacedObject PlacedObject;
        public SpriteTile Tile;

        public PlacedObjectInstance(PlacedObject placedObject)
        {
            PlacedObject = placedObject;
            Tile = TileManager.GetTileFromID(placedObject.TileID).DeepCopy();
        }

        public void Update(GameTime gameTime)
        {
            if (PlacedObject.GetType().IsSubclassOf(typeof(InteractablePlacedObject)))
            {
                ((InteractablePlacedObject)PlacedObject).Update(gameTime, Tile);
            }
        }

        public void Break(string tool)
        {
            if (PlacedObject.GetType().IsSubclassOf(typeof(BreakablePlacedObject)))
            {
                var breakableObject = (BreakablePlacedObject)PlacedObject;
                breakableObject.Health -= breakableObject.BreakableWith[tool];

                if (breakableObject.Health > 0)
                {
                    return;
                }
            }
            else if (PlacedObject.GetType().IsSubclassOf(typeof(BreakableInteractablePlacedObject)))
            {
                var breakableObject = (BreakableInteractablePlacedObject)PlacedObject;
                breakableObject.SubtractHealth(breakableObject.BreakableWith()[tool]);

                if (breakableObject.Health() > 0)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            MapManager.CurrentMap.PlacedObjectForRemoval = this;

            if (Tile.GetType().Equals(typeof(CollisionTile)))
            {
                MapManager.CurrentMap.CollisionRects.Remove((CollisionTile)Tile);
            }

            if (PlacedObject.GetType().Equals(typeof(PlacedObjectWithDrop)) || 
                PlacedObject.GetType().IsSubclassOf(typeof(PlacedObjectWithDrop)))
            {
                var objectWithDrop = (PlacedObjectWithDrop)PlacedObject;

                foreach (var tuple in objectWithDrop.ItemIdAndQuantity)
                {
                    MapManager.CurrentMap.Spawn(tuple.Item1, Tile.Position, tuple.Item2);
                }
            }
            else if (PlacedObject.GetType().IsSubclassOf(typeof(InteractablePlacedObjectWithDrop)))
            {
                var objectWithDrop = (InteractablePlacedObjectWithDrop)PlacedObject;

                if (objectWithDrop.ItemIdAndQuantityOnDrop() != null)
                {
                    foreach (var tuple in objectWithDrop.ItemIdAndQuantityOnDrop())
                    {
                        MapManager.CurrentMap.Spawn(tuple.Item1, Tile.Position, tuple.Item2);
                    }
                }
            }
        }

    }
}