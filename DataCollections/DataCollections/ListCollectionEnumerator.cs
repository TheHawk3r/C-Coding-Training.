using System;
using System.Collections.Generic;

namespace DataCollections
{
    public class ListCollectionEnumerator<T> : IEnumerator<T>
    {
        private readonly ListCollection<T> list;
        private int position = -1;
        private bool disposedValue;

        public ListCollectionEnumerator(ListCollection<T> list)
        {
            this.list = list;
        }

        public object Current
        {
            get => list[position];
        }

        T IEnumerator<T>.Current
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

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                return;
            }

            disposedValue = true;
        }
    }
}
