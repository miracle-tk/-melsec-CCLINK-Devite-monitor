using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Class.Inheritance
{
    public class AccessibleObject
    {
        public int StartAddress;
        public Dictionary<int, ConsoleColor> dicAddressColor;
        public int MaximumCount;


        public AccessibleObject()
        {
            StartAddress = 0;
            MaximumCount = 0;
            dicAddressColor = new Dictionary<int, ConsoleColor>();
        }
    }
}
