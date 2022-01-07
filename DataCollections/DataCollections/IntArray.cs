using System;

namespace DataCollections
{
    public class IntArray
    {
        private const int InitialSize = 4;
        private int previousSize;
        private int[] array;
        private int count;

        public IntArray()
        {
            array = new int[InitialSize];
            previousSize = InitialSize;
            count = 0;
        }

        public void Add(int element)
        {
            CheckArrayCount();
            array[count] = element;
            count++;
        }

        public int Count()
        {
            return count;
        }

        public int Element(int index)
        {
            return array[index];
        }

        public void SetElement(int index, int element)
        {
            array[index] = element;
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
            count++;
            this.ShiftToTheRight(index);
            array[index] = element;
        }

        public void Clear()
        {
            array = Array.Empty<int>();
            CheckArrayCount();
            count = 0;
        }

        public void Remove(int element)
        {
            int index = Array.IndexOf(array, element);
            this.RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            this.ShiftToTheLeft(index);
            count--;
            CheckArrayCount();
        }

        private void ShiftToTheRight(int index)
        {
            for (int i = index; i < count; i++)
            {
                array[i + 1] = array[index];
            }
        }

        private void ShiftToTheLeft(int index)
        {
            for (int i = index; i < count; i++)
            {
                array[i] = array[i + 1];
            }
        }

        private void CheckArrayCount()
        {
            const int two = 2;

            if (count <= previousSize && array.Length != previousSize)
            {
                Array.Resize(ref array, previousSize);
                return;
            }

            if (count <= array.Length - 1)
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
