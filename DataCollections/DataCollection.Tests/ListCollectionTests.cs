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
    }
}
