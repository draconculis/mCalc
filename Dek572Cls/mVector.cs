using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dek572.Cls
{
    /// <summary>
    /// Specialized class for a list of mPonts
    /// </summary>
    public class mVector : IEnumerable<MPoint>
    {
        private OrderedList<MPoint> m_Points;
        public int Count => m_Points.Count;
        public int LastIdx => m_Points.Count - 1;
        public MPoint First => m_Points.Any() ? m_Points.First() : MPoint.;
        public MPoint Last => m_Points.First();

        public mVector()
        {
            m_Points = new OrderedList<MPoint>();
        }

        public mVector(mVector points)
        {
            m_Points = points.m_Points;
        }

        /// <summary>
        /// Indexer
        /// </summary>
        public MPoint this[int idx]
        {
            get => m_Points[idx];
            set => m_Points[idx] = value;
        }

        /// <summary>
        /// Adds item in order, according to ticks
        /// </summary>
        /// <param name="point"></param>
        public void Add(MPoint point)
        {
            m_Points.Add(point);
        }

        /// <summary>
        /// Merges two vectors in order
        /// </summary>
        /// <param name="points"></param>
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

        public decimal MeanValue(DateTime from, DateTime to)
        {
            if (Count == 0)
                return 0m;

            decimal sum = 0m;
            foreach (var point in m_Points)
            {
                sum += point.Amount;
            }
            return sum / Count;
        }

        public Tuple<decimal, decimal> Range(long fromTicks = long.MinValue, long toTicks = long.MaxValue)
        {
            if (fromTicks < m_Points[0].Ticks)
                fromTicks = m_Points[0].Ticks;

            if (toTicks > m_Points[LastIdx].Ticks)
                fromTicks = m_Points[LastIdx].Ticks;

            decimal min = decimal.MinValue;
            decimal max = decimal.MinValue;
            foreach (var point in m_Points)
            {
                if (point.Amount > max)
                    max = point.Amount;
                if (point.Amount < min)
                    min = point.Amount;
            }
            return new Tuple<decimal, decimal>(min, max);
        }

        /// <summary>
        /// Returns a tuple of from idx and to idx with a close match to the provided ticks range.
        /// Will return closest greater minTicks and closest smaller maxtick
        /// </summary>
        /// <param name="fromTicks"></param>
        /// <param name="toTicks"></param>
        /// <returns></returns>
        public Tuple<int, int> IdxRange(long fromTicks = long.MinValue, long toTicks = long.MaxValue)
        {
            int fromIdx = NextOrEqualTicksIdx(fromTicks);
            int toIdx = PrevOrEqualTicksIdx(toTicks);

            return new Tuple<int, int>(fromIdx, toIdx);
        }

        public int NextTicksIdx(long ticks)
        {
            for (int i = 0; i < Count; i++)
            {
                if (m_Points[i].Ticks > ticks)
                    return i;
            }

            return LastIdx;
        }

        public int PrevTicksIdx(long ticks)
        {
            for (int i = LastIdx; i >= 0; i--)
            {
                if (m_Points[i].Ticks < ticks)
                    return i;
            }

            return 0;
        }

        public int NextOrEqualTicksIdx(long ticks)
        {
            for (int i = 0; i < Count; i++)
            {
                if (m_Points[i].Ticks >= ticks)
                    return i;
            }

            return LastIdx;
        }

        public int PrevOrEqualTicksIdx(long ticks)
        {
            for (int i = LastIdx; i >= 0; i--)
            {
                if (m_Points[i].Ticks <= ticks)
                    return i;
            }

            return 0;
        }


        /// <summary>
        /// Get min and max for ticks
        /// </summary>
        public Tuple<long, long> TicksRange()
        {
            return new Tuple<long, long>(m_Points[0].Ticks, m_Points[LastIdx].Ticks);
        }

        public Tuple<DateTime, DateTime> DateRange()
        {
            var tickRange = TicksRange();
            return new Tuple<DateTime, DateTime>(new DateTime(tickRange.Item1), new DateTime(tickRange.Item2));
        }


        #region File stuff --------------------------------------------

        public bool Load(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);

                foreach (var line in lines)
                {
                    var lineparts = line.Split(';');
                    m_Points.Add(new MPoint(long.Parse(lineparts[0]), decimal.Parse(lineparts[1])));
                }

            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Save(string filename)
        {
            try
            {
                using (StreamWriter streamw = new StreamWriter(filename))
                {
                    foreach (var point in m_Points)
                    {
                        streamw.WriteLine($"{point.Ticks};{point.Amount};");
                    }
                }

            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion File stuff --------------------------------------------


        public IEnumerator<MPoint> GetEnumerator()
        {
            return m_Points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }

}
