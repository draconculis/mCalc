using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dek572.cls;

namespace mCalc.cls
{
    public class mVector : IEnumerable<mPoint>
    {
        private OrderedList<mPoint> m_Points;

        public mVector()
        {
            m_Points = new OrderedList<mPoint>();
        }

        public mVector(mVector points)
        {
            m_Points = points.m_Points;
        }

        public mPoint this[int idx]
        {
            get => m_Points[idx];
            set => m_Points[idx] = value;
        }

        public void Add(mPoint point)
        {
            m_Points.Add(point);
        }

        public void AddRange(mVector points)
        {
            m_Points.AddRange(points);
        }

        public decimal MaxValue()
        {
            decimal max = Decimal.MinValue;
            foreach (var point in m_Points)
            {
                if (max < point.Amount)
                    max = point.Amount;
            }
            return max;
        }

        public decimal MinValue()
        {
            decimal min = Decimal.MaxValue;
            foreach (var point in m_Points)
            {
                if (min > point.Amount)
                    min = point.Amount;
            }
            return min;
        }

        public decimal MaxValue(DateTime from, DateTime to)
        {
            decimal max = Decimal.MinValue;
            foreach (var point in m_Points)
            {
                if (max < point.Amount)
                    max = point.Amount;
            }
            return max;
        }

        public IEnumerator<mPoint> GetEnumerator()
        {
            return m_Points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

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
            return this.Ticks.CompareTo(((mPoint) obj).Ticks);
        }

        public class AmountComparer : IComparer, IComparer<mPoint>
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
