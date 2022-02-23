using System;
using System.Collections;
using System.Collections.Generic;

namespace DataCollections
{
    public class ListCollection<T> : IList<T>
    {
        protected T[] array;
        private const int InitialSize = 4;

        public ListCollection()
        {
            array = new T[InitialSize];
            Count = 0;
        }

        public int Count
        {
            get;
            private set;
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual T this[int index]
        {
            get
            {
                CheckArgumentOutOfBoundsException(index);
                return array[index];
            }

            set
            {
                CheckArgumentOutOfBoundsException(index);
                array[index] = value;
            }
        }

        public ReadOnlyListCollection<T> AsReadOnly()
        {
            return new ReadOnlyListCollection<T>(this);
        }

        public virtual void Add(T item)
        {
            CheckArrayCount();
            array[Count] = item;
            Count++;
        }

        public bool Contains(T item)
        {
            foreach (T element in this)
            {
                if (item.Equals(element))
                {
                    return this.IndexOf(item) <= Count - 1;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Array is null. Please input a proper array");
            }

            if (Count > array.Length - arrayIndex)
            {
                throw new ArgumentException(
                    $"The number of elements in the source List is greater than the available space from {nameof(arrayIndex)} to the end of the destination {nameof(array)}.",
                    nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "index is less then zero");
            }

            for (int i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = this.array[i];
            }
        }

        public int IndexOf(T item)
        {
            return Array.IndexOf(array, item, 0, Count);
        }

        public virtual void Insert(int index, T item)
        {
            CheckArgumentOutOfBoundsException(index);
            CheckArrayCount();
            Count++;
            ShiftToTheRight(index);
            array[index] = item;
        }

        public virtual void Clear()
        {
            array = Array.Empty<T>();
            CheckArrayCount();
            Count = 0;
        }

        public virtual bool Remove(T item)
        {
            int index = this.IndexOf(item);
            if (index == -1)
            {
                return false;
            }

            this.RemoveAt(index);
            return true;
        }

        public virtual void RemoveAt(int index)
        {
            CheckArgumentOutOfBoundsException(index);
            ShiftToTheLeft(index);
            Count--;
            CheckArrayCount();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return array[i];
            }
        }

        protected void CheckArrayCount()
        {
            const int two = 2;

            if (Count <= array.Length - 1)
            {
                return;
            }

            Array.Resize(ref array, array.Length * two);
        }

        protected void ShiftToTheRight(int index)
        {
            for (int i = Count - 1; i > index; i--)
            {
                array[i] = array[i - 1];
            }
        }

        protected void ShiftToTheLeft(int index)
        {
            for (int i = index; i < Count; i++)
            {
                array[i] = array[i + 1];
            }
        }

        protected void CheckArgumentOutOfBoundsException(int index)
        {
            if (index >= 0 && index <= Count - 1)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(index), "index outside bounds of List");
        }
    }
}
