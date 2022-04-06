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
                    throw new KeyNotFoundException("Key not present in the Dictionary.");
                }

                return value;
            }

            set
            {
                CheckKeyNullException(key);
                int index = FindElementIndex(key);
                if (index == -1)
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
            CheckKeys(key);
            int index;
            if (freeCount > 0)
            {
                index = PopFreeIndex();
            }
            else
            {
                CheckDictionarySize(key);
                index = Count;
            }

            Count++;
            elements[index].Next = GetBucket(key);
            elements[index].Key = key;
            elements[index].Value = value;
            buckets[Math.Abs(key.GetHashCode()) % buckets.Length] = index;
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
            int index = FindElementIndex(item.Key);
            return !Unsafe.IsNullRef(ref elements[index].Value) && object.Equals(item.Value, elements[index].Value);
        }

        public bool ContainsKey(TKey key)
        {
            return !Unsafe.IsNullRef(ref elements[FindElementIndex(key)].Value);
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
            int last;
            int index;
            SearchForElement(key, out index, out last);
            if (index == -1)
            {
                return false;
            }

            if (last >= 0)
            {
                elements[last].Next = elements[index].Next;
            }
            else if (last == -1)
            {
                buckets[Math.Abs(key.GetHashCode()) % buckets.Length] = elements[index].Next == -1 ? -1 : elements[index].Next;
            }

            elements[index].Next = -1;
            elements[index].Key = default;
            elements[index].Value = default;

            if (freeList != -1)
            {
                elements[index].Next = freeList;
            }

            freeList = index;
            freeCount++;
            Count--;

            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = FindElementIndex(item.Key);
            if (index == -1 || !object.Equals(elements[index].Value, item.Value))
            {
                return false;
            }

            Remove(item.Key);
            return true;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            int index = FindElementIndex(key);
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

        internal int FindElementIndex(TKey key)
        {
            CheckNullExceptions(key);
            for (int i = GetBucket(key); i != -1; i = elements[i].Next)
            {
                if (object.Equals(key, elements[i].Key))
                {
                    return i;
                }
            }

            return -1;
        }

        private void SearchForElement(TKey key, out int index, out int previousIndex)
        {
            previousIndex = -1;
            index = GetBucket(key);
            if (index == -1)
            {
                return;
            }

            for (int i = index; i != -1; i = elements[i].Next)
            {
                if (elements[i].Key.GetHashCode() == key.GetHashCode() && object.Equals(elements[i].Key, key))
                {
                    index = i;
                    return;
                }

                previousIndex = i;
            }

            index = -1;
        }

        private int PopFreeIndex()
        {
            int temp = freeList;
            freeList = elements[freeList].Next;
            freeCount--;
            return temp;
        }

        private void CheckDictionarySize(TKey key)
        {
            if (Count != elements.Length)
            {
                return;
            }

            const int two = 2;
            Resize(elements.Length * two);
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

        private int GetBucket(TKey key)
        {
            int hashCode = key.GetHashCode();
            return buckets[Math.Abs(hashCode) % buckets.Length];
        }

        private void CheckKeys(TKey key)
        {
            for (int i = GetBucket(key); i != -1; i = elements[i].Next)
            {
                CheckKeyPresentInvalidOperationException(i, key);
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

        private void CheckKeyPresentInvalidOperationException(int i, TKey key)
        {
            if (elements[i].Key.GetHashCode() != key.GetHashCode() || !object.Equals(elements[i].Key, key))
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
