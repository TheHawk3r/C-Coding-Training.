using DataCollections;
using Xunit;

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
        public void TryingToGetObjectOutsideListBoundsShouldReturnTypeDefault()
        {
            var testList1 = new ListCollection<string> { "A", "B", "C" };

            Assert.Equal(default(string), testList1[3]);

            var testList2 = new ListCollection<int> { 1, 2, 3 };

            Assert.Equal(default(int), testList2[3]);

            var testList3 = new ListCollection<bool> { true, false, true };

            Assert.Equal(default(bool), testList3[3]);
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

        public void CanSwapTwoObjects()
        {
            ListCollection<int> testListOne = new ListCollection<int>() { 1, 2, 3 };
            ListCollection<int> testListTwo = new ListCollection<int>() { 4, 5, 6 };

            testListOne.SwapObjects<ListCollection<int>>(ref testListOne, ref testListTwo);

            Assert.Equal(4, testListOne[0]);
            Assert.Equal(1, testListTwo[0]);
        }
    }
}
