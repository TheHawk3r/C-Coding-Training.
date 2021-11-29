using Xunit;

namespace Range
{
    public class RangeFacts
    {
        [Fact]
        public void RangeMatchMethodIfTextIsNullReturnsFalse()
        {
            var digit = new Range('a', 'f');

            Assert.False(digit.Match(null));
        }

        [Fact]
        public void RangeMatchMethodIfTextIsEmptyReturnsFalse()
        {
            var digit = new Range('a', 'f');

            Assert.False(digit.Match(string.Empty));
        }

        [Fact]
        public void RangeMatchMethodFirstCharacterOfTextThatIsInRangeOfCharactersShouldReturnTrue()
        {
            var digit = new Range('1', '4');

            Assert.True(digit.Match("123"));
        }

        [Fact]
        public void RangeMatchMethodFirstCharacterOfTextThatIsNotInRangeOfCharactersShouldReturnFalse()
        {
            var digit = new Range('7', '9');

            Assert.False(digit.Match("34"));
        }

        [Fact]
        public void RangeMatchMethodFirstCharacterOfTextIsInRangeButOtherCharactersAreNotShouldReturnTrue()
        {
            var digit = new Range('a', 'g');

            Assert.True(digit.Match("abecedar"));
        }

        [Fact]
        public void RangeMatchMethodHomeWorkFirstStringShouldReturnTrue()
        {
            var digit = new Range('a', 'f');

            Assert.True(digit.Match("abc"));
        }

        [Fact]
        public void RangeMatchMethodHomeWorkSecondStringShouldReturnTrue()
        {
            var digit = new Range('a', 'f');

            Assert.True(digit.Match("fab"));
        }

        [Fact]
        public void RangeMatchMethodHomeWorkThirdStringShouldReturnTrue()
        {
            var digit = new Range('a', 'f');

            Assert.True(digit.Match("bcd"));
        }

        [Fact]
        public void RangeMatchMethodHomeWorkFourthStringShouldReturnFalse()
        {
            var digit = new Range('a', 'f');

            Assert.False(digit.Match("1ab"));
        }
    }
}
