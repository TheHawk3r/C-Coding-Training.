using System.Collections;

namespace DataCollections
{
    internal class ObjectArrayCollectionEnumerator : IEnumerator
    {
        private readonly object[] array;
        private readonly int count;
        private int position = -1;

        public ObjectArrayCollectionEnumerator(object[] list, int count)
        {
            array = list;
            this.count = count;
        }

        public object Current
        {
            get => array[position];
        }

        public bool MoveNext()
        {
            position++;
            return position < count;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
