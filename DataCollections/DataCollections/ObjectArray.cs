using System;

namespace DataCollections
{
    public class ObjectArray
    {
        protected object[] array;
        private const int InitialSize = 4;
        private int previousSize;

        public ObjectArray()
        {
            array = new object[InitialSize];
            previousSize = InitialSize;
            Count = 0;
        }

        public int Count
        {
            get;
            private set;
        }

        public object this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public void Add(object element)
        {
            CheckArrayCount();
            array[Count] = element;
            Count++;
        }

        public bool Contains(object element)
        {
            return Array.Exists(array, elementToCheck => elementToCheck.Equals(element));
        }

        public int IndexOf(object element)
        {
            return Array.IndexOf(array, element);
        }

        public void Insert(int index, object element)
        {
            CheckArrayCount();
            Count++;
            ShiftToTheRight(index);
            array[index] = element;
        }

        public void Clear()
        {
            array = Array.Empty<object>();
            CheckArrayCount();
            Count = 0;
        }

        public void Remove(object element)
        {
            int index = Array.IndexOf(array, element);
            this.RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            ShiftToTheLeft(index);
            Count--;
            CheckArrayCount();
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
