using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataCollections
{
    public class LinkedListCollection<T> : ICollection<T>
    {
        private readonly LinkedListNode<T> sentinel;

        public LinkedListCollection()
        {
            sentinel = new LinkedListNode<T>(this, default);
            sentinel.Next = sentinel;
            sentinel.Previous = sentinel;
        }

        public LinkedListCollection(ICollection<T> collection) : this()
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (T item in collection)
            {
                AddLast(item);
            }
        }

        public LinkedListNode<T> First => sentinel.Next == sentinel ? null : sentinel.Next;

        public LinkedListNode<T> Last => sentinel.Previous == sentinel ? null : sentinel.Previous;

        public int Count
        {
            get;
            private set;
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            AddLast(item);
        }

        public void AddAfter(LinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            AddBefore(node.Next, value);
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNewNode(newNode);
            AddBefore(node.Next, newNode);
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            LinkedListNode<T> result = new LinkedListNode<T>(node.List, value);
            AddBefore(node, result);
            return result;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ValidateNode(node);
            newNode.Next = node;
            newNode.Previous = node.Previous;
            node.Previous.Next = newNode;
            node.Previous = newNode;
            Count++;
            newNode.List = this;
        }

        public void AddFirst(T value)
        {
            AddAfter(sentinel, value);
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            ValidateNewNode(node);
            AddAfter(sentinel, node);
        }

        public void AddLast(T value)
        {
            AddBefore(sentinel, value);
        }

        public void AddLast(LinkedListNode<T> node)
        {
            ValidateNewNode(node);
            AddBefore(sentinel, node);
        }

        public void Clear()
        {
            var node = sentinel.Next;
            for (var temp = node; temp != sentinel; temp = node)
            {
                node = node.Next;
                temp.Invalidate();
            }

            sentinel.Next = sentinel;
            sentinel.Previous = sentinel;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(arrayIndex));
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Index outside bounds of array.");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Not enough space to copy Items to array.");
            }

            for (var node = sentinel.Next; node != sentinel; node = node.Next)
            {
                array[arrayIndex++] = node.Value;
            }
        }

        public LinkedListNode<T> Find(T value)
        {
            for (var node = sentinel.Next; node != sentinel; node = node.Next)
            {
                if (Equals(node.Value, value))
                {
                    return node;
                }
            }

            return null;
        }

        public LinkedListNode<T> FindLast(T value)
        {
            for (var node = sentinel.Previous; node != sentinel; node = node.Previous)
            {
                if (Equals(node.Value, value))
                {
                    return node;
                }
            }

            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var current = sentinel.Next; current != sentinel; current = current.Next)
            {
                yield return current.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(T item)
        {
            LinkedListNode<T> node = Find(item);
            if (node == null)
            {
                return false;
            }

            Remove(node);
            return true;
        }

        public void Remove(LinkedListNode<T> node)
        {
            ValidateNode(node);
            if (node.List != this)
            {
                throw new InvalidOperationException("Node does not belong to this list!");
            }

            if (sentinel.Next == sentinel)
            {
                throw new InvalidOperationException("Can not remove from a empty list.");
            }

            node.Next.Previous = node.Previous;
            node.Previous.Next = node.Next;

            node.Invalidate();
            Count--;
        }

        public void RemoveFirst()
        {
            Remove(sentinel.Next);
        }

        public void RemoveLast()
        {
            Remove(sentinel.Previous);
        }

        internal void ValidateNewNode(LinkedListNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.List == null)
            {
                return;
            }

            throw new InvalidOperationException("Node is not new.");
        }

        internal void ValidateNode(LinkedListNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.List == this)
            {
                return;
            }

            throw new InvalidOperationException("Node is not present on this list.");
        }
    }
}
