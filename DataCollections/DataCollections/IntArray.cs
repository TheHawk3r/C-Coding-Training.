using System;

namespace DataCollections
{
    public class IntArray
    {
        private const int InitialSize = 4;
        private int[] array;
        private int count;

        public IntArray()
        {
            array = new int[InitialSize];
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
            count++;
            CheckArrayCount();
            this.ShiftToTheRight(index);
            array[index] = element;
        }

        public void Clear()
        {
            array = Array.Empty<int>();
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
        }

        private void ShiftToTheRight(int index)
        {
            for (int i = index; i < array.Length - 1; i++)
            {
                array[i + 1] = array[index];
            }
        }

        private void ShiftToTheLeft(int index)
        {
            for (int i = index; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }
        }

        private void CheckArrayCount()
        {
            const int two = 2;

            if (count <= array.Length - 1)
            {
                return;
            }

            Array.Resize(ref array, array.Length * two);
        }
    }
}
