using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Dek572.Cls
{
    public class MPoint : IComparable, IMPoint
    {
        //public static IMPoint Null => MPoint
        private static IMPoint m_Null => new MNullPoint();
        public static IMPoint Null => m_Null;

        public long Ticks { get; set; }
        public Decimal Amount { get; set; }

        public DateTime Date
        {
            get => new DateTime(Ticks);
            set => Ticks = value.Ticks;
        }

        #region Constructors -------------------------------------------
        public MPoint(decimal amount)
            : this(DateTime.Now.Ticks, amount)
        {
        }

        public MPoint(long ticks, decimal amount)
        {
            Ticks = ticks;
            Amount = amount;
        }

        public MPoint(DateTime date, decimal amount)
        {
            Date = date;
            Amount = amount;
        }
        #endregion Constructors ----------------------------------------

        #region ICompare stuff ----------------------------------------
        public int CompareTo(object obj)
        {
            return this.Ticks.CompareTo(((MPoint)obj).Ticks);
        }

        public class AmountComparer : IComparer<MPoint>
        {
            public int Compare(object x, object y)
            {
                return ((MPoint)x).Amount.CompareTo(((MPoint)y).Amount);
            }

            public int Compare(MPoint x, MPoint y)
            {
                return x.Amount.CompareTo(y.Amount);
            }
        }
        #endregion ICompare stuff -------------------------------------
    }

}
