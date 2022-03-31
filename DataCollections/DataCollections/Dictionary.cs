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
                    return;
                }

                elements[index].Value = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            CheckKeyNullException(key);

            if (buckets == null || elements == null)
            {
                Initialize(1);
            }

            int hashCode = key.GetHashCode();
            ref int bucket = ref buckets[Math.Abs(hashCode) % buckets.Length];
            CheckKeys(key, value, hashCode, ref bucket);
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
                    Resize(elements.Length + 1);
                    bucket = ref buckets[Math.Abs(hashCode) % buckets.Length];
                }

                index = Count;
                Count++;
            }

            ref DictionaryElement<TValue, TKey> element = ref elements![index];
            element.HashCode = hashCode;
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
            CheckArrayNullExcepiton(array);
            CheckArrayIndexOutOfRangeException(arrayIndex, array);
            CheckNotEnoughSpaceArgumentException(arrayIndex, array);
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
            CheckKeyNullException(key);
            CheckBucketsNullException();
            CheckElementsNullException();

            uint collisionCount = 0;
            int hashCode = key.GetHashCode();
            int last = -1;
            for (int i = buckets[Math.Abs(hashCode) % buckets.Length]; i != -1; i = elements[i].Next)
            {
                if (elements[i].HashCode == hashCode && object.Equals(elements[i].Key, key))
                {
                    if (last >= 0)
                    {
                        elements[last].Next = elements[i].Next;
                    }
                    else if (last == -1)
                    {
                        buckets[Math.Abs(hashCode) % buckets.Length] = elements[i].Next == -1 ? -1 : elements[i].Next;
                    }

                    elements[i].Next = -1;
                    elements[i].HashCode = default;
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
                collisionCount++;
                CheckCollitsionLimitExceededExecption(collisionCount);
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
            CheckKeyNullException(key);
            CheckBucketsNullException();
            CheckElementsNullException();

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
            for (int i = 0; i < capacity; i++)
            {
                elements[i].Next = -1;
                buckets[i] = -1;
            }
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

        private void CheckKeys(TKey key, TValue value, int hashCode, ref int bucket)
        {
            uint collisionCount = 0;

            for (int i = bucket; i != -1; i = elements[i].Next)
            {
                CheckKeyPresentInvalidOperationException(i, hashCode, key);

                collisionCount++;

                CheckCollitsionLimitExceededExecption(collisionCount);
            }
        }

        private void Resize(int newSize)
        {
            CheckElementsNullException();
            CheckNewSizeArgumentException(newSize);

            var newElements = new DictionaryElement<TValue, TKey>[newSize];
            Array.Copy(elements, newElements, Count);
            var newBuckets = new int[newSize];
            Array.Fill(newBuckets, -1);
            newElements[newElements.Length - 1].Next = -1;
            for (int i = 0; i < Count; i++)
            {
                int hashCode = elements[i].HashCode;
                if (newBuckets[Math.Abs(hashCode) % newBuckets.Length] != -1 && !object.Equals(newElements[newBuckets[Math.Abs(hashCode) % newBuckets.Length]].Key, elements[i].Key))
                {
                    newElements[newBuckets[Math.Abs(hashCode) % newBuckets.Length]].Next = i;
                    if (newElements[i].Next == newBuckets[Math.Abs(hashCode) % newBuckets.Length])
                    {
                        newElements[i].Next = newBuckets[Math.Abs(hashCode) % newBuckets.Length];
                    }
                }
                else if (newBuckets[Math.Abs(hashCode) % newBuckets.Length] == -1)
                {
                    newBuckets[Math.Abs(hashCode) % newBuckets.Length] = i;
                }
            }

            buckets = newBuckets;
            elements = newElements;
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

        private void CheckCollitsionLimitExceededExecption(uint collisionCount)
        {
            if (collisionCount <= (uint)elements.Length)
            {
                return;
            }

            throw new InvalidOperationException("Collisions surpassed dictionary size");
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
            if (elements[i].HashCode != hashCode || !object.Equals(elements[i].Key, key))
            {
                return;
            }

            throw new InvalidOperationException("Key already present in dictionary.");
        }
    }
}
