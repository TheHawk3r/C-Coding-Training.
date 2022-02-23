using DataCollections;
using System;
using Xunit;

namespace DataCollection.Tests
{
    public class ReadOnlyListCollectionTests
    {
        [Fact]

        public void SettingAELementInAReadOnlyListShouldThrowNotSupportedException()
        {
            var testListOne = new ListCollection<int>() { 1, 2 };

            var testListTwo = testListOne.AsReadOnly();

            Assert.Throws<NotSupportedException>(() => testListTwo[0] = 2);
        }

        [Fact]

        public void AddingAELementInAReadOnlyListShouldThrowNotSupportedException()
        {
            var testListOne = new ListCollection<int>() { 1, 2 };

            var testListTwo = testListOne.AsReadOnly();

            Assert.Throws<NotSupportedException>(() => testListTwo.Add(5));
        }

        [Fact]

        public void InsertingAnELementInAReadOnlyListShouldThrowNotSupportedException()
        {
            var testListOne = new ListCollection<int>() { 1, 2 };

            var testListTwo = testListOne.AsReadOnly();

            Assert.Throws<NotSupportedException>(() => testListTwo.Insert(2, 10));
        }

        [Fact]

        public void RemovingAnELementInAReadOnlyListShouldThrowNotSupportedException()
        {
            var testListOne = new ListCollection<int>() { 1, 2 };

            var testListTwo = testListOne.AsReadOnly();

            Assert.Throws<NotSupportedException>(() => testListTwo.Remove(1));
        }

        [Fact]

        public void RemovingAnELementAtAIndexInAReadOnlyListShouldThrowNotSupportedException()
        {
            var testListOne = new ListCollection<int>() { 1, 2 };

            var testListTwo = testListOne.AsReadOnly();

            Assert.Throws<NotSupportedException>(() => testListTwo.RemoveAt(1));
        }

        [Fact]

        public void ClearingAAReadOnlyListShouldThrowNotSupportedException()
        {
            var testListOne = new ListCollection<int>() { 1, 2 };

            var testListTwo = testListOne.AsReadOnly();

            Assert.Throws<NotSupportedException>(() => testListTwo.Clear());
        }
    }
}
