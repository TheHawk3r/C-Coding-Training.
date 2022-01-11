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

        public void IfAnElementIsSetOnTheFisrtIndexAndIsBiggerThenTheNextElementItIsNotSet()
        {
            var testArray = new SortedIntArray();

            testArray.Add(3);
            testArray.Add(2);
            testArray[0] = 4;
            Assert.Equal(2, testArray[0]);
        }

        [Fact]

        public void IfAnElementIsSetOnTheLastIndexAndIsSmallerThenThePreviousElementItIsNotSet()
        {
            var testArray = new SortedIntArray();

            testArray.Add(3);
            testArray.Add(2);
            testArray[testArray.Count - 1] = 1;
            Assert.Equal(3, testArray[testArray.Count - 1]);
        }

        [Fact]

        public void IfAnElementIsSetAndIsBiggerThenTheNextElementAndSmallerThenThePreviousElementItIsNotSet()
        {
            var testArray = new SortedIntArray();

            testArray.Add(3);
            testArray.Add(2);
            testArray.Add(5);
            testArray.Add(6);
            testArray[2] = 1;
            Assert.Equal(5, testArray[2]);
        }

        [Fact]

        public void IfAnElementIsInsertedAndIsBiggerThenElementWhereItIsInsertedAndSmallerThenThePreviousElementItIsNotInserted()
        {
            var testArray = new SortedIntArray();

            testArray.Add(3);
            testArray.Add(2);
            testArray.Add(5);
            testArray.Add(6);
            testArray.Insert(1, 1);
            Assert.Equal(3, testArray[1]);
        }

        [Fact]

        public void IfAnElementIsInsertedOnTheFirstIndexAndIsBiggerThenTheElementWhereItIsInsertedItIsNotInserted()
        {
            var testArray = new SortedIntArray();

            testArray.Add(4);
            testArray.Add(3);
            testArray.Add(5);
            testArray.Add(6);
            testArray.Insert(0, 7);
            Assert.Equal(3, testArray[0]);
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
            testArray.Add(2);
            testArray.Add(4);
            testArray.Add(1);

            Assert.Equal(1, testArray[0]);
            Assert.Equal(2, testArray[1]);
            Assert.Equal(4, testArray[2]);
            Assert.Equal(5, testArray[3]);
        }
    }
}
