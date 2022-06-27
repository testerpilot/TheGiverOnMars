using System;
using System.Collections.Generic;
using System.Text;

namespace TheGiverOnMars.Components.Item.Base
{
    public class ContractElement
    {
        public Item Item { get; set; }
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

    public class TimeContract : Contract
    { 
    
    }
}
