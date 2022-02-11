using System.Collections;

namespace DataCollections
{
    internal class ListCollectionEnumerator<T> : IEnumerator
    {
        private readonly ListCollection<T> list;
        private int position = -1;

        public ListCollectionEnumerator(ListCollection<T> list)
        {
            this.list = list;
        }

        public object Current
        {
            get => list[position];
        }

        public bool MoveNext()
        {
            position++;
            return position < list.Count;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
