namespace DataCollections
{
    public class SortedIntArray : IntArray
    {
        public override int this[int index]
        {
            set
            {
                this[index] = value;
                BubbleSort();
            }
        }

        public override void Add(int element)
        {
            CheckArrayCount();
            this[Count] = element;
            Count++;
            BubbleSort();
        }

        public override void Insert(int index, int element)
        {
            CheckArrayCount();
            Count++;
            ShiftToTheRight(index);
            this[index] = element;
            BubbleSort();
        }

        private void BubbleSort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = 0; j < Count - i - 1; j++)
                {
                    if (this[j] > this[j + 1])
                    {
                        int temp = this[j];
                        this[j] = this[j + 1];
                        this[j + 1] = temp;
                    }
                }
            }
        }
    }
}
