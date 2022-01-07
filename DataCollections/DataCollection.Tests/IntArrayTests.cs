using DataCollections;
using Xunit;

namespace DataCollection.Tests
{
    public class IntArrayTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(2)]
        public void CanAddElements(int element)
        {
            var testArray = new IntArray();

            testArray.Add(element);

            Assert.Equal(element, testArray[testArray.Count - 1]);
        }

        [Fact]
        public void CanReturnCountOfAThreeIntegerArray()
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(10);

            Assert.Equal(3, testArray.Count);
        }

        [Fact]
        public void CanReturnCountOfAFiveIntegerArray()
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(10);
            testArray.Add(4);
            testArray.Add(0);

            Assert.Equal(5, testArray.Count);
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 10)]
        [InlineData(3, 4)]
        [InlineData(4, 0)]
        public void CanReturnElementFromAGivenIndex(int index, int elementReturned)
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(10);
            testArray.Add(4);
            testArray.Add(0);

            Assert.Equal(elementReturned, testArray[index]);
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 1)]
        [InlineData(2, 4)]
        [InlineData(3, 8)]
        [InlineData(4, 9)]
        public void CanModifyElementFromAGivenIndex(int index, int elementToSet)
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(10);
            testArray.Add(4);
            testArray.Add(0);

            testArray[index] = elementToSet;

            Assert.Equal(elementToSet, testArray[index]);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(2)]
        [InlineData(10)]
        [InlineData(4)]
        [InlineData(0)]
        public void ShouldReturnTrueIfArrayContainsElement(int element)
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(10);
            testArray.Add(4);
            testArray.Add(0);

            Assert.True(testArray.Contains(element));
        }

        [Theory]
        [InlineData(9)]
        [InlineData(8)]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(11)]
        public void ShouldReturnFalseIfArrayDoesNotContainElement(int element)
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(10);
            testArray.Add(4);
            testArray.Add(0);

            Assert.False(testArray.Contains(element));
        }

        [Theory]
        [InlineData(5, 0)]
        [InlineData(2, 1)]
        [InlineData(10, 2)]
        [InlineData(4, 3)]
        [InlineData(0, 4)]
        public void ShouldReturnTheIndexOfAElementIfPresent(int element, int index)
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(10);
            testArray.Add(4);
            testArray.Add(0);

            Assert.Equal(index, testArray.IndexOf(element));
        }

        [Theory]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void ShouldReturnMinusOneIfTheElementIsNotPresent(int element)
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(10);
            testArray.Add(4);
            testArray.Add(0);

            Assert.Equal(-1, testArray.IndexOf(element));
        }

        [Theory]
        [InlineData(4, 0, 5)]
        [InlineData(1, 1, 2)]
        [InlineData(3, 2, 10)]
        [InlineData(12, 3, 4)]
        [InlineData(21, 4, 0)]
        public void CanInsertElements(int element, int index, int nextElement)
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(10);
            testArray.Add(4);

            testArray.Insert(index, element);

            Assert.Equal(nextElement, testArray[index + 1]);
            Assert.Equal(element, testArray[index]);
        }

        [Fact]
        public void CanClearArray()
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(2);
            testArray.Add(10);
            testArray.Add(4);
            testArray.Add(9);

            testArray.Clear();
            Assert.Equal(0, testArray.Count);
            Assert.Equal(0, testArray[3]);
        }

        [Theory]
        [InlineData(5, 2)]
        [InlineData(4, 3)]
        [InlineData(10, 4)]

        public void CanRemoveFirstFoundElement(int element, int indexOfElement)
        {
            var testArray = new IntArray();

            testArray.Add(5);
            testArray.Add(4);
            testArray.Add(10);
            testArray.Add(5);
            testArray.Add(4);
            testArray.Add(10);

            testArray.Remove(element);

            Assert.Equal(indexOfElement, testArray.IndexOf(element));
        }

        [Theory]
        [InlineData(1, -1)]
        [InlineData(2, -1)]
        [InlineData(3, -1)]
        [InlineData(4, -1)]
        [InlineData(5, -1)]
        [InlineData(6, -1)]

        public void CanRemoveElementAtGivenIndex(int element, int indexOfElement)
        {
            var testArray = new IntArray();

            testArray.Add(1);
            testArray.Add(2);
            testArray.Add(3);
            testArray.Add(4);
            testArray.Add(5);
            testArray.Add(6);

            testArray.Remove(element);

            Assert.Equal(indexOfElement, testArray.IndexOf(element));
        }

        [Fact]
        public void IfAnElementIsRemovedAndCountOfElementsIsThePreviousArrayLengthArrayGetsRealocatedToThatLengthSmallArray()
        {
            var testArray = new IntArray();

            testArray.Add(1);
            testArray.Add(2);
            testArray.Add(3);
            testArray.Add(4);
            testArray.Add(5);
            testArray.Add(6);

            testArray.RemoveAt(0);
            testArray.RemoveAt(1);

            Assert.Equal(-1, testArray.IndexOf(0));
        }

        [Fact]
        public void IfAnElementIsRemovedAndCountOfElementsIsThePreviousArrayLengthArrayGetsRealocatedToThatLengthLargeArray()
        {
            var testArray = new IntArray();

            testArray.Add(1);
            testArray.Add(2);
            testArray.Add(3);
            testArray.Add(4);
            testArray.Add(5);
            testArray.Add(6);
            testArray.Add(7);
            testArray.Add(8);
            testArray.Add(9);

            testArray.RemoveAt(8);

            Assert.Equal(-1, testArray.IndexOf(0));
        }
    }
}
