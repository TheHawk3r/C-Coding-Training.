using DataCollections;
using Xunit;
using System;

namespace DataCollection.Tests
{
    public class ReadOnlyListCollectionTests
    {
        [Fact]

        public void SettingAELementInAReadOnlyListShouldThrowNotImplementedException()
        {
            var testList = new ListCollection<int>() { 1, 2 };

            testList = testList.AsReadOnly();

            Assert.Throws<NotImplementedException>(() => testList[0] = 2);
        }

        [Fact]

        public void AddingAELementInAReadOnlyListShouldThrowNotImplementedException()
        {
            var testList = new ListCollection<int>() { 1, 2 };

            testList = testList.AsReadOnly();

            Assert.Throws<NotImplementedException>(() => testList.Add(5));
        }

        [Fact]

        public void InsertingAnELementInAReadOnlyListShouldThrowNotImplementedException()
        {
            var testList = new ListCollection<int>() { 1, 2 };

            testList = testList.AsReadOnly();

            Assert.Throws<NotImplementedException>(() => testList.Insert(2, 10));
        }

        [Fact]

        public void RemovingAnELementInAReadOnlyListShouldThrowNotImplementedException()
        {
            var testList = new ListCollection<int>() { 1, 2 };

            testList = testList.AsReadOnly();

            Assert.Throws<NotImplementedException>(() => testList.Remove(1));
        }

        [Fact]

        public void RemovingAnELementAtAIndexInAReadOnlyListShouldThrowNotImplementedException()
        {
            var testList = new ListCollection<int>() { 1, 2 };

            testList = testList.AsReadOnly();

            Assert.Throws<NotImplementedException>(() => testList.RemoveAt(1));
        }
    }
}
