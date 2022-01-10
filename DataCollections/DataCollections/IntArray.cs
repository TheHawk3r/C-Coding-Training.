using System;

namespace DataCollections
{
    public class IntArray
    {
        private const int InitialSize = 4;
        private int previousSize;
        private int[] array;

        public IntArray()
        {
            array = new int[InitialSize];
            previousSize = InitialSize;
            Count = 0;
        }

        public int Count
        {
            get;
            private set;
        }

        public int this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public void Add(int element)
        {
            CheckArrayCount();
            array[Count] = element;
            Count++;
        }

        public bool Contains(int element)
        {
            return Array.Exists(array, elementToCheck => elementToCheck == element);
        }

        public int IndexOf(int element)
        {
            return Array.IndexOf(array, element);
        }

        public void Insert(int index, int element)
        {
            CheckArrayCount();
            Count++;
            this.ShiftToTheRight(index);
            array[index] = element;
        }

        public void Clear()
        {
            array = Array.Empty<int>();
            CheckArrayCount();
            Count = 0;
        }

        public void Remove(int element)
        {
            int index = Array.IndexOf(array, element);
            this.RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            this.ShiftToTheLeft(index);
            Count--;
            CheckArrayCount();
        }

        private void ShiftToTheRight(int index)
        {
            for (int i = index; i < Count; i++)
            {
                array[i + 1] = array[index];
            }
        }

        private void ShiftToTheLeft(int index)
        {
            for (int i = index; i < Count; i++)
            {
                array[i] = array[i + 1];
            }
        }

        private void CheckArrayCount()
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
    }
}
