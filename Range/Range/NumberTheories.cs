using Xunit;

namespace Range
{
    public class NumberTheories
    {
        [Theory]
        [InlineData("0", "")]
        [InlineData("7", "")]
        [InlineData("70", "")]
        [InlineData("07", "7")]
        public void IsNumber(string text, string remainingText)
        {
            var number = new Number();

            Assert.True(number.Match(text).Success());
            Assert.Equal(remainingText, number.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("", "")]
        [InlineData(null, null)]

        public void IsNotNumber(string text, string remainingText)
        {
            var number = new Number();

            Assert.False(number.Match(text).Success());
            Assert.Equal(remainingText, number.Match(text).RemainingText());
        }
    }
}
