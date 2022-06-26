using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public enum FurnanceState { Idle, Processing, Done }

    public class Furnace : InteractablePlacedObjectWithDrop
    {
        List<TimedContract> Contracts = new List<TimedContract>()
        {
            new TimedContract()
            {
                Requirement = (new Items.CopperOre(), 3),
                Promise = (new Items.CopperBar(), 1),
                SecondsToFulfill = 10
            }
        };

        public int _health = 4;
        public TimedContract CurrentContract;
        public double? TimeLeft = null;
        public FurnanceState State = FurnanceState.Idle; 

        public Dictionary<FurnanceState, Texture2D> Textures = new Dictionary<FurnanceState, Texture2D>()
        {
            { FurnanceState.Idle, SpriteManager.GetSpriteFromDict(17) },
            { FurnanceState.Processing, SpriteManager.GetSpriteFromDict(18) },
            { FurnanceState.Done, SpriteManager.GetSpriteFromDict(17) }
        };

        public Texture2D CurrentTexture = SpriteManager.GetSpriteFromDict(17); 

        public Furnace()
        {
            Name = "Furnace";
            TileID = 28;
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

        public override void Interact(Player player)
        {
            if (State == FurnanceState.Idle)
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

                    CurrentContract = appliedContract;
                    TimeLeft = CurrentContract.SecondsToFulfill;
                    State = FurnanceState.Processing;
                    CurrentTexture = Textures[FurnanceState.Processing];
                }
            }

            else if (State == FurnanceState.Done)
            {
                MapManager.CurrentMap.Spawn(CurrentContract.Promise.Item1, player.Tile.Position, CurrentContract.Promise.Item2);

                CurrentContract = null;
                TimeLeft = null;
                State = FurnanceState.Idle;
                CurrentTexture = Textures[FurnanceState.Idle];
            }
        }

        public override List<(Item.Base.Item, int)> ItemIdAndQuantityOnDrop()
        {
            var list = new List<(Item.Base.Item, int)>();

            if (CurrentContract != null)
            {
                list.Add(CurrentContract.Requirement);
            }

            return list; 
        }

        public override void Update(GameTime gameTime, SpriteTile tile)
        {
            if (State == FurnanceState.Processing)
            {
                TimeLeft -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (TimeLeft <= 0)
            {
                State = FurnanceState.Done;
                CurrentTexture = Textures[FurnanceState.Done];
                TimeLeft = null;
            }

            if (!tile._texture.Equals(CurrentTexture))
            {
                tile._texture = CurrentTexture;
            }
        }

        public override PlacedObject DeepCopy()
        {
            var temp = new Furnace
            {
                _health = _health,
                CurrentContract = CurrentContract,
                State = State,
                Name = Name,
                TileID = TileID,
                TimeLeft = TimeLeft,
                CurrentTexture = CurrentTexture
            };

            return temp;
        }
    }
}
