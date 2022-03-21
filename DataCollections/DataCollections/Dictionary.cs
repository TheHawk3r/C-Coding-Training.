using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DataCollections
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private const int StartOfFreeList = -3;
        private int[] buckets;
        private DictionaryElement<TValue, TKey>[] elements;
        private int freeList;
        private int freeCount;

        public ICollection<TKey> Keys
        {
            get
            {
                var keys = new ListCollection<TKey>();
                foreach (var element in this)
                {
                    Keys.Add(element.Key);
                }

                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var values = new ListCollection<TValue>();
                foreach (var element in this)
                {
                    values.Add(element.Value);
                }

                return values;
            }
        }

        public int Count
        {
            get;
            set;
        }

        public bool IsReadOnly => false;

        public TValue this[TKey key]
        {
            get
            {
                if (!TryGetIndexOfKey(key, out int index))
                {
                    return default;
                }

                return elements[index].Value;
            }

            set
            {
                if (!TryGetIndexOfKey(key, out int index))
                {
                    Add(key, value);
                }

                elements[index].Value = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return elements[i];
            }
        }

        private void Initialize(uint capacity)
        {
            buckets = new int[capacity];
            elements = new DictionaryElement<TValue, TKey>[capacity];
            freeList = -1;
        }

        private bool TryGetIndexOfKey(TKey key, out int index)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (object.Equals(key, elements[i].Key))
                {
                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;
        }

        private bool TryInsert(TKey key, TValue value, bool throwOnExisting)
        {
            if (key == null)
            {
                throw new ArgumentNullException(key.ToString());
            }

            if (buckets == null || elements == null)
            {
                Initialize(0);
            }

            uint hashCode = (uint)key.GetHashCode();
            ref int bucket = ref buckets[hashCode % (uint)buckets.Length];
            CheckKeys(key, value, true);

            int index;
            if (freeCount > 0)
            {
                index = freeList;
                freeList = StartOfFreeList - elements[freeList].Next;
                freeCount--;
            }
            else
            {
                if (Count == elements.Length)
                {
                    Resize(elements.Length + 1);
                    bucket = ref buckets[hashCode % (uint)buckets.Length];
                }

                index = Count;
                Count++;
            }

            ref DictionaryElement<TValue, TKey> element = ref elements![index];
            element.HashCode = hashCode;
            element.Next = bucket - 1;
            element.Key = key;
            element.Value = value;
            bucket = index + 1;

            return true;
        }

        private void CheckKeys(TKey key, TValue value, bool throwOnExisting)
        {
            uint hashCode = (uint)key.GetHashCode();
            uint collisionCount = 0;
            ref int bucket = ref buckets[hashCode % (uint)buckets.Length];

            for (int i = bucket - 1; i < elements.Length; i = elements[i].Next)
            {
                if (elements[i].HashCode == hashCode && object.Equals(elements[i].Key, key))
                {
                    if (throwOnExisting)
                    {
                        throw new InvalidOperationException("Key already present in dictionary.");
                    }

                    elements[i].Value = value;
                }

                collisionCount++;

                if (collisionCount > elements.Length)
                {
                    throw new InvalidOperationException("Collisions excedet capacity");
                }
            }
        }

        private void Resize(int newSize)
        {
            if (elements == null)
            {
                throw new InvalidOperationException("elements is null");
            }

            if (newSize <= elements.Length)
            {
                throw new ArgumentException("The size should be bigger then the actual size");
            }

            var newElements = new DictionaryElement<TValue, TKey>[newSize];
            Array.Copy(elements, newElements, Count);
            buckets = new int[newSize];

            for (int i = 0; i < Count; i++)
            {
                if (newElements[i].Next >= -1)
                {
                    ref int bucket = ref buckets[elements[i].HashCode % (uint)buckets.Length];
                    newElements[i].Next = bucket - 1;
                }
            }

            elements = newElements;
        }
    }
}
