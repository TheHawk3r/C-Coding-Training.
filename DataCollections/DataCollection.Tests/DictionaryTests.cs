using System;
using DataCollections;
using Xunit;
using System.Collections.Generic;

namespace DataCollection.Tests
{
    public class DictionaryTests
    {
        [Fact]
        public void CanInitializeADictionary()
        {
            var testDictionaryOne = new DataCollections.Dictionary<string, int>();
            var testDictionaryTwo = new DataCollections.Dictionary<string, int>(5);

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
            var testDictionary = new DataCollections.Dictionary<string, int>(3);

            testDictionary.Add("A", 1);
            testDictionary.Add("B", 2);
            testDictionary.Add("C", 3);
            ListCollection<string> testList = (ListCollection<string>)testDictionary.Keys;

            Assert.Contains("A", testList);
            Assert.Contains("B", testList);
            Assert.Contains("C", testList);
        }

        [Fact]
        public void ValuesPropertyShouldReturnAnICollectionContainingTheDictionaryValues()
        {
            var testDictionary = new DataCollections.Dictionary<string, int>(3);

            testDictionary.Add("A", 1);
            testDictionary.Add("B", 2);
            testDictionary.Add("C", 3);
            ListCollection<int> testList = (ListCollection<int>)testDictionary.Values;

            Assert.Contains(1, testList);
            Assert.Contains(2, testList);
            Assert.Contains(3, testList);
        }

        [Fact]
        public void CountPropertyShouldReturnCountOfEntriesInDictionary()
        {
            var testDictionary = new DataCollections.Dictionary<string, int>(3);
            Assert.Empty(testDictionary);
            testDictionary.Add("A", 1);
            Assert.Single(testDictionary);
            testDictionary.Add("B", 2);
            Assert.Equal(2, testDictionary.Count);
            testDictionary.Clear();
            Assert.Empty(testDictionary);
        }

        [Fact]
        public void KeyGetAccessorShouldThrowKeyNotFoundExceptionIfKeyIsNotPresent()
        {
            var testDcitionary = new DataCollections.Dictionary<int, string>(5);
            testDcitionary[1] = "A";
            testDcitionary[2] = "B";
            testDcitionary[3] = "C";

            Assert.Throws<KeyNotFoundException>(() => testDcitionary[4]);
        }

        [Fact]
        public void KeyGetAccessorShouldReturnTheKeysValue()
        {
            var testDcitionary = new DataCollections.Dictionary<int, string>(5);
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
            var testDcitionary = new DataCollections.Dictionary<int, string>(5);
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
            var testDcitionary = new DataCollections.Dictionary<int, string>(5);
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
            var testDcitionary = new DataCollections.Dictionary<int, int>(5);

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
            var testDcitionary = new DataCollections.Dictionary<string, int>(1);

            Assert.Throws<ArgumentNullException>(() => testDcitionary.Add(null, 10));
        }

        [Fact]
        public void AddFunctionShouldThrowAnInvalidOpertionExceptionIfKeyIsAlreadyPresent()
        {
            var testDcitionary = new DataCollections.Dictionary<string, int>(1);

            testDcitionary.Add("A", 10);

            Assert.Throws<InvalidOperationException>(() => testDcitionary.Add("A", 20));
        }

        [Fact]
        public void AddFunctionShouldUseFreeSpacesIfElementsHaveBeenRemoved()
        {
            var testDictionary = new DataCollections.Dictionary<string, int>();

            testDictionary.Add("A", 10);
            testDictionary.Add("B", 20);
            testDictionary.Add("C", 30);
            testDictionary.Add("D", 60);
            testDictionary.Add("E", 70);
            testDictionary.Remove("B");
            testDictionary.Remove("C");
            testDictionary.Remove("A");
            testDictionary.Add("B", 40);
            testDictionary.Add("C", 50);
            testDictionary.Add("A", 10);

            Assert.Equal(5, testDictionary.Count);
        }

        [Fact]
        public void ClearFunctionShouldClearTheDictionaryProperly()
        {
            var testDictionary = new DataCollections.Dictionary<string, int>();

            testDictionary.Add("A", 1);
            testDictionary.Add("B", 2);
            testDictionary["C"] = 3;
            testDictionary.Clear();

            Assert.Empty(testDictionary);
        }

