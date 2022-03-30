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
            var test = testDictionaryTwo.Keys;
            testDictionaryTwo.Add("B", 2);

            Assert.Empty(testDictionaryOne);
            Assert.Equal(1, testDictionaryTwo["A"]);
            Assert.True(testDictionaryTwo.Keys.Contains("A"));
        }

        [Fact]
        public void KeysPropertyShouldReturnAnICollectionContainingTheDictionaryKeys()
        {
            var testDictionary = new Dictionary<string, int>(3);

            testDictionary.Add("A", 1);
            testDictionary.Add("B", 2);
            testDictionary.Add("C", 3);
            ListCollection<string> testList = (ListCollection<string>)testDictionary.Keys;

            Assert.Contains("A", testList);
            Assert.Contains("B", testList);
            Assert.Contains("C", testList);
            Assert.Equal("A", testList[0]);
            Assert.Equal("B", testList[1]);
            Assert.Equal("C", testList[2]);
        }

        [Fact]
        public void ValuesPropertyShouldReturnAnICollectionContainingTheDictionaryValues()
        {
            var testDictionary = new Dictionary<string, int>(3);

            testDictionary.Add("A", 1);
            testDictionary.Add("B", 2);
            testDictionary.Add("C", 3);
            ListCollection<int> testList = (ListCollection<int>)testDictionary.Values;

            Assert.Contains(1, testList);
            Assert.Contains(2, testList);
            Assert.Contains(3, testList);
            Assert.Equal(1, testList[0]);
            Assert.Equal(2, testList[1]);
            Assert.Equal(3, testList[2]);
        }

        [Fact]
        public void CountPropertyShouldReturnCountOfEntriesInDictionary()
        {
            var testDictionary = new Dictionary<string, int>(3);
            Assert.Empty(testDictionary);
            testDictionary.Add("A", 1);
            Assert.Single(testDictionary);
            testDictionary.Add("B", 2);
            Assert.Equal(2, testDictionary.Count);
            testDictionary.Clear();
            Assert.Empty(testDictionary);
        }

        [Fact]
        public void KeyGetAccessorShouldReturnDefaultValueOfTypeIfKeyIsNotFoundInDictionary()
        {
            var testDcitionary = new Dictionary<int, string>(5);
            testDcitionary[1] = "A";
            testDcitionary[2] = "B";
            testDcitionary[3] = "C";
            var testString = testDcitionary[4];

            Assert.Equal(default(string), testString);
        }

        [Fact]
        public void KeyGetAccessorShouldReturnTheKeysValue()
        {
            var testDcitionary = new Dictionary<int, string>(5);
            testDcitionary[1] = "A";
            testDcitionary[2] = "B";
            testDcitionary[3] = "C";

            Assert.Equal("A", testDcitionary[1]);
            Assert.Equal("B", testDcitionary[2]);
            Assert.Equal("C", testDcitionary[3]);
        }

        [Fact]
        public void SettingAKeyThatIsNotPresentShouldAddTheKeyValuePairToTheDictionary()
        {
            var testDcitionary = new Dictionary<int, string>(5);
            testDcitionary[1] = "X";
            testDcitionary[2] = "Y";
            testDcitionary[3] = "Z";

            Assert.Equal("X", testDcitionary[1]);
            Assert.Equal("Y", testDcitionary[2]);
            Assert.Equal("Z", testDcitionary[3]);
        }

        [Fact]
        public void SettingAnExistingKeyShouldOverwriteItsExistingValue()
        {
            var testDcitionary = new Dictionary<int, string>(5);
            testDcitionary.Add(1, "X");
            testDcitionary.Add(2, "Y");
            testDcitionary.Add(3, "Z");
#pragma warning disable S4143 // Collection elements should not be replaced unconditionally
            testDcitionary[1] = "A";
            testDcitionary[2] = "B";
            testDcitionary[3] = "C";
#pragma warning restore S4143 // Collection elements should not be replaced unconditionally

            Assert.Equal("A", testDcitionary[1]);
            Assert.Equal("B", testDcitionary[2]);
            Assert.Equal("C", testDcitionary[3]);
        }

        [Fact]
        public void AddFunctionShouldWorkProperlyWithBucketsAndElementsUsedProperly()
        {
            var testDcitionary = new Dictionary<int, int>(5);

            testDcitionary.Add(1, 10);
            testDcitionary.Add(2, 20);
            testDcitionary.Add(3, 30);
            testDcitionary.Add(4, 40);
            testDcitionary.Add(5, 50);

            Assert.Equal(10, testDcitionary[1]);
            Assert.Equal(20, testDcitionary[2]);
            Assert.Equal(30, testDcitionary[3]);
            Assert.Equal(40, testDcitionary[4]);
            Assert.Equal(50, testDcitionary[5]);
        }

        [Fact]
        public void AddFunctionShouldThrowAnArgumentNullExceptionIfTheKeyIsNull()
        {
            var testDcitionary = new Dictionary<string, int>(1);

            Assert.Throws<ArgumentNullException>(() => testDcitionary.Add(null, 10));
        }

        [Fact]
        public void AddFunctionShouldThrowAnInvalidOpertionExceptionIfKeyIsAlreadyPresent()
        {
            var testDcitionary = new Dictionary<string, int>(1);

            testDcitionary.Add("A", 10);

            Assert.Throws<InvalidOperationException>(() => testDcitionary.Add("A", 20));
        }

        [Fact]
        public void AddFunctionShouldUseFreeSpacesIfElementsHaveBeenRemoved()
        {
            var testDictionary = new Dictionary<string, int>();

            testDictionary.Add("A", 10);
            testDictionary.Add("B", 20);
            testDictionary.Add("C", 30);
            testDictionary.Remove("B");
            testDictionary.Remove("C");
            testDictionary.Add("D", 40);

            Assert.Equal(2, testDictionary.Count);
        }
    }
}
