namespace DataCollections
{
    internal struct DictionaryElement<TValue, TKey>
    {
        public uint HashCode;
        public int Next;
        public TKey Key;
        public TValue Value;
    }
}
