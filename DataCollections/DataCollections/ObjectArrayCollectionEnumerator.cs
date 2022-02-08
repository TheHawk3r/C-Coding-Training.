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

        private object Current
        {
            get => array[position];
        }

        private bool MoveNext()
        {
            position++;
            return position < count;
        }

        private void Reset()
        {
            position = -1;
        }
    }
}
