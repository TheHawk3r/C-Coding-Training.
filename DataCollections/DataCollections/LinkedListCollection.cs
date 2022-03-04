using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataCollections
{
    public class LinkedListCollection<T> : ICollection<T>
    {
        internal LinkedListNode<T> Head;

        public LinkedListCollection()
        {
        }

        public LinkedListCollection(ICollection<T> collection)
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

        public LinkedListNode<T> First => Head;

        public LinkedListNode<T> Last => Head?.PreviousNode;

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

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            LinkedListNode<T> result = new LinkedListNode<T>(node.List, value);
            InternalInsertNodeBefore(node.NextNode, result);
            return result;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNewNode(newNode);
            InternalInsertNodeBefore(node.NextNode, newNode);
            newNode.List = this;
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            LinkedListNode<T> result = new LinkedListNode<T>(node.List, value);
            InternalInsertNodeBefore(node, result);
            if (node == Head)
            {
                Head = result;
            }

            return result;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
            {
            ValidateNode(node);
            ValidateNewNode(newNode);
            InternalInsertNodeBefore(node, newNode);
            newNode.List = this;
            if (node != Head)
            {
                return;
            }

            Head = newNode;
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> result = new LinkedListNode<T>(this, value);
            if (Head == null)
            {
                InternalInsertNodeToEmptyList(result);
            }
            else
            {
                InternalInsertNodeBefore(Head, result);
                Head = result;
            }

            return result;
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            ValidateNewNode(node);

            if (Head == null)
            {
                InternalInsertNodeToEmptyList(node);
            }
            else
            {
                InternalInsertNodeBefore(Head, node);
                Head = node;
            }

            node.List = this;
        }

        public LinkedListNode<T> AddLast(T value)
        {
            LinkedListNode<T> result = new LinkedListNode<T>(this, value);
            if (Head == null)
            {
                InternalInsertNodeToEmptyList(result);
            }
            else
            {
                InternalInsertNodeBefore(Head, result);
            }

            return result;
        }

        public void AddLast(LinkedListNode<T> node)
        {
            ValidateNewNode(node);

            if (Head == null)
            {
                InternalInsertNodeToEmptyList(node);
            }
            else
            {
                InternalInsertNodeBefore(Head, node);
            }

            node.List = this;
        }

        public void Clear()
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                LinkedListNode<T> temp = current;
                current = current.Next;
                temp.Invalidate();
            }

            Head = null;
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

            LinkedListNode<T> node = Head;
            if (node == null)
            {
                return;
            }

            do
            {
                array[arrayIndex++] = node.Value;
                node = node.NextNode;
            }
            while (node != Head);
        }

        public LinkedListNode<T> Find(T value)
        {
            LinkedListNode<T> node = Head;
            EqualityComparer<T> c = EqualityComparer<T>.Default;
            if (node == null)
            {
                return null;
            }

            do
            {
                if (c.Equals(node.Value, value))
                {
                    return node;
                }

                node = node.NextNode;
            }
            while (node != Head);

            return null;
        }

        public LinkedListNode<T> FindLast(T value)
       {
            if (Head == null)
            {
                return null;
            }

            LinkedListNode<T> last = Head.PreviousNode;
            LinkedListNode<T> node = last;
            EqualityComparer<T> c = EqualityComparer<T>.Default;

            if (node == null)
            {
                return null;
            }

            do
            {
                if (c.Equals(node.Value, value))
                {
                    return node;
                }

                node = node.PreviousNode;
            }
            while (node != last);

            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = Head;
            for (int i = 0; i < Count; i++)
            {
                yield return current.Value;
                current = current.Next;
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

            InternalRemoveNode(node);
            return true;
        }

        public void Remove(LinkedListNode<T> node)
        {
            ValidateNode(node);
            InternalRemoveNode(node);
        }

        public void RemoveFirst()
        {
            if (Head == null)
            {
                throw new InvalidOperationException("Can not remove from a empty list.");
            }

            InternalRemoveNode(Head);
        }

        public void RemoveLast()
        {
            if (Head == null)
            {
                throw new InvalidOperationException("Can not remove from a empty list.");
            }

            InternalRemoveNode(Head.PreviousNode);
        }

        internal void InternalRemoveNode(LinkedListNode<T> node)
        {
            Debug.Assert(node.List == this, "Deleting the node from another list!");
            Debug.Assert(Head != null, "This method shouldn't be called on empty list!");
            if (node.NextNode == node)
            {
                Debug.Assert(Count == 1 && Head == node, "this should only be true for a list with only one node");
                Head = null;
            }
            else
            {
                node.NextNode.PreviousNode = node.PreviousNode;
                node.PreviousNode.NextNode = node.NextNode;
                if (Head == node)
                {
                    Head = node.NextNode;
                }
            }

            node.Invalidate();
            Count--;
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

        private void InternalInsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            newNode.NextNode = node;
            newNode.PreviousNode = node.PreviousNode;
            node.PreviousNode.NextNode = newNode;
            node.PreviousNode = newNode;
            Count++;
        }

        private void InternalInsertNodeToEmptyList(LinkedListNode<T> newNode)
        {
            Debug.Assert(Head == null && Count == 0, "LinkedList must be empty when this method is called!");
            newNode.NextNode = newNode;
            newNode.PreviousNode = newNode;
            Head = newNode;
            Count++;
        }
    }
}
