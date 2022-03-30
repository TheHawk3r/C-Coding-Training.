namespace DataCollections
{
    internal struct DictionaryElement<TValue, TKey>
    {
        public int HashCode;
        public int Next;
        public TKey Key;
        public TValue Value;
    }
}
