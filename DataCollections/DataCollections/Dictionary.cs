using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DataCollections
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private int[] buckets;
        private DictionaryElement<TValue, TKey>[] elements;
        private int freeList;
        private int freeCount;

        public Dictionary() : this(1)
        {
        }

        public Dictionary(uint capacity)
        {
            CheckCapacityOutOfRangeException(capacity);

            Initialize(capacity);
        }

        public ICollection<TKey> Keys
        {
            get
            {
                var keys = new ListCollection<TKey>();
                foreach (var element in this)
                {
                    keys.Add(element.Key);
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
            internal set;
        }

        public bool IsReadOnly => false;

        public TValue this[TKey key]
        {
            get
            {
                CheckKeyNullException(key);
                if (!TryGetValue(key, out TValue value))
                {
                    return value;
                }

                return value;
            }

            set
            {
                CheckKeyNullException(key);
                if (!TryGetIndexOfKey(key, out int index))
                {
                    Add(key, value);
                    return;
                }

                elements[index].Value = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            CheckNullExceptions(key);

            ref int bucket = ref GetBucket(key);
            CheckKeys(key, value, key.GetHashCode(), ref bucket);
            int index;
            if (freeCount > 0)
            {
                index = freeList;
                freeList = elements[freeList].Next;
                freeCount--;
                Count++;
            }
            else
            {
                if (Count == elements.Length)
                {
                    const int two = 2;
                    Resize(elements.Length * two);
                    bucket = ref GetBucket(key);
                }

                index = Count;
                Count++;
            }

            ref DictionaryElement<TValue, TKey> element = ref elements![index];
            element.Next = bucket;
            element.Key = key;
            element.Value = value;
            bucket = index;
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

            CheckElementsNullException();

            Array.Fill<int>(buckets, -1);
            Array.Clear(elements, 0, Count);
            for (int i = 0; i < Count; i++)
            {
                elements[i].Next = -1;
            }

            Count = 0;
            freeList = -1;
            freeCount = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int index = FindValue(item.Key);
            return !Unsafe.IsNullRef(ref elements[index].Value) && object.Equals(item.Value, elements[index].Value);
        }

        public bool ContainsKey(TKey key)
        {
            return !Unsafe.IsNullRef(ref elements[FindValue(key)].Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            CheckArrayNullExcepiton(array);
            CheckArrayIndexOutOfRangeException(arrayIndex, array);
            CheckNotEnoughSpaceArgumentException(arrayIndex, array);
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] == -1)
                {
                    continue;
                }

                for (int j = buckets[i]; j != -1; j = elements[j].Next)
                {
                    array[arrayIndex++] = new KeyValuePair<TKey, TValue>(elements[j].Key, elements[j].Value);
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            KeyValuePair<TKey, TValue> pair;
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] == -1)
                {
                    continue;
                }

                for (int j = buckets[i]; j != -1; j = elements[j].Next)
                {
                    pair = new KeyValuePair<TKey, TValue>(elements[j].Key, elements[j].Value);
                    yield return pair;
                }
            }
        }

        public bool Remove(TKey key)
        {
            CheckNullExceptions(key);
            int last = -1;
            for (int i = GetBucket(key); i != -1; i = elements[i].Next)
            {
                if (elements[i].Key.GetHashCode() == key.GetHashCode() && object.Equals(elements[i].Key, key))
                {
                    if (last >= 0)
                    {
                        elements[last].Next = elements[i].Next;
                    }
                    else if (last == -1)
                    {
                        buckets[Math.Abs(key.GetHashCode()) % buckets.Length] = elements[i].Next == -1 ? -1 : elements[i].Next;
                    }

                    elements[i].Next = -1;
                    elements[i].Key = default;
                    elements[i].Value = default;

                    if (freeList != -1)
                    {
                        elements[i].Next = freeList;
                    }

                    freeList = i;
                    freeCount++;
                    Count--;
                    return true;
                }

                last = i;
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = FindValue(item.Key);
            if (index == -1 || !object.Equals(elements[index].Value, item.Value))
            {
                return false;
            }

            Remove(item.Key);
            return true;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            int index = FindValue(key);
            if (index != -1)
            {
                value = elements[index].Value;
                return true;
            }

            value = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal int FindValue(TKey key)
        {
            CheckNullExceptions(key);
            int index = GetBucket(key);
            for (int collisionCount = 0; collisionCount <= (uint)elements.Length; collisionCount++)
            {
                if (index >= elements.Length || index < 0)
                {
                    return -1;
                }

                ref DictionaryElement<TValue, TKey> element = ref elements[index];
                if (element.Key.GetHashCode() == key.GetHashCode() && object.Equals(element.Key, key))
                {
                    return index;
                }

                index = element.Next;
            }

            throw new InvalidOperationException("Number of collisions exceded Dictionary size");
        }

        private void Initialize(uint capacity)
        {
            buckets = new int[capacity];
            elements = new DictionaryElement<TValue, TKey>[capacity];
            freeList = -1;
            for (int i = 0; i < capacity; i++)
            {
                elements[i].Next = -1;
                buckets[i] = -1;
            }
        }

        private ref int GetBucket(TKey key)
        {
            int hashCode = key.GetHashCode();
            return ref buckets[Math.Abs(hashCode) % buckets.Length];
        }

        private bool TryGetIndexOfKey(TKey key, out int index)
        {
            for (int i = GetBucket(key); i != -1; i = elements[i].Next)
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

        private void CheckKeys(TKey key, TValue value, int hashCode, ref int bucket)
        {
            for (int i = bucket; i != -1; i = elements[i].Next)
            {
                CheckKeyPresentInvalidOperationException(i, hashCode, key);
            }
        }

        private void Resize(int newSize)
        {
            CheckElementsNullException();
            CheckNewSizeArgumentException(newSize);

            var oldElements = elements;
            Count = 0;
            Initialize((uint)newSize);
            Array.Fill(buckets, -1);
            for (int i = 0; i < oldElements.Length; i++)
            {
                Add(oldElements[i].Key, oldElements[i].Value);
            }
        }

        private void CheckKeyNullException(TKey key)
        {
            if (key != null)
            {
                return;
            }

            throw new ArgumentNullException(nameof(key));
        }

        private void CheckBucketsNullException()
        {
            if (buckets != null)
            {
                return;
            }

            throw new InvalidOperationException("Buckets array can not be null");
        }

        private void CheckElementsNullException()
        {
            if (elements != null)
            {
                return;
            }

            throw new InvalidOperationException("Elements should not be empty");
        }

        private void CheckCapacityOutOfRangeException(uint capacity)
        {
            if (capacity > 0)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(capacity));
        }

        private void CheckArrayNullExcepiton(KeyValuePair<TKey, TValue>[] array)
            {
            if (array != null)
            {
                return;
            }

            throw new ArgumentNullException(nameof(array));
        }

        private void CheckArrayIndexOutOfRangeException(int arrayIndex, KeyValuePair<TKey, TValue>[] array)
            {
            if ((uint)arrayIndex <= (uint)array.Length)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        private void CheckNotEnoughSpaceArgumentException(int arrayIndex, KeyValuePair<TKey, TValue>[] array)
            {
            if (array.Length - arrayIndex >= Count)
            {
                return;
            }

            throw new ArgumentException("Not enough space to copy Items to array.");
        }

        private void CheckNewSizeArgumentException(int newSize)
            {
            if (newSize > elements.Length)
            {
                return;
            }

            throw new ArgumentException("The new size should be bigger then the actual size");
        }

        private void CheckKeyPresentInvalidOperationException(int i, int hashCode, TKey key)
            {
            if (elements[i].Key.GetHashCode() != hashCode || !object.Equals(elements[i].Key, key))
            {
                return;
            }

            throw new InvalidOperationException("Key already present in dictionary.");
        }

        private void CheckNullExceptions(TKey key)
        {
            CheckKeyNullException(key);
            CheckBucketsNullException();
            CheckElementsNullException();
        }
    }
}
