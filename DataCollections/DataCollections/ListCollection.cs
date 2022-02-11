using System;
using System.Collections;

namespace DataCollections
{
    public class ListCollection<T> : IEnumerable
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

        public T this[int index]
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

        public void Add(T element)
        {
            CheckArrayCount();
            array[Count] = element;
            Count++;
        }

        public bool Contains(T element)
        {
            return Array.Exists(array, elementToCheck => elementToCheck.Equals(element) && this.IndexOf(element) <= Count - 1);
        }

        public int IndexOf(T element)
        {
            return Array.IndexOf(array, element, 0, Count);
        }

        public void Insert(int index, T element)
        {
            CheckArrayCount();
            Count++;
            ShiftToTheRight(index);
            array[index] = element;
        }

        public void Clear()
        {
            array = Array.Empty<T>();
            CheckArrayCount();
            Count = 0;
        }

        public void Remove(T element)
        {
            int index = this.IndexOf(element);
            if (index == -1)
            {
                return;
            }

            this.RemoveAt(index);
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

        private ListCollectionEnumerator<T> GetEnumerator()
        {
            return new ListCollectionEnumerator<T>(this);
        }
    }
}
