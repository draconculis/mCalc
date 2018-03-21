using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dek572.cls
{
    /// <summary>
    /// https://stackoverflow.com/questions/5721889/is-there-a-list-that-is-sorted-automatically-in-net
    /// </summary>
    public class OrderedList<T> : IList<T>, ICollection<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    {
        readonly List<T> m_List;
        readonly IComparer<T> m_Comparer;

        #region Constructors ------------------------------------------
        // For internal use
        private OrderedList(List<T> list, IComparer<T> comparer)
        {
            m_List = list;
            m_Comparer = comparer;
        }

        public OrderedList()
            : this(new List<T>(), Comparer<T>.Default)
        {
        }

        public OrderedList(IComparer<T> comparer)
            : this(new List<T>(), comparer)
        {
        }

        public OrderedList(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {
        }

        public OrderedList(IEnumerable<T> collection, IComparer<T> comparer)
            : this(new List<T>(collection), comparer)
        {
            m_List.Sort(comparer);
        }

        public OrderedList(int capacity)
            : this(new List<T>(capacity), Comparer<T>.Default)
        {
        }

        public OrderedList(int capacity, IComparer<T> comparer)
            : this(new List<T>(capacity), comparer)
        {
        }

        //yet to be implemented
        //public void OrderedList(Comparison<T> comparison);

        #endregion Contructors ----------------------------------------

        #region Properties --------------------------------------------
        public int Capacity { get => m_List.Capacity; set => m_List.Capacity = value; }
        public int Count => m_List.Count;
        public T Last => m_List[m_List.Count - 1];
        object IList.this[int index] { get => m_List[index]; set => m_List[index] = (T)value; }
        public T this[int index] { get => m_List[index]; set => m_List[index] = value; }
        //public bool IsSynchronized { get { return false; } }
        bool ICollection.IsSynchronized => false;
        //public object SyncRoot { get { return _list; } }
        object ICollection.SyncRoot => m_List; //? should return this 
        bool IList.IsFixedSize => false;
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;

        #endregion Properties -----------------------------------------

        #region Methods -----------------------------------------------
        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        /// <summary>
        /// Adds a new item to the appropriate index of the SortedList
        /// </summary>
        /// <param name="item">The item to be removed</param>
        /// <returns>The index at which the item was inserted</returns>
        public int Add(T item)
        {
            int index = BinarySearch(item);
            if (index < 0)
            {
                index = ~index;
            }

            m_List.Insert(index, item);
            return index;
        }

        int IList.Add(object item)
        {
            return Add((T)item);
        }

        //NOT performance tested against other ways algorithms yet
        public void AddRange(IEnumerable<T> collection)
        {
            var insertList = new List<T>(collection);
            if (insertList.Count == 0)
            {
                return;
            }
            if (m_List.Count == 0)
            {
                m_List.AddRange(collection);
                m_List.Sort(m_Comparer);
                return;
            }

            //if we insert backwards, index we are inserting at does not keep incrementing
            insertList.Sort(m_Comparer);
            int searchLength = m_List.Count;
            for (int i = insertList.Count - 1; i >= 0; i--)
            {
                T item = insertList[i];
                int insertIndex = BinarySearch(0, searchLength, item);
                if (insertIndex < 0)
                {
                    insertIndex = ~insertIndex;
                }
                else
                {
                    while (--insertIndex >= 0 && m_List[insertIndex].Equals(item)) { }
                    insertIndex++;
                }
                if (insertIndex <= 0)
                {
                    m_List.InsertRange(0, insertList.GetRange(0, i + 1));
                    break;
                }
                searchLength = insertIndex - 1;
                item = m_List[searchLength];
                int endInsert = i;
                while (--i >= 0 && m_Comparer.Compare(insertList[i], item) > 0) { }
                i++;
                m_List.InsertRange(insertIndex, insertList.GetRange(i, endInsert - i + 1));
            }
        }

        //NOT performance tested against other ways algorithms yet
        public void AddRange(OrderedList<T> collection)
        {
            var insertList = new List<T>(collection);
            if (insertList.Count == 0)
            {
                return;
            }
            if (m_List.Count == 0)
            {
                m_List.AddRange(collection);
                return;
            }

            //if we insert backwards, index we are inserting at does not keep incrementing
            int searchLength = m_List.Count;
            for (int i = insertList.Count - 1; i >= 0; i--)
            {
                T item = insertList[i];
                int insertIndex = BinarySearch(0, searchLength, item);
                if (insertIndex < 0)
                {
                    insertIndex = ~insertIndex;
                }
                else
                {
                    while (--insertIndex >= 0 && m_List[insertIndex].Equals(item)) { }
                    insertIndex++;
                }
                if (insertIndex <= 0)
                {
                    m_List.InsertRange(0, insertList.GetRange(0, i + 1));
                    break;
                }
                searchLength = insertIndex - 1;
                item = m_List[searchLength];
                int endInsert = i;
                while (--i >= 0 && m_Comparer.Compare(insertList[i], item) > 0) { }
                i++;
                m_List.InsertRange(insertIndex, insertList.GetRange(i, endInsert - i + 1));
            }
        }

        public int BinarySearch(T item)
        {
            return m_List.BinarySearch(item, m_Comparer);
        }

        public int BinarySearch(int index, int count, T item)
        {
            return m_List.BinarySearch(index, count, item, m_Comparer);
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return m_List.AsReadOnly();
        }

        public void Clear() { m_List.Clear(); }

        public bool Contains(T item) { return BinarySearch(item) >= 0; }

        bool IList.Contains(object item)
        {
            return Contains((T)item);
        }

        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) { return m_List.ConvertAll(converter); }

        public void CopyTo(T[] array) { m_List.CopyTo(array); }

        public void CopyTo(T[] array, int arrayIndex) { m_List.CopyTo(array, arrayIndex); }

        void ICollection.CopyTo(Array array, int arrayIndex) { m_List.CopyTo((T[])array, arrayIndex); }

        public void CopyTo(int index, T[] array, int arrayIndex, int count) { m_List.CopyTo(index, array, arrayIndex, count); }

        public void ForEach(Action<T> action)
        {
            foreach (T item in m_List)
            {
                action(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { return m_List.GetEnumerator(); }

        public IEnumerator<T> GetEnumerator() { return m_List.GetEnumerator(); }

        public List<T> GetRange(int index, int count) { return m_List.GetRange(index, count); }

        public bool Remove(T item)
        {
            int index = BinarySearch(item);
            if (index < 0)
            {
                return false;
            }
            m_List.RemoveAt(index);
            return true;
        }
        void IList.Remove(object item)
        {
            Remove((T)item);
        }

        public void RemoveAt(int index) { m_List.RemoveAt(index); }
        public void RemoveRange(int index, int count) { m_List.RemoveRange(index, count); }
        public T[] ToArray() { return m_List.ToArray(); }
        public void TrimExcess() { m_List.TrimExcess(); }

        /// <summary>
        /// Find the first index of the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            int index = BinarySearch(item);
            if (index < 0) return -1;
            while (--index >= 0 && m_List[index].Equals(item)) { }
            return index + 1;
        }

        int IList.IndexOf(object item)
        {
            return IndexOf((T)item);
        }

        /// <summary>
        /// Find the last index of the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int LastIndexOf(T item)
        {
            int index = BinarySearch(item);
            if (index < 0) return -1;
            while (++index < m_List.Count && m_List[index].Equals(item)) { }
            return index - 1;
        }

        /// <summary>
        /// Return all values within bounds specified
        /// </summary>
        /// <param name="min">Minimum Bound</param>
        /// <param name="max">Maximum Bound</param>
        /// <returns>subset of list with values within or equal to bounds specified</returns>
        public T[] WithinRange(T min, T max)
        {
            if (m_Comparer.Compare(min, max) > 0)
            {
                throw new ArgumentException("min must be <= max");
            }
            int minSearchLength;
            int maxIndex = m_List.BinarySearch(max, m_Comparer);
            if (maxIndex >= 0)
            {
                minSearchLength = maxIndex + 1;
                while (++maxIndex < m_List.Count && m_Comparer.Compare(max, m_List[maxIndex]) == 0) { }
                --maxIndex;
            }
            else
            {
                minSearchLength = ~maxIndex;
                if (minSearchLength <= 0)
                {
                    return new T[0];
                }
                maxIndex = minSearchLength - 1;
            }

            int minIndex = m_List.BinarySearch(0, minSearchLength, min, m_Comparer);
            if (minIndex >= 0)
            {
                while (--minIndex >= 0 && m_Comparer.Compare(max, m_List[minIndex]) == 0) { }
                ++minIndex;
            }
            else
            {
                minIndex = ~minIndex;
                if (minIndex > maxIndex)
                {
                    return new T[0];
                }
            }
            int length = maxIndex - minIndex + 1;
            var returnVar = new T[length];
            m_List.CopyTo(minIndex, returnVar, 0, length);
            return returnVar;

        }
        #endregion ----------------------------------------------------

        #region NotImplemented ----------------------------------------
        private const string insertExceptionMsg = "OrderedList detemines position to insert automatically - use add method without an index";
        void IList.Insert(int index, object item)
        {
            throw new NotImplementedException(insertExceptionMsg);
        }
        void IList<T>.Insert(int index, T item)
        {
            throw new NotImplementedException(insertExceptionMsg);
        }
        #endregion ----------------------------------------------------
    }
}
