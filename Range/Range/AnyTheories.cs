using Xunit;

namespace Range
{
    public class AnyTheories
    {
        [Theory]
        [InlineData("ea", "a")]
        [InlineData("Ea", "a")]

        public void ReturnsAValidMatchWhenATextInWitchTheFirstLetterContainsAnAcceptedLetter(string text, string remainingText)
        {
            var e = new Any("eE");

            Assert.True(e.Match(text).Success());
            Assert.Equal(remainingText, e.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("", "")]
        [InlineData(null, null)]

        public void ReturnsAnInvalidMatchWhenATextInWitchTheFirstLetterDoesNotContainAnAcceptedLetter(string text, string remainingText)
        {
            var e = new Any("eE");

            Assert.False(e.Match(text).Success());
            Assert.Equal(remainingText, e.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("+3", "3")]
        [InlineData("-2", "2")]

        public void ReturnsAValidMatchWhenATextInWitchTheFirstLetterContainsAnAcceptedSign(string text, string remainingText)
        {
            var sign = new Any("-+");

            Assert.True(sign.Match(text).Success());
            Assert.Equal(remainingText, sign.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("2", "2")]
        [InlineData("", "")]
        [InlineData(null, null)]

        public void ReturnsAnInvalidMatchWhenATextInWitchTheFirstLetterDoesNotContainAnAcceptedSign(string text, string remainingText)
        {
            var sign = new Any("-+");

            Assert.False(sign.Match(text).Success());
            Assert.Equal(remainingText, sign.Match(text).RemainingText());
        }
    }
}
