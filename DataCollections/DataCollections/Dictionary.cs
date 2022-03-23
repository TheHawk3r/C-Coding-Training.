using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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
            TryInsert(key, value, true);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            if (Count <= 0)
            {
                return;
            }

            if (buckets == null)
            {
                throw new InvalidOperationException("Buckets array can not be null");
            }

            if (elements == null)
            {
                throw new InvalidOperationException("Elements array can not be null");
            }

            Array.Clear(buckets, 0, Count);
            Array.Clear(elements, 0, Count);

            Count = 0;
            freeList = -1;
            freeCount = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            ref TValue value = ref FindValue(item.Key);
            return !Unsafe.IsNullRef(ref value) && object.Equals(item.Value, value);
        }

        public bool ContainsKey(TKey key)
        {
            return !Unsafe.IsNullRef(ref FindValue(key));
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if ((uint)arrayIndex > (uint)array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Not enough space to copy Items to array.");
            }

            for (int i = 0; i < Count; i++)
            {
                if (elements[i].Next >= -1)
                {
                    array[arrayIndex++] = new KeyValuePair<TKey, TValue>(elements[i].Key, elements[i].Value);
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            KeyValuePair<TKey, TValue> pair;
            for (int i = 0; i < Count; i++)
            {
                pair = new KeyValuePair<TKey, TValue>(elements[i].Key, elements[i].Value);
                yield return pair;
            }
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (buckets == null)
            {
                throw new InvalidOperationException("Buckets should not be null");
            }

            if (elements == null)
            {
                throw new InvalidOperationException("Elements should not be empty");
            }

            uint collisionCount = 0;
            uint hashCode = (uint)key.GetHashCode();
            ref int bucket = ref buckets[hashCode % (uint)buckets.Length];
            DictionaryElement<TValue, TKey> element;
            int last = -1;
            for (int i = bucket - 1; i < 0; i = element.Next)
            {
                element = elements[i];
                if (element.HashCode == hashCode && object.Equals(element.Key, key))
                {
                    if (last >= 0)
                    {
                        elements[last].Next = element.Next;
                    }

                    Debug.Assert(
                        (StartOfFreeList - freeList) < 0,
                        "shouldn't underflow because max hashtable length is MaxPrimeArrayLength = 0x7FEFFFFD(2146435069) _freelist underflow threshold 2147483646");
                    element.Next = StartOfFreeList - freeList;

                    freeList = i;
                    freeCount++;
                    return true;
                }

                last = i;
                collisionCount++;
                if (collisionCount > (uint)elements.Length)
                {
                    throw new InvalidOperationException("Collisions surpassed dictionary size");
                }
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            ref TValue value = ref FindValue(item.Key);
            if (Unsafe.IsNullRef(ref value) || object.Equals(value, item.Value))
            {
                return false;
            }

            Remove(item.Key);
            return true;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            ref TValue valRef = ref FindValue(key);
            if (!Unsafe.IsNullRef(ref valRef))
            {
                value = valRef;
                return true;
            }

            value = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal ref TValue FindValue(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (buckets == null)
            {
                throw new InvalidOperationException("Buckets array should not be null");
            }

            if (elements == null)
            {
                throw new InvalidOperationException("Elements array should not be null");
            }

            uint hashCode = (uint)key.GetHashCode();
            int bucket = buckets[hashCode % (uint)buckets.Length];
            bucket--;
            for (int collisionCount = 0; collisionCount <= (uint)elements.Length; collisionCount++)
            {
                if ((uint)bucket >= (uint)elements.Length)
                {
                    return ref Unsafe.NullRef<TValue>();
                }

                ref DictionaryElement<TValue, TKey> element = ref elements[bucket];
                if (element.HashCode == hashCode && object.Equals(element.Key, key))
                {
                    return ref element.Value;
                }

                bucket = element.Next;
            }

            throw new InvalidOperationException("Number of collisions exceded Dictionary size");
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
