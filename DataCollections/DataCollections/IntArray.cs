using System;

namespace DataCollections
{
    public class IntArray
    {
        private int[] array;

        public IntArray()
        {
            array = new int[0];
        }

        public void Add(int element)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = element;
        }

        public int Count()
        {
            return array.Length;
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
            Array.Resize(ref array, array.Length + 1);
            int oldArrayLength = array.Length - 1;
            Array.Copy(array[..oldArrayLength], 0, array, 1, oldArrayLength);
            array[index] = element;
        }

        public void Clear()
        {
            array = Array.Empty<int>();
        }

        public void Remove(int element)
        {
            int index = Array.IndexOf(array, element);
            this.RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            Array.Copy(array, 0, array, 0, index);
            Array.Copy(array, index + 1, array, index, array.Length - (index + 1));
            Array.Resize(ref array, array.Length - 1);
        }
    }
}
