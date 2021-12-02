using Xunit;

namespace Range
{
    public class ChoiceFacts
    {
        [Theory]
        [InlineData("012")]
        [InlineData("12")]
        [InlineData("92")]
        public void ChoiceClassValidatesDigitsProperlyInDigitObject(string text)
        {
            IPattern digit = new Choice(
                new Character('0'),
                new Range('1', '9'));
            Assert.True(digit.Match(text).Success());
        }

        [Theory]
        [InlineData("a9")]
        [InlineData("")]
        [InlineData(null)]
        public void ChoiceClassDigitObjectReturnsFalseForInvalidData(string text)
        {
            IPattern digit = new Choice(
                new Character('0'),
                new Range('1', '9'));

            Assert.False(digit.Match(text).Success());
        }

        [Theory]
        [InlineData("012")]
        [InlineData("12")]
        [InlineData("92")]
        [InlineData("a9")]
        [InlineData("f8")]
        [InlineData("A9")]
        [InlineData("F8")]
        public void ChoiceClassValidatesHexesProperly(string text)
        {
            IPattern hex = new Choice(
                new Character('0'),
                new Range('1', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));

            Assert.True(hex.Match(text).Success());
        }

        [Theory]
        [InlineData("g8")]
        [InlineData("G8")]
        [InlineData("")]
        [InlineData(null)]
        public void ChoiceClassHexObjectReturnsFalseToInvalidData(string text)
        {
            IPattern hex = new Choice(
                new Character('0'),
                new Range('1', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));

            Assert.False(hex.Match(text).Success());
        }
    }
}
