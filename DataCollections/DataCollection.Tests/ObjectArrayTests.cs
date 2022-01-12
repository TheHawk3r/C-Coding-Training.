using DataCollections;
using Xunit;

namespace DataCollection.Tests
{
    public class ObjectArrayTests
    {
        [Fact]

        public void CanAddVarriedObjectsToObjectArray()
        {
            var testArray = new ObjectArray();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(true);

            Assert.Equal("Hello", testArray[0]);
        }

        [Fact]

        public void CanCheckForAObjectWithContainsMethod()
        {
            var testArray = new ObjectArray();

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
            var testArray = new ObjectArray();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(true);

            Assert.Equal(0, testArray.IndexOf("Hello"));
            Assert.Equal(1, testArray.IndexOf(1));
            Assert.Equal(2, testArray.IndexOf(true));
        }

        [Fact]
        public void CanInsertAnElement()
        {
            var testArray = new ObjectArray();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(false);

            testArray.Insert(1, 1.500);

            Assert.Equal(1.500, testArray[1]);
        }

        [Fact]
        public void CanClearArray()
        {
            var testArray = new ObjectArray();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(false);

            testArray.Clear();

            Assert.Null(testArray[0]);
            Assert.Null(testArray[1]);
        }

        [Fact]
        public void CanRemoveObject()
        {
            var testArray = new ObjectArray();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(false);

            testArray.Remove("Hello");

            Assert.Equal(1, testArray[0]);
        }

        [Fact]
        public void CanRemoveObjectAtIndex()
        {
            var testArray = new ObjectArray();

            testArray.Add("Hello");
            testArray.Add(1);
            testArray.Add(false);

            testArray.RemoveAt(0);

            Assert.Equal(1, testArray[0]);
        }
    }
}
