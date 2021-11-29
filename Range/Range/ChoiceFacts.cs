using Xunit;

namespace Range
{
    public class ChoiceFacts
    {
        [Fact]
        public void ChoiceClassValidatesDigitsProperly()
        {
            var digit = new Choice(
                new Character('0'),
                new Range('1', '9'));

            Assert.True(digit.Match("012"));
            Assert.True(digit.Match("12"));
            Assert.True(digit.Match("92"));
            Assert.False(digit.Match("a9"));
            Assert.False(digit.Match(""));
            Assert.False(digit.Match(null));
        }

        [Fact]
        public void ChoiceClassValidatesHexesProperly()
        {
            var hex = new Choice(
                new Character('0'),
                new Range('1', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));

            Assert.True(hex.Match("012"));
            Assert.True(hex.Match("12"));
            Assert.True(hex.Match("92"));
            Assert.True(hex.Match("a9"));
            Assert.True(hex.Match("f8"));
            Assert.True(hex.Match("A9"));
            Assert.True(hex.Match("F8"));
            Assert.False(hex.Match("g8"));
            Assert.False(hex.Match("G8"));
            Assert.False(hex.Match(""));
            Assert.False(hex.Match(null));
        }
    }
}
