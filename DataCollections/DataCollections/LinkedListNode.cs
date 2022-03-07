namespace DataCollections
{
    public class LinkedListNode<T>
    {
        public LinkedListNode(T item)
        {
            this.Value = item;
        }

        public LinkedListNode(LinkedListCollection<T> list, T item)
        {
            List = list;
            this.Value = item;
        }

        public LinkedListCollection<T> List
        {
            get;
            internal set;
        }

        public LinkedListNode<T> Next
        {
            get;
            internal set;
        }

        public LinkedListNode<T> Previous
        {
            get;
            internal set;
        }

        public T Value { get; set; }

        internal void Invalidate()
        {
            List = null;
            Next = null;
            Previous = null;
        }
    }
}
