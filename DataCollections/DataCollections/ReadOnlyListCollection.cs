using System;

namespace DataCollections
{
    public class ReadOnlyListCollection<T> : ListCollection<T>
    {
        public ReadOnlyListCollection(T[] array)
        {
            this.array = array;
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override T this[int index]
        {
            set { throw new NotImplementedException(); }
        }

        public override void Add(T item)
        {
            throw new NotImplementedException();
        }

        public override void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public override void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }
}
