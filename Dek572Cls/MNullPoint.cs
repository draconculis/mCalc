using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dek572.Cls
{
    public class MNullPoint : IMPoint
    {
        public decimal Amount => decimal.MinValue;
        public DateTime Date => DateTime.MinValue;
        public long Ticks => long.MinValue;
        public int CompareTo(object obj)
        {
            return -1;
        }
    }
}
