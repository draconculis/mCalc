using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mCalc.cls
{
    public class mPoint : IComparable
    {
        public long Ticks { get; set; }
        public Decimal Amount { get; set; }

        public DateTime Date
        {
            get => new DateTime(Ticks);
            set => Ticks = value.Ticks;
        }

        #region Contructors -------------------------------------------
        public mPoint(decimal amount)
            : this(DateTime.Now.Ticks, amount)
        {
        }

        public mPoint(long ticks, decimal amount)
        {
            Ticks = ticks;
            Amount = amount;
        }

        public mPoint(DateTime date, decimal amount)
        {
            Date = date;
            Amount = amount;
        }
        #endregion Contructors ----------------------------------------

        #region ICompare stuff ----------------------------------------
        public int CompareTo(object obj)
        {
            return this.Ticks.CompareTo(((mPoint)obj).Ticks);
        }

        public class AmountComparer : IComparer<mPoint>
        {
            public int Compare(object x, object y)
            {
                return ((mPoint)x).Amount.CompareTo(((mPoint)y).Amount);
            }

            public int Compare(mPoint x, mPoint y)
            {
                return x.Amount.CompareTo(y.Amount);
            }
        }
        #endregion ICompare stuff -------------------------------------
    }

}
