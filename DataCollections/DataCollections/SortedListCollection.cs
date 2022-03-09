using System;

namespace DataCollections
{
    public class SortedListCollection<T> : ListCollection<T>
        where T : IComparable<T>
    {
        public override T this[int index]
        {
            set
            {
                if (value.CompareTo(ElementAt(index + 1, value)) > 0 || value.CompareTo(ElementAt(index - 1, value)) < 0)
                {
                    return;
                }

                base[index] = value;
            }
        }

        public override void Add(T item)
        {
            base.Add(item);
            BubbleSort();
        }

        public override void Insert(int index, T item)
        {
            if (item.CompareTo(ElementAt(index, item)) > 0 || item.CompareTo(ElementAt(index - 1, item)) < 0)
            {
                return;
            }

            base.Insert(index, item);
        }

        private T ElementAt(int index, T value)
        {
            if (index < 0 || index > Count - 1)
            {
                return value;
            }

            return base[index];
        }

        private void BubbleSort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                bool swaped = false;
                for (int j = 0; j < Count - i - 1; j++)
                {
                    if (base[j].CompareTo(base[j + 1]) > 0)
                    {
                        T temp = base[j];
                        base[j] = base[j + 1];
                        base[j + 1] = temp;
                        swaped = true;
                    }
                }

                if (!swaped)
                {
                    break;
                }
            }
        }
    }
}
