using Xunit;

namespace Range
{
    public class RangeFacts
    {
        [Fact]
        public void RangeMatchMethodIfTextIsNullReturnsFalse()
        {
            IPattern digit = new Range('a', 'f');

            Assert.False(digit.Match(null).Success());
        }

        [Fact]
        public void RangeMatchMethodIfTextIsEmptyReturnsFalse()
        {
            IPattern digit = new Range('a', 'f');

            Assert.False(digit.Match(string.Empty).Success());
        }

        [Fact]
        public void RangeMatchMethodFirstCharacterOfTextThatIsInRangeOfCharactersShouldReturnTrue()
        {
            IPattern digit = new Range('1', '4');

            Assert.True(digit.Match("123").Success());
        }

        [Fact]
        public void RangeMatchMethodFirstCharacterOfTextThatIsNotInRangeOfCharactersShouldReturnFalse()
        {
            IPattern digit = new Range('7', '9');

            Assert.False(digit.Match("34").Success());
        }

        [Fact]
        public void RangeMatchMethodFirstCharacterOfTextIsInRangeButOtherCharactersAreNotShouldReturnTrue()
        {
            IPattern digit = new Range('a', 'g');

            Assert.True(digit.Match("abecedar").Success());
        }

        [Fact]
        public void RangeMatchMethodHomeWorkFirstStringShouldReturnTrue()
        {
            IPattern digit = new Range('a', 'f');

            Assert.True(digit.Match("abc").Success());
        }

        [Fact]
        public void RangeMatchMethodHomeWorkSecondStringShouldReturnTrue()
        {
            IPattern digit = new Range('a', 'f');

            Assert.True(digit.Match("fab").Success());
        }

        [Fact]
        public void RangeMatchMethodHomeWorkThirdStringShouldReturnTrue()
        {
            IPattern digit = new Range('a', 'f');

            Assert.True(digit.Match("bcd").Success());
        }

        [Fact]
        public void RangeMatchMethodHomeWorkFourthStringShouldReturnFalse()
        {
            IPattern digit = new Range('a', 'f');

            Assert.False(digit.Match("1ab").Success());
        }
    }
}
