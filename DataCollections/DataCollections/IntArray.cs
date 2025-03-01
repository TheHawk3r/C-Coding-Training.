﻿using System;

namespace DataCollections
{
    public class IntArray
    {
        private const int InitialSize = 4;
        private int[] array;
        private int previousSize;

        public IntArray()
        {
            array = new int[InitialSize];
            previousSize = InitialSize;
            Count = 0;
        }

        public int Count
        {
            get;
            protected set;
        }

        public virtual int this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
                {
                    return -1;
                }

                return array[index];
            }
            set => array[index] = value;
        }

        public virtual void Add(int element)
        {
            CheckArrayCount();
            array[Count] = element;
            Count++;
        }

        public bool Contains(int element)
        {
            return Array.Exists(array, elementToCheck => elementToCheck == element && this.IndexOf(element) <= Count - 1);
        }

        public int IndexOf(int element)
        {
            return Array.IndexOf(array, element, 0, Count);
        }

        public virtual void Insert(int index, int element)
        {
            CheckArrayCount();
            Count++;
            ShiftToTheRight(index);
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
