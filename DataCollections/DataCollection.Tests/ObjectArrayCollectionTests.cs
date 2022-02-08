using DataCollections;
using Xunit;

namespace DataCollection.Tests
{
    public class ObjectArrayCollectionTests
    {
        [Fact]

        public void CanAddVarriedObjectsToObjectArray()
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(true);

            Assert.Equal("Hello", testArray[0]);
            Assert.Equal(true, testArray[2]);
        }

        [Fact]

        public void CanCheckForAObjectWithContainsMethod()
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(true);

            Assert.True(testArray.Contains("Hello"));
            Assert.True(testArray.Contains(1));
            Assert.True(testArray.Contains(true));
        }

        [Fact]

        public void CanFindIndexOfAnElement()
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(true);

            Assert.Equal(0, testArray.IndexOf("Hello"));
            Assert.Equal(1, testArray.IndexOf(1));
            Assert.Equal(2, testArray.IndexOf(true));
        }

        [Fact]

        public void SearchingForAnIndexOutsideOfArrayBoundsReturnsMinusOne()
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(true);

            Assert.Equal(-1, testArray[3]);
            Assert.Equal(-1, testArray[-1]);
            Assert.Equal(-1, testArray[4]);
        }

        [Fact]
        public void CanInsertAnElement()
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(false);

            testArray.Insert(1, 1.500);

            Assert.Equal(1.500, testArray[1]);
        }

        [Fact]
        public void CanClearArray()
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(false);

            testArray.Clear();

            Assert.Equal(0, testArray.Count);
            Assert.Equal(-1, testArray[0]);
        }

        [Fact]
        public void CanRemoveObject()
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(false);

            testArray.Remove("Hello");

            Assert.Equal(1, testArray[0]);
        }

        [Fact]
        public void CanRemoveObjectAtIndex()
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(false);

            testArray.RemoveAt(0);

            Assert.Equal(1, testArray[0]);
        }

        [Fact]
        public void CanNotRemoveObjectOutsideOfBounds()
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(false);

            testArray.RemoveAt(-1);

            Assert.Equal("Hello", testArray[0]);
            Assert.Equal(1, testArray[1]);
            Assert.Equal(false, testArray[2]);
            Assert.Equal(-1, testArray[-1]);
        }

        [Theory]
        [InlineData(9)]
        [InlineData("Hi")]
        [InlineData(null)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(1)]
        public void ShouldReturnFalseIfArrayDoesNotContainElement(object element)
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add(5);
            testArray.Add("Hello");
            testArray.Add(true);
            testArray.Add(false);

            Assert.False(testArray.Contains(element));
        }

        [Theory]
        [InlineData(5)]
        [InlineData("No")]
        [InlineData("Yes")]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldReturnTrueIfArrayContainsElement(object element)
        {
            var testArray = new ObjectArrayCollection();

            testArray.Add(5);
            testArray.Add("No");
            testArray.Add("Yes");
            testArray.Add(true);
            testArray.Add(false);

            Assert.True(testArray.Contains(element));
        }

        [Fact]
        public void CanIterateTroughObjects()
        {
            var testArray = new ObjectArrayCollection();
            int i = 0;

            testArray.Add(5);
            testArray.Add("No");
            testArray.Add("Yes");
            testArray.Add(true);
            testArray.Add(false);
            testArray.Add("HI");

            foreach (object o in testArray)
            {
                Assert.Equal(testArray[i], o);
                ++i;
            }
        }
    }
}
