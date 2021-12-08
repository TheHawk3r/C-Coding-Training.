using Xunit;

namespace Range
{
    public class OneOrMoreTheories
    {
        [Theory]
        [InlineData("123", "")]
        [InlineData("1a", "a")]
        public void ReturnsAValidMatchWithATextInWitchTheRangePatternIsFoundAtLeastOnce(string text, string remainingText)
        {
            var a = new OneOrMore(new Range('0', '9'));

            Assert.True(a.Match(text).Success());
            Assert.Equal(remainingText, a.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("bc", "bc")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ReturnsAInvalidMatchWithATextInWitchTheRangePatternIsNotFound(string text, string remainingText)
        {
            var a = new OneOrMore(new Range('0', '9'));

            Assert.False(a.Match(text).Success());
            Assert.Equal(remainingText, a.Match(text).RemainingText());
        }
    }
}
