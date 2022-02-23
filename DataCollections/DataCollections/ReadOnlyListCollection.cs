using System;
using System.Collections;
using System.Collections.Generic;

namespace DataCollections
{
    public class ReadOnlyListCollection<T> : IList<T>
    {
        private readonly ListCollection<T> list;

        public ReadOnlyListCollection(ListCollection<T> list)
        {
            this.list = list;
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public int Count => list.Count;

        public T this[int index]
        {
            get { return list[index]; }
            set { throw new NotSupportedException(); }
        }

        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
