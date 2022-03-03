using System.Collections;
using System.Collections.Generic;

namespace DataCollections
{
    internal abstract class BaseListDecorator<T> : ICollection<T>
    {
        protected ICollection<T> list;

        public BaseListDecorator(ICollection<T> list)
        {
            this.list = list;
        }

        public abstract int Count { get; }

        public abstract bool IsReadOnly { get; }

        public abstract void Add(T item);

        public abstract void Clear();

        public abstract bool Contains(T item);

        public abstract void CopyTo(T[] array, int arrayIndex);

        public abstract IEnumerator<T> GetEnumerator();

        public abstract bool Remove(T item);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