        [Fact]
        public void ContainsFunctionShoouldReturnTrueIfKeyValuePairIsPresent()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();

            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);

            Assert.Contains(new KeyValuePair<string, int>("A", 1), dictionary);

            Assert.Contains(new KeyValuePair<string, int>("B", 2), dictionary);

            Assert.Contains(new KeyValuePair<string, int>("C", 3), dictionary);
        }

        [Fact]
        public void ContainsKeyFunctionShouldReturnTrueIfKeyIsPresent()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();

            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);

            Assert.Contains("A", dictionary);
            Assert.Contains("B", dictionary);
            Assert.Contains("C", dictionary);
        }

        [Fact]
        public void CopyToFunctionShouldCopyContentsToKeyValuePairArray()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();
            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);

            KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[dictionary.Count];

            dictionary.CopyTo(array, 0);
            int keysFound = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Key == "A")
                {
                    keysFound++;
                }

                if (array[i].Key == "B")
                {
                    keysFound++;
                }

                if (array[i].Key == "C")
                {
                    keysFound++;
                }
            }

            Assert.Equal(3, keysFound);
        }

        [Fact]
        public void CopyToFunctionShouldNotCopyRemovedElements()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();
            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);
            dictionary.Remove("A");
            dictionary.Remove("B");
            dictionary.Remove("C");

            KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[3];
            dictionary.CopyTo(array, 0);

            Assert.Equal(null, array[0].Key);
            Assert.Null(array[1].Key);
            Assert.Null(array[2].Key);
        }

        [Fact]
        public void CopyToFunctionIfArrayIsNullShouldThrowArgumentNullException()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();
            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);

            KeyValuePair<string, int>[] array = null;

            Assert.Throws<ArgumentNullException>(() => dictionary.CopyTo(array, 0));
        }

        [Fact]
        public void CopyToFunctionIfArrayIndexIsOutOfBoundsShouldThrowArgumentOutOfRangeException()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();
            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);

            KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[dictionary.Count];

            Assert.Throws<ArgumentOutOfRangeException>(() => dictionary.CopyTo(array, 5));
        }

        [Fact]
        public void CopyToFunctionIfNotEnoughSpaceInArrayShouldThrowArgumentException()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();
            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);

            KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[dictionary.Count];

            Assert.Throws<ArgumentException>(() => dictionary.CopyTo(array, 2));
        }

        [Fact]
        public void CanIterateThroughElements()
        {
            int i = 0;
            var dictionary = new DataCollections.Dictionary<string, int>();
            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);
            var keyList = (ListCollection<string>)dictionary.Keys;

            foreach (var element in dictionary)
            {
                Assert.Equal(keyList[i], element.Key);
                i++;
            }
        }

        [Fact]
        public void RemoveFunctionShouldThrowArgumentNullExceptionIfKeyIsNull()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();
            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);

            Assert.Throws<ArgumentNullException>(() => dictionary.Remove(null));
        }

        [Fact]
        public void RemoveFunctionShouldRemoveGivenKey()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();
            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);

            Assert.True(dictionary.Remove("A"));
            Assert.True(dictionary.Remove("B"));
            Assert.True(dictionary.Remove("C"));
        }

        [Fact]
        public void RemoveFunctionShouldReturnFalseIfKeyNotPresent()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();
            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);

            Assert.False(dictionary.Remove("D"));
            Assert.False(dictionary.Remove("E"));
            Assert.False(dictionary.Remove("F"));
        }

        [Fact]
        public void RemoveFunctionShouldRemoveGivenKeyValuePair()
        {
            var dictionary = new DataCollections.Dictionary<string, int>();
            dictionary.Add("A", 1);
            dictionary.Add("B", 2);
            dictionary.Add("C", 3);

            Assert.True(dictionary.Remove(new KeyValuePair<string, int>("A", 1)));
            Assert.True(dictionary.Remove(new KeyValuePair<string, int>("B", 2)));
            Assert.True(dictionary.Remove(new KeyValuePair<string, int>("C", 3)));
        }
    }
}
