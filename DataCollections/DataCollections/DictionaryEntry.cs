namespace DataCollections
{
    public class DictionaryEntry<TValue, TKey>
    {
        internal LinkedListNode<TValue> Node;

        public DictionaryEntry(LinkedListNode<TValue> node, TKey key)
        {
            Node = node;
            Key = key;
        }

        public DictionaryEntry(LinkedListNode<TValue> node, LinkedListCollection<TValue> list, TKey key)
        {
            Node = node;
            List = list;
            Key = key;
        }

        public LinkedListCollection<TValue> List
        {
            get => Node.List;
            internal set => Node.List = value;
        }

        public LinkedListNode<TValue> Next
        {
            get => Node.Next;
            internal set => Node.Next = value;
        }

        public LinkedListNode<TValue> Previous
        {
            get => Node.Previous;
            internal set => Node.Previous = value;
        }

        public TKey Key
        {
            get;
            internal set;
        }

        public TValue Value
        {
            get => Node.Value;
            set => Node.Value = value;
        }

        internal void Invalidate()
        {
            Node.Invalidate();
        }
    }
}
