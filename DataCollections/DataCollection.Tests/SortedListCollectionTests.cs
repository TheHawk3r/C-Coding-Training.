using DataCollections;
using Xunit;

namespace DataCollection.Tests
{
    public class SortedListCollectionTests
    {
        [Fact]

        public void IntListShouldBeSorted()
        {
            var testArray = new SortedListCollection<int> { 2, 1, 0 };

            Assert.Equal(0, testArray[0]);
            Assert.Equal(1, testArray[1]);
            Assert.Equal(2, testArray[2]);
        }

        [Fact]

        public void CanInsertElement()
        {
            var testArray = new SortedListCollection<int> { 2, 1, 0 };

            testArray.Insert(1, 1);

            Assert.Equal(1, testArray[1]);
        }

        [Fact]

        public void ElementNotInsertedIfSortingNotRespected()
        {
            var testArray = new SortedListCollection<int> { 20, 15, 10 };

            testArray.Insert(1, 1);

            Assert.False(testArray.Contains(1));
        }

        [Fact]

        public void StringListShouldBeSorted()
        {
            var testArray = new SortedListCollection<string> { "C", "B", "A" };

            Assert.Equal("A", testArray[0]);
            Assert.Equal("B", testArray[1]);
            Assert.Equal("C", testArray[2]);
        }

        [Fact]

        public void BoolListShouldBeSorted()
        {
            var testArray = new SortedListCollection<bool> { true, false, true };

            Assert.False(testArray[0]);
            Assert.True(testArray[1]);
            Assert.True(testArray[2]);
        }
    }
}
