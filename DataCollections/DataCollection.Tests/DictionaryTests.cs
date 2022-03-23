using System;
using DataCollections;
using Xunit;

namespace DataCollection.Tests
{
    public class DictionaryTests
    {
        [Fact]
        public void CanInitializeADictionary()
        {
            var testDictionaryOne = new Dictionary<string, int>();
            var testDictionaryTwo = new Dictionary<string, int>(5);

            testDictionaryTwo.Add("A", 1);

            Assert.Empty(testDictionaryOne);
            Assert.Equal(1, testDictionaryTwo["A"]);
        }
    }
}
