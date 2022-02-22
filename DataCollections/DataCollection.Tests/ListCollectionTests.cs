using DataCollections;
using Xunit;
using System;

namespace DataCollection.Tests
{
    public class ListCollectionTests
    {
        [Fact]

        public void CanCreateAIntList()
        {
            var testList = new ListCollection<int> { 1, 2, 3 };

            Assert.IsType<int>(testList[0]);
            Assert.IsType<int>(testList[1]);
            Assert.IsType<int>(testList[2]);
        }

        [Fact]
        public void CanCreateABoolList()
        {
            var testList = new ListCollection<bool> { true, false, true };

            Assert.IsType<bool>(testList[0]);
            Assert.IsType<bool>(testList[1]);
            Assert.IsType<bool>(testList[2]);
        }

        [Fact]
        public void CanCreateAStringList()
        {
            var testList = new ListCollection<string> { "A", "B", "C" };

            Assert.IsType<string>(testList[0]);
            Assert.IsType<string>(testList[1]);
            Assert.IsType<string>(testList[2]);
        }

        [Fact]
        public void TryingToGetItemOutsideListBoundsShouldThrowArgumentOutOfBoundsException()
        {
            var testList1 = new ListCollection<string> { "A", "B", "C" };

            Assert.Throws<ArgumentOutOfRangeException>(() => testList1[3]);

            var testList2 = new ListCollection<int> { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>(() => testList2[3]);

            var testList3 = new ListCollection<bool> { true, false, true };

            Assert.Throws<ArgumentOutOfRangeException>(() => testList3[3]);
        }

        [Fact]
        public void TryingToSetItemOutsideListBoundsShouldThrowArgumentOutOfBoundsException()
        {
            var testList1 = new ListCollection<string> { "A", "B", "C" };

            Assert.Throws<ArgumentOutOfRangeException>(() => testList1[3] = "D");

            var testList2 = new ListCollection<int> { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>(() => testList2[3] = 4);

            var testList3 = new ListCollection<bool> { true, false, true };

            Assert.Throws<ArgumentOutOfRangeException>(() => testList3[3] = false);
        }

        [Fact]

        public void TryingToInsertAItemOutsideBoundsOfListShouldThrowArgumentOutOfBoundsException()
        {
            var testList = new ListCollection<string> { "A", "B", "C" };

            Assert.Throws<ArgumentOutOfRangeException>(() => testList.Insert(4, "D"));
        }

        [Fact]

        public void TryingToRemoveAnItemThatIsNotPresentInTheListShouldThrowArgumentException()
        {
            var testList = new ListCollection<string> { "A", "B", "C" };

            Assert.Throws<ArgumentException>(() => testList.Remove("D"));
        }

        [Fact]

        public void TryingToRemoveAnItemAtAIndexOutsideBoundsOfListShouldThrowArgumentOutOfBoundsException()
        {
            var testList = new ListCollection<string> { "A", "B", "C" };

            Assert.Throws<ArgumentOutOfRangeException>(() => testList.RemoveAt(3));
        }

        [Fact]
        public void TryingToCopyListToANullArrayShouldThrowArgumentNullException()
        {
            var testList = new ListCollection<string> { "A", "B", "C" };
            string[] testArray = null;

            Assert.Throws<ArgumentNullException>(() => testList.CopyTo(testArray, 0));
        }

        [Fact]
        public void TryingToCopyListToSmallerArrayShouldThrowArgumentException()
        {
            var testList = new ListCollection<string> { "A", "B", "C" };
            string[] testArrayOne = new string[2];
            string[] testArrayTwo = new string[5];

            Assert.Throws<ArgumentException>(() => testList.CopyTo(testArrayOne, 1));
            Assert.Throws<ArgumentException>(() => testList.CopyTo(testArrayTwo, 3));
        }

        [Fact]
        public void TryingToCopyListToArrrayAtANegativeIndexShouldThrowArgumentOutOfRangeException()
        {
            var testList = new ListCollection<string> { "A", "B", "C" };
            string[] testArrayOne = new string[2];

            Assert.Throws<ArgumentOutOfRangeException>(() => testList.CopyTo(testArrayOne, -1));
        }

        [Fact]

        public void CanCopyListArrayToAnotherArray()
        {
            var testList = new ListCollection<string> { "A", "B", "C" };
            var testArray = new string[4];

            testList.CopyTo(testArray, 0);

            Assert.Equal("A", testArray[0]);
            Assert.Equal("B", testArray[1]);
            Assert.Equal("C", testArray[2]);
        }

        [Fact]
        public void CanIterateTroughList()
        {
            int i = 0;
            var testList = new ListCollection<int> { 1, 2, 3 };

            foreach (int n in testList)
            {
                Assert.Equal(testList[i++], n);
            }
        }

        [Fact]
        public void CanSwapObjectItems()
        {
            var testList = new ListCollection<object> { 1, true, "A" };

            testList.SwapItems(1, true);

            Assert.Equal(true, testList[0]);
            Assert.Equal(1, testList[1]);
        }

        [Fact]
        public void CanSwapStringItems()
        {
            var testList = new ListCollection<string> { "A", "B", "C" };

            testList.SwapItems("A", "B");

            Assert.Equal("B", testList[0]);
            Assert.Equal("A", testList[1]);
        }

        [Fact]
        public void CanSwapInttItems()
        {
            var testList = new ListCollection<int> { 1, 2, 3 };

            testList.SwapItems(1, 2);

            Assert.Equal(2, testList[0]);
            Assert.Equal(1, testList[1]);
        }

        [Fact]
        public void CanSwapCharItems()
        {
            var testList = new ListCollection<char> { 'a', 'b', 'c' };

            testList.SwapItems('a', 'b');

            Assert.Equal('b', testList[0]);
            Assert.Equal('a', testList[1]);
        }

        [Fact]

        public void CanSwapTwoListObjectsOfSameType()
        {
            ListCollection<int> testListOne = new ListCollection<int>() { 1, 2, 3 };
            ListCollection<int> testListTwo = new ListCollection<int>() { 4, 5, 6 };

            testListOne.SwapObjects<ListCollection<int>>(ref testListOne, ref testListTwo);

            Assert.Equal(4, testListOne[0]);
            Assert.Equal(1, testListTwo[0]);
        }

        [Fact]

        public void CanSwapTwoListObjectsOfObjectType()
        {
            ListCollection<object> testListOne = new ListCollection<object>() { 1, null, false };
            ListCollection<object> testListTwo = new ListCollection<object>() { 4, true, "H" };

            testListOne.SwapObjects<ListCollection<object>>(ref testListOne, ref testListTwo);

            Assert.Equal(4, testListOne[0]);
            Assert.Equal(1, testListTwo[0]);
        }
    }
}
