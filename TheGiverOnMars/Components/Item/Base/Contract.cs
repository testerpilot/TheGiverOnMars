using System;
using System.Collections.Generic;
using System.Text;

namespace TheGiverOnMars.Components.Item.Base
{
    public class SimpleContract
    {
        public (Item, int) Requirement;
        public (Item, int) Promise;
    }

    public class TimedContract : SimpleContract
    {
        public int SecondsToFulfill;
    }

    /// <summary>
    /// Class for converting certains items into other item.
    /// Used by crafting and interactable objects.
    /// </summary>
    public class Contract
    {
        public List<(Item, int)> Requirement;
        public List<(Item, int)> Promise;
    }

    public class TimeContract : Contract
    { 
    
    }
}
