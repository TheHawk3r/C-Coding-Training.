namespace DataCollections
{
    public class SortedIntArray : IntArray
    {
        public override int this[int index]
        {
            set
            {
                bool indexIsNotFirstOrLastElement = (index != 0 && index != Count - 1) && (value > base[index + 1] || value < base[index - 1]);
                bool indexIsFirstElement = index == 0 && value > base[index + 1];
                bool indexIsLastElement = index == Count - 1 && value < base[index - 1];
                if (indexIsNotFirstOrLastElement || indexIsFirstElement || indexIsLastElement)
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
            bool indexIsNotFirstOrLastElement = index != 0 && (element > base[index] || element < base[index - 1]);
            bool indexIsFirstElement = index == 0 && element > base[index];
            if (indexIsNotFirstOrLastElement || indexIsFirstElement)
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
