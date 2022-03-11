using DataCollections;
using System;
using Xunit;

namespace DataCollection.Tests
{
    public class LinkedListCollectionTests
    {
        [Fact]

        public void CanInitializeALinkedList()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(1);

            Assert.Single(testList);
            Assert.Equal(1, testList.First.Value);
        }

        [Fact]
        public void CanInitializeALinkedListWithAnootherICollectionObject()
        {
            var testListOne = new ListCollection<int>() { 1, 2, 3 };
            var testListTwo = new LinkedListCollection<int>(testListOne);

            Assert.Equal(3, testListTwo.Count);
            Assert.Equal(1, testListTwo.First.Value);
            Assert.Equal(2, testListTwo.First.Next.Value);
            Assert.Equal(3, testListTwo.Last.Value);
        }

        [Fact]
        public void InitializingALinkedListWhitANullCollectionThrwosArgumentNullException()
        {
            ListCollection<int> testListOne = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new LinkedListCollection<int>(testListOne));
        }

        [Fact]
        public void CanAddNewItemsToLinkedList()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            Assert.Equal(4, testList.Count);
            Assert.Equal(1, testList.First.Value);
            Assert.Equal(2, testList.First.Next.Value);
            Assert.Equal(3, testList.Last.Previous.Value);
            Assert.Equal(4, testList.Last.Value);
        }

        [Fact]
        public void CanAddNewValueAfterExistingNodeInLinkedList()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(1);
            testList.Add(2);
            testList.AddAfter(testList.First, 3);

            Assert.Equal(1, testList.First.Value);
            Assert.Equal(2, testList.Last.Value);
            Assert.Equal(3, testList.First.Next.Value);
            Assert.Equal(3, testList.Count);
        }

        [Fact]
        public void CanAddNewNodeAfterExistingNodeInLinkedList()
        {
            var testList = new LinkedListCollection<int>();
            var newNode = new LinkedListNode<int>(3);

            testList.Add(1);
            testList.Add(2);
            testList.AddAfter(testList.First, newNode);

            Assert.Equal(3, testList.First.Next.Value);
            Assert.Equal(3, testList.Count);
        }

        [Fact]
        public void CanAddNewValueBeforeExistingNodeInLinkedList()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(1);
            testList.Add(2);
            testList.AddBefore(testList.First, 3);

            Assert.Equal(3, testList.First.Value);
            Assert.Equal(3, testList.Count);
        }

        [Fact]
        public void CanAddNewNodeBeforeExistingNodeInLinkedList()
        {
            var testList = new LinkedListCollection<int>();
            var newNode = new LinkedListNode<int>(3);

            testList.Add(1);
            testList.Add(2);
            testList.AddBefore(testList.First, newNode);

            Assert.Equal(3, testList.First.Value);
            Assert.Equal(3, testList.Count);
        }

        [Fact]
        public void CanAddNewValueAtStartOfLinkedList()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(1);
            testList.Add(2);
            testList.AddFirst(3);

            Assert.Equal(3, testList.First.Value);
            Assert.Equal(3, testList.Count);
        }

        [Fact]
        public void CanAddNewNodeAtStartOfLinkedList()
        {
            var testList = new LinkedListCollection<int>();
            var newNode = new LinkedListNode<int>(3);

            testList.Add(1);
            testList.Add(2);
            testList.AddFirst(newNode);

            Assert.Equal(3, testList.First.Value);
            Assert.Equal(3, testList.Count);
        }

        [Fact]
        public void CanAddNewValueAtEndOfLinkedList()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(1);
            testList.Add(2);
            testList.AddLast(3);

            Assert.Equal(3, testList.Last.Value);
            Assert.Equal(3, testList.Count);
        }

        [Fact]
        public void CanAddNewNodeAtEndOfLinkedList()
        {
            var testList = new LinkedListCollection<int>();
            var newNode = new LinkedListNode<int>(3);

            testList.Add(1);
            testList.Add(2);
            testList.AddLast(newNode);

            Assert.Equal(3, testList.Last.Value);
            Assert.Equal(3, testList.Count);
        }

        [Fact]

        public void CanClearLinkedList()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Clear();

            Assert.Empty(testList);
        }

        [Fact]
        public void CanDetermineIfAValueIsInTheList()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            Assert.Contains(3, testList);
        }

        [Fact]
        public void CanCopyValuesToAnArray()
        {
            var testList = new LinkedListCollection<int>();
            int[] array = new int[2];
            testList.Add(1);
            testList.Add(2);

            testList.CopyTo(array, 0);

            Assert.Equal(1, array[0]);
            Assert.Equal(2, array[1]);
        }

        [Fact]
        public void CanCopyValuesToAnArrayAtArrayInde()
        {
            var testList = new LinkedListCollection<int>();
            int[] array = new int[4];
            testList.Add(1);
            testList.Add(2);

            testList.CopyTo(array, 2);

            Assert.Equal(1, array[2]);
            Assert.Equal(2, array[3]);
        }

        [Fact]
        public void IfArrayIsNullCopyToThrowsArgumentNullException()
        {
            var testList = new LinkedListCollection<int>();
            int[] array = null;
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            Assert.Throws<ArgumentNullException>(() => testList.CopyTo(array, 0));
        }

        [Fact]
        public void IfArrayIndexIsSmallerThenZeroOrBiggerOrEqualToArrayLenghtThenCopyToThrowsArgumentNullException()
        {
            var testList = new LinkedListCollection<int>();
            int[] array = new int[4];
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            Assert.Throws<ArgumentOutOfRangeException>(() => testList.CopyTo(array, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => testList.CopyTo(array, 4));
        }

        [Fact]
        public void IfSpaceFromArrayindexIsSmallerThenCountCopyToThrwosArgumentExxception()
        {
            var testList = new LinkedListCollection<int>();
            int[] array = new int[4];
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            Assert.Throws<ArgumentException>(() => testList.CopyTo(array, 2));
        }

        [Fact]
        public void IfLinkedListIsEmptyNothingWillBeCopiedtoTheArray()
        {
            var testList = new LinkedListCollection<int>();
            int[] array = new int[4];

            testList.CopyTo(array, 0);

            Assert.Equal(0, array[0]);
            Assert.Equal(0, array[1]);
            Assert.Equal(0, array[2]);
            Assert.Equal(0, array[3]);
        }

        [Fact]
        public void IfLinkedListIsEmptyFindMethodWillReturnNull()
        {
            var testList = new LinkedListCollection<int>();

            Assert.Null(testList.Find(5));
        }

        [Fact]
        public void CanFindNodeWithGivenValue()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Add(5);

            Assert.Equal(testList.Last, testList.Find(5));
            Assert.Equal(testList.Last.Previous, testList.Find(4));
            Assert.Equal(testList.Last.Previous.Previous, testList.Find(3));
            Assert.Equal(testList.First.Next, testList.Find(2));
            Assert.Equal(testList.First, testList.Find(1));
        }

        [Fact]
        public void CanFindNullValues()
        {
            var testList = new LinkedListCollection<string>();

            testList.Add("A");
            testList.Add("B");
            testList.Add("C");
            testList.Add("D");
            testList.Add(null);

            Assert.Equal(testList.Last, testList.Find(null));
        }

        [Fact]
        public void CanFindLastNodeWithGivenValue()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(1);
            testList.Add(1);
            testList.Add(2);
            testList.Add(2);
            testList.Add(3);
            testList.Add(3);
            testList.Add(4);
            testList.Add(4);
            testList.Add(5);
            testList.Add(5);

            Assert.Equal(testList.Last, testList.FindLast(5));
            Assert.Equal(testList.Last.Previous.Previous, testList.FindLast(4));
            Assert.Equal(testList.Last.Previous.Previous.Previous.Previous, testList.FindLast(3));
            Assert.Equal(testList.First.Next.Next.Next, testList.FindLast(2));
            Assert.Equal(testList.First.Next, testList.FindLast(1));
        }

        [Fact]

        public void CanIterateThroughItemsOfLinkedList()
        {
            var testList = new LinkedListCollection<int>();
            var array = new[] { 2, 3, 4, 5, 5 };
            int i = 0;

            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Add(5);
            testList.Add(5);

            foreach (int n in testList)
            {
                Assert.Equal(n, array[i++]);
            }
        }

        [Fact]
        public void CanRemoveNodeBasedOnGivenValue()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Add(5);
            testList.Add(5);

            Assert.True(testList.Remove(2));
            Assert.Equal(3, testList.First.Value);
        }

        [Fact]
        public void CanRemoveNodeBasedOnGivenNode()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Add(5);
            testList.Add(5);

            testList.Remove(testList.First.Next);

            Assert.Equal(4, testList.First.Next.Value);
        }

        [Fact]

        public void IfNodeIsNullRemoveWillThrowArgumentNullException()
        {
            var testList = new LinkedListCollection<int>();
            LinkedListNode<int> node = null;

            var exception = Assert.Throws<ArgumentNullException>(() => testList.Remove(node));
        }

        [Fact]

        public void IfNodeIsNotPresentOnTheListRemoveWillThrowInvalidOperationException()
        {
            var testList = new LinkedListCollection<int>();
            LinkedListNode<int> node = new LinkedListNode<int>(2);

            var exception = Assert.Throws<InvalidOperationException>(() => testList.Remove(node));
        }

        [Fact]

        public void CanRemoveFirstNode()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(2);

            testList.RemoveFirst();

            Assert.Empty(testList);
        }

        [Fact]

        public void IfListIsEmptyRemoveFirstThrowsInvalidOperationExcetiopn()
        {
            var testList = new LinkedListCollection<int>();

            var exception = Assert.Throws<InvalidOperationException>(() => testList.RemoveFirst());
        }

        [Fact]
        public void CanRemoveLastNode()
        {
            var testList = new LinkedListCollection<int>();

            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Add(5);

            testList.RemoveLast();

            Assert.Equal(4, testList.Last.Value);
        }

        [Fact]

        public void IfListIsEmptyRemoveLastThrowsInvalidOperationExcetiopn()
        {
            var testList = new LinkedListCollection<int>();

            var exception = Assert.Throws<InvalidOperationException>(() => testList.RemoveLast());
        }

        [Fact]

        public void AddingAValueAfterANodeThatDoesNotBelongToTheListThrowsArgumentNullException()
        {
            var testList = new LinkedListCollection<int>();

            Assert.Throws<InvalidOperationException>(() => testList.AddAfter(new LinkedListNode<int>(3), 1));
        }

        [Fact]

        public void AddingANullNodeAfterANodeOfTheListThrowsArgumentNullException()
        {
            var testList = new LinkedListCollection<int>() { 1 };

            Assert.Throws<ArgumentNullException>(() => testList.AddAfter(testList.First, null));
        }

        [Fact]

        public void AddingANodeOfAnotherLinkedListAfterANodeOfTheListThrowsArgumentNullException()
        {
            var testListOne = new LinkedListCollection<int>() { 1 };
            var testListTwo = new LinkedListCollection<int>() { 2 };

            Assert.Throws<InvalidOperationException>(() => testListOne.AddAfter(testListOne.First, testListTwo.First));
        }

        [Fact]

        public void LinkedListWithOneNodePrevioousPropertyShouldReturnSentinelValue()
        {
            var testListOne = new LinkedListCollection<int> { 1 };

            Assert.Equal(0, testListOne.First.Previous.Value);
        }

        [Fact]

        public void LinkedListWithOneNodeNextPropertyShouldReturnSentinelValue()
        {
            var testListOne = new LinkedListCollection<int> { 1 };

            Assert.Equal(0, testListOne.First.Next.Value);
        }

        [Fact]

        private void CanHaveAListWithMultipleNodes()
        {
            var testListOne = new LinkedListCollection<int> { 2, 3, 4, 5, 6, 7 };

            Assert.Equal(2, testListOne.First.Value);
            Assert.Equal(3, testListOne.First.Next.Value);
            Assert.Equal(4, testListOne.First.Next.Next.Value);
            Assert.Equal(5, testListOne.Last.Previous.Previous.Value);
            Assert.Equal(6, testListOne.Last.Previous.Value);
        }

        [Fact]

        private void EmptyLinkedListFirstAndLastPropertiesReturnNull()
        {
            var testListOne = new LinkedListCollection<int>();

            Assert.Null(testListOne.First);
            Assert.Null(testListOne.Last);
        }
    }
}
