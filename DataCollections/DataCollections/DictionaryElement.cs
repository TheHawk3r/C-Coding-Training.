namespace DataCollections
{
    internal struct DictionaryElement<TValue, TKey>
    {
        public int Next;
        public TKey Key;
        public TValue Value;
    }
}
