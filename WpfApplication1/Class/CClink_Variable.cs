using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Class.Inheritance;

namespace WpfApplication1.Class
{
    public class CClink_Variable
    {
        public int MAX_X_COUNT = 8192;
        public int MAX_Y_COUNT = 8192;
        public int MAX_SB_COUNT = 512;
        public int MAX_SW_COUNT = 512;
        public int MAX_RAB_COUNT = 1535;
        public int MAX_WW_COUNT = 2047;
        public int MAX_WR_COUNT = 2047;
        public int MAX_SPB_COUNT = 32767;

        public X _X;
        public Y _Y;
        public SB _SB;
        public SW _SW;
        public RAB _RAB;
        public WW _WW;
        public WR _WR;
        public SPB _SPB;

        public AccessibleObject CurrentTarget;

        public CClink_Variable()
        {
            _X = new X(MAX_X_COUNT);
            _Y = new Y(MAX_Y_COUNT);
            _SB = new SB(MAX_SB_COUNT);
            _SW = new SW(MAX_SW_COUNT);
            _RAB = new RAB(MAX_RAB_COUNT);
            _WW = new WW(MAX_WW_COUNT);
            _WR = new WR(MAX_WR_COUNT);
            _SPB = new SPB(MAX_SPB_COUNT);

        }
        public class X : AccessibleObject
        {
            public X(int MaximumCount)
            {
                base.MaximumCount = MaximumCount;
            }
        }
        public class Y : AccessibleObject { 
            public Y(int MaximumCount)
            {
                base.MaximumCount = MaximumCount;
            }
        
        }

        public class SB : AccessibleObject { 
            public SB(int MaximumCount)
            {
                base.MaximumCount = MaximumCount;
            }
        }
        public class SW : AccessibleObject { 
            public SW(int MaximumCount)
            {
                base.MaximumCount = MaximumCount;
            }
        }
        public class RAB : AccessibleObject { 
            public RAB(int MaximumCount)
            {
                base.MaximumCount = MaximumCount;
            }
        }
        public class WW : AccessibleObject {
            public WW(int MaximumCount)
            {
                base.MaximumCount = MaximumCount;
            }
        }
        public class WR : AccessibleObject {
            public WR(int MaximumCount)
            {
                base.MaximumCount = MaximumCount;
            }
        }
        public class SPB : AccessibleObject { 
            public SPB(int MaximumCount)
            {
                base.MaximumCount = MaximumCount;
            }
        }


    }
}
