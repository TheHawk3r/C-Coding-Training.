using DataCollections;
using Xunit;

namespace DataCollection.Tests
{
    public class SortedIntArrayTests
    {
        [Fact]

        public void AfterTwoElementsAreAddedArrayIsSorted()
        {
            var testArray = new SortedIntArray();

            testArray.Add(5);
            testArray.Add(2);

            Assert.Equal(2, testArray[0]);
        }

        [Fact]

        public void AfterThreeElementsAreAddedArrayIsSorted()
        {
            var testArray = new SortedIntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(4);

            Assert.Equal(2, testArray[0]);
            Assert.Equal(4, testArray[1]);
        }

        [Fact]

        public void InsertingAnElementSortsTheArray()
        {
            var testArray = new SortedIntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(4);

            testArray.Insert(0, 10);

            Assert.Equal(2, testArray[0]);
            Assert.Equal(4, testArray[1]);
        }

        [Fact]

        public void ArrayIsProperlySorted()
        {
            var testArray = new SortedIntArray();

            testArray.Add(5);

            Assert.Equal(5, testArray[0]);
            Assert.Equal(0, testArray[1]);
        }

        [Fact]

        public void AfterSettingAnElementArrayIsSorted()
        {
            var testArray = new SortedIntArray();

            testArray.Add(12);
            testArray.Add(14);
            testArray[0] = 15;

            Assert.Equal(14, testArray[0]);
        }
    }
}
