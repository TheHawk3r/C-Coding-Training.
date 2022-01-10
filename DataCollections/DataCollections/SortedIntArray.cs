namespace DataCollections
{
    public class SortedIntArray : IntArray
    {
        public override int this[int index]
        {
            get => array[index];
            set
            {
                array[index] = value;
                BubbleSort();
            }
        }

        public override void Add(int element)
        {
            CheckArrayCount();
            array[Count] = element;
            Count++;
            BubbleSort();
        }

        public override void Insert(int index, int element)
        {
            CheckArrayCount();
            Count++;
            ShiftToTheRight(index);
            array[index] = element;
            BubbleSort();
        }

        private void BubbleSort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = 0; j < Count - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }
}
