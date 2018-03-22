using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dek572.Extensions
{
    public static class IEnumeratorExtensions
    {

        /// <summary>
        /// Merge ordered lists contained in a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="aa"></param>
        /// <param name="orderFunc"></param>
        /// <returns></returns>
        public static IEnumerable<T> MergePreserveOrder<T, TOrder>(
            this IEnumerable<IEnumerable<T>> aa,
            Func<T, TOrder> orderFunc) where TOrder : IComparable<TOrder>
        {
            List<Tuple<TOrder, IEnumerator<T>>> items = aa.Select(xx => xx.GetEnumerator())
                .Where(ee => ee.MoveNext())
                .Select(ee => Tuple.Create(orderFunc(ee.Current), ee))
                .OrderBy(ee => ee.Item1).ToList();

            while (items.Count > 0)
            {
                yield return items[0].Item2.Current;

                var next = items[0];
                items.RemoveAt(0);
                if (next.Item2.MoveNext())
                {
                    var value = orderFunc(next.Item2.Current);
                    var ii = 0;
                    for (; ii < items.Count; ++ii)
                    {
                        if (value.CompareTo(items[ii].Item1) <= 0)
                        {
                            // NB: using a tuple to minimize calls to orderFunc
                            items.Insert(ii, Tuple.Create(value, next.Item2));
                            break;
                        }
                    }

                    if (ii == items.Count) items.Add(Tuple.Create(value, next.Item2));
                }
                else next.Item2.Dispose(); // Dispose enumerator
            }
        }

        /// <summary>
        /// Merge 2 ordered lists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="aa"></param>
        /// <param name="bb"></param>
        /// <param name="orderFunc"></param>
        /// <returns></returns>
        public static IEnumerable<T> MergePreserveOrder<T, TOrder>(
            this IEnumerable<T> aa,
            IEnumerable<T> bb,
            Func<T, TOrder> orderFunc) 
            where TOrder : IComparable<TOrder>
        {
            return MergePreserveOrder(new List<IEnumerable<T>>{aa, bb}, orderFunc);
        }
    }
}
