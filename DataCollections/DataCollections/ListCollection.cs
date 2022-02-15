using System;
using System.Collections;
using System.Collections.Generic;

namespace DataCollections
{
    public class ListCollection<T> : IList<T>
    {
        protected T[] array;
        private const int InitialSize = 4;
        private int previousSize;

        public ListCollection()
        {
            array = new T[InitialSize];
            previousSize = InitialSize;
            Count = 0;
        }

        public int Count
        {
            get;
            private set;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public virtual T this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
                {
                    return default;
                }

                return array[index];
            }

            set => array[index] = value;
        }

        public virtual void Add(T item)
        {
            CheckArrayCount();
            array[Count] = item;
            Count++;
        }

        public void SwapItems(T a, T b)
        {
            if (!this.Contains(a) || !this.Contains(b))
            {
                return;
            }

            int indexOfA = this.IndexOf(a);
            int indexOfB = this.IndexOf(b);
            T temp = a;
            this.array[indexOfA] = b;
            this.array[indexOfB] = temp;
        }

        public void SwapObjects<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public bool Contains(T item)
        {
            return Array.Exists(array, elementToCheck => elementToCheck.Equals(item) && this.IndexOf(item) <= Count - 1);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                return;
            }

            if (arrayIndex < 0)
            {
                return;
            }

            if (Count > array.Length - arrayIndex + 1)
            {
                return;
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
            CheckArrayCount();
            Count++;
            ShiftToTheRight(index);
            array[index] = item;
        }

        public void Clear()
        {
            array = Array.Empty<T>();
            CheckArrayCount();
            Count = 0;
        }

        public bool Remove(T item)
        {
            bool itemRemoved = false;
            int index = this.IndexOf(item);
            if (index == -1)
            {
                return itemRemoved;
            }

            this.RemoveAt(index);
            itemRemoved = true;
            return itemRemoved;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
            {
                return;
            }

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

            if (Count <= previousSize && array.Length != previousSize)
            {
                Array.Resize(ref array, previousSize);
                return;
            }

            if (Count <= array.Length - 1)
            {
                return;
            }

            if (array.Length < InitialSize)
            {
                Array.Resize(ref array, InitialSize);
            }

            previousSize = array.Length;
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
    }
}
