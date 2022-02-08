using System.Collections;

namespace DataCollections
{
    internal class ObjectArrayCollectionEnumerator : IEnumerator
    {
        private readonly ObjectArrayCollection objectArray;
        private int position = -1;

        public ObjectArrayCollectionEnumerator(ObjectArrayCollection objectArray)
        {
            this.objectArray = objectArray;
        }

        public object Current
        {
            get => objectArray[position];
        }

        public bool MoveNext()
        {
            position++;
            return position < objectArray.Count;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
