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

        public void CanInsertAnElement()
        {
            var testArray = new ObjectArray();
        }
    }
}
