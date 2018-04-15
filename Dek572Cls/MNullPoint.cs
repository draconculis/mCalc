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

        decimal IMPoint.Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime IMPoint.Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        long IMPoint.Ticks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int CompareTo(object obj)
        {
            return -1;
        }

        int IMPoint.CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
