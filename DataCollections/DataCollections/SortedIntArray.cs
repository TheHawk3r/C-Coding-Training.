namespace DataCollections
{
    public class SortedIntArray : IntArray
    {
        public override int this[int index]
        {
            set
            {
                if (value > ElementAt(index + 1, value) || value < ElementAt(index - 1, value))
                {
                    return;
                }

                base[index] = value;
            }
        }

        public override void Add(int element)
        {
            base.Add(element);
            BubbleSort();
        }

        public override void Insert(int index, int element)
        {
            if (element > ElementAt(index, element) || element < ElementAt(index - 1, element))
            {
                return;
            }

            base.Insert(index, element);
        }

        private int ElementAt(int index, int value)
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
                    if (base[j] > base[j + 1])
                    {
                        int temp = base[j];
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
