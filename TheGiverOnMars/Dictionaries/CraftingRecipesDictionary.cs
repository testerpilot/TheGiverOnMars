using System;
using System.Collections.Generic;
using System.Text;
using TheGiverOnMars.Components.Item.Base;
using TheGiverOnMars.Components.Item.Definitions;

namespace TheGiverOnMars.Dictionaries
{
    public static class CraftingRecipesDictionary
    {
        public static Dictionary<string, Contract> Dictionary = new Dictionary<string, Contract>()
        {
            {
                "Test",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 3
                        }
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 2
                        }
                    }
                }
            },
            {
                "2",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 5
                        },
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 7
                        },
                        new ContractElement()
                        {
                            Item = new GoldOre(),
                            Quantity = 3
                        },
                        new ContractElement()
                        {
                            Item = new TinOre(),
                            Quantity = 2
                        }
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new SteelBar(),
                            Quantity = 2
                        }
                    }
                }
            },
            {
                "3",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new GoldOre(),
                            Quantity = 1
                        },
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new Pickaxe(),
                            Quantity = 1
                        }
                    }
                }
            },
            {
                "4",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        },
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new ChestItem(),
                            Quantity = 1
                        }
                    }
                }
            },
            {
                "5",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new FurnaceItem(),
                            Quantity = 1
                        }
                    }
                }
            },
                        {
                "Test2",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        }
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperBar(),
                            Quantity = 1
                        }
                    }
                }
            },
            {
                "7",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new GoldOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new TinOre(),
                            Quantity = 1
                        }
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new GoldBar(),
                            Quantity = 1
                        }
                    }
                }
            },
            {
                "8",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new GoldOre(),
                            Quantity = 1
                        },
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new TinBar(),
                            Quantity = 1
                        }
                    }
                }
            },
            {
                "9",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        },
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new BronzeBar(),
                            Quantity = 1
                        }
                    }
                }
            },
            {
                "10",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new GoldOre(),
                            Quantity = 1
                        }
                    }
                }
            },
                        {
                "11",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        }
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new Pickaxe(),
                            Quantity = 1
                        }
                    }
                }
            },
            {
                "12",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new GoldOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new TinOre(),
                            Quantity = 1
                        }
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new ChestItem(),
                            Quantity = 1
                        }
                    }
                }
            },
            {
                "13",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new GoldOre(),
                            Quantity = 1
                        },
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        }
                    }
                }
            },
            {
                "14",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                        new ContractElement()
                        {
                            Item = new IronOre(),
                            Quantity = 1
                        },
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new TinOre(),
                            Quantity = 1
                        }
                    }
                }
            },
            {
                "15",
                new Contract()
                {
                    Requirement = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new CopperOre(),
                            Quantity = 1
                        },
                    },
                    Promise = new List<ContractElement>()
                    {
                        new ContractElement()
                        {
                            Item = new TinBar(),
                            Quantity = 1
                        }
                    }
                }
            },
        };
    }
}
