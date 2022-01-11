using DataCollections;
using Xunit;

namespace DataCollection.Tests
{
    public class ObjectArrayTests
    {
        [Fact]

        public void CanAddVarriedObjectsToObjectArray()
        {
            var testarray = new ObjectArray();

            testarray.Add("Hello");
            testarray.Add(1);
            testarray.Add(true);

            Assert.Equal("Hello", testarray[0]);
        }

        [Fact]

        public void CanCheckForAObjectWithContainsMethod()
        {
            var testarray = new ObjectArray();

            testarray.Add("Hello");
            testarray.Add(1);
            testarray.Add(true);

            Assert.True(testarray.Contains("Hello"));
            Assert.True(testarray.Contains(1));
            Assert.True(testarray.Contains(true));
        }

        [Fact]

        public void CanFindIndexOfAnElement()
        {
            var testarray = new ObjectArray();

            testarray.Add("Hello");
            testarray.Add(1);
            testarray.Add(true);

            Assert.Equal(0, testarray.IndexOf("Hello"));
            Assert.Equal(1, testarray.IndexOf(1));
            Assert.Equal(2, testarray.IndexOf(true));
        }
    }
}
