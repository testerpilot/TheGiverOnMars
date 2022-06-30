using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Managers;
using TheGiverOnMars.Objects;

using Items = TheGiverOnMars.Components.Item.Definitions;

namespace TheGiverOnMars.Components.PlacedObject.Definitions
{
    public enum FurnanceState { Idle, Processing, Done }

    public class Furnace : InteractablePlacedObjectWithDrop
    {
        private List<TimedSimpleContract> Contracts = new List<TimedSimpleContract>()
        {
            new TimedSimpleContract()
            {
                Requirement = new ContractElement()
                {
                    Item = new Items.CopperOre(),
                    Quantity = 3
                },
                Promise = new ContractElement()
                {
                    Item = new Items.CopperBar(),
                    Quantity = 1
                },
                SecondsToFulfill = 10
            },
            new TimedSimpleContract()
            {
                Requirement = new ContractElement()
                {
                    Item = new Items.IronOre(),
                    Quantity = 3
                },
                Promise = new ContractElement()
                {
                    Item = new Items.IronBar(),
                    Quantity = 1
                },
                SecondsToFulfill = 10
            },
            new TimedSimpleContract()
            {
                Requirement = new ContractElement()
                {
                    Item = new Items.GoldOre(),
                    Quantity = 3
                },
                Promise = new ContractElement()
                {
                    Item = new Items.GoldBar(),
                    Quantity = 1
                },
                SecondsToFulfill = 10
            },
            new TimedSimpleContract()
            {
                Requirement = new ContractElement()
                {
                    Item = new Items.TinOre(),
                    Quantity = 3
                },
                Promise = new ContractElement()
                {
                    Item = new Items.TinBar(),
                    Quantity = 1
                },
                SecondsToFulfill = 10
            },
        };

        public int Health_ { get; set; } = 4;
        public TimedSimpleContract CurrentContract { get; set; }
        public double? TimeLeft { get; set; } = null;
        public FurnanceState State { get; set; } = FurnanceState.Idle; 

        public Dictionary<FurnanceState, Texture2D> Textures = new Dictionary<FurnanceState, Texture2D>()
        {
            { FurnanceState.Idle, SpriteManager.GetSpriteFromDict(17) },
            { FurnanceState.Processing, SpriteManager.GetSpriteFromDict(18) },
            { FurnanceState.Done, SpriteManager.GetSpriteFromDict(17) }
        };

        public Texture2D CurrentTexture = SpriteManager.GetSpriteFromDict(17);

        public PointLight Light { get; set; } = new PointLight()
        {
            Color = Color.Orange,
            Radius = 100,
            Intensity = 1f,
            Enabled = true,
            Scale = new Vector2(500, 500)
        };

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
            Health_ -= damage;
        }

        public override int Health() => Health_;

        public override void Interact(Player player)
        {
            if (State == FurnanceState.Idle)
            {
                var selectedSpace = player.InventoryManager.Inventory.Spaces[player.InventoryManager.SelectedTile];

                if (selectedSpace.HasValue)
                {
                    var appliedContract = Contracts.FirstOrDefault(x => x.Requirement.Item.Name.Equals(selectedSpace.ItemInterfaced.Name));

                    if (appliedContract == null)
                    {
                        return;
                    }

                    if (selectedSpace.ItemInterfaced.IsStackable)
                    {
                        var stack = ((StackInventorySpace)selectedSpace).ItemStack;

                        if (stack.Count < appliedContract.Requirement.Quantity)
                        {
                            return;
                        }

                        stack.Count -= appliedContract.Requirement.Quantity;

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
                    Constants.Penumbra.Lights.Add(Light);
                }
            }

            else if (State == FurnanceState.Done)
            {
                MapManager.CurrentMap.Spawn(CurrentContract.Promise.Item, player.Tile.Position, CurrentContract.Promise.Quantity);

                CurrentContract = null;
                TimeLeft = null;
                State = FurnanceState.Idle;
                CurrentTexture = Textures[FurnanceState.Idle];
            }
        }

        public override List<(Item.Base.BaseItem, int)> ItemIdAndQuantityOnDrop()
        {
            var list = new List<(Item.Base.BaseItem, int)>();

            if (CurrentContract != null)
            {
                list.Add((CurrentContract.Promise.Item, CurrentContract.Promise.Quantity));
            }

            return list; 
        }

        public override void Update(GameTime gameTime, SpriteTile tile)
        {
            if (Light.Position != tile.Position)
            {
                Light.Position = tile.Center.ToVector2();
            }

            if (State == FurnanceState.Processing)
            {
                TimeLeft -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (TimeLeft <= 0)
            {
                State = FurnanceState.Done;
                CurrentTexture = Textures[FurnanceState.Done];
                Constants.Penumbra.Lights.Remove(Light);
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
                Health_ = Health_,
                CurrentContract = CurrentContract,
                State = State,
                Name = Name,
                TileID = TileID,
                TimeLeft = TimeLeft,
                CurrentTexture = CurrentTexture
            };

            return temp;
        }

        public override string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public override void Parse(string data)
        {
            var temp = JsonSerializer.Deserialize<Furnace>(data);

            Health_ = temp.Health_;
            CurrentContract = temp.CurrentContract;
            TimeLeft = temp.TimeLeft;
            State = temp.State;
            CurrentTexture = Textures[State];
            Light = Light;

            if (State == FurnanceState.Processing)
            {
                Constants.Penumbra.Lights.Add(Light);
            }
        }
    }
}
