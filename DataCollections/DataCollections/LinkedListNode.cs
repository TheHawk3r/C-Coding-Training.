namespace DataCollections
{
    public class LinkedListNode<T>
    {
        internal LinkedListNode<T> PreviousNode;
        internal LinkedListNode<T> NextNode;

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

        public LinkedListNode<T> Next => NextNode == null || NextNode == List.Head ? null : NextNode;

        public LinkedListNode<T> Previous => PreviousNode == null || this == List.Head ? null : PreviousNode;

        public T Value { get; set; }

        internal void Invalidate()
        {
            List = null;
            NextNode = null;
            PreviousNode = null;
        }
    }
}
