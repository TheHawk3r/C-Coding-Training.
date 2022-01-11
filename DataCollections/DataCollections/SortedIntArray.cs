namespace DataCollections
{
    public class SortedIntArray : IntArray
    {
        public override int this[int index]
        {
            set
            {
                if (value <= base[index + 1] && value >= base[index - 1])
                {
                    return;
                }

                base[index] = value;
            }
        }

        public override void Add(int element)
        {
            CheckArrayCount();
            base[Count] = element;
            Count++;
            BubbleSort();
        }

        public override void Insert(int index, int element)
            {
            if (element > base[index + 1] || element < base[index - 1])
            {
                return;
            }

            base.Insert(index, element);
        }

        private void BubbleSort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = 0; j < Count - i - 1; j++)
                {
                    if (base[j] > base[j + 1])
                    {
                        int temp = base[j];
                        base[j] = base[j + 1];
                        base[j + 1] = temp;
                    }
                }
            }
        }
    }
}
