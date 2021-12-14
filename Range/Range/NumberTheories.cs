using Xunit;

namespace Range
{
    public class NumberTheories
    {
        [Theory]
        [InlineData("0", "")]
        [InlineData("-0", "")]
        [InlineData("7", "")]
        [InlineData("70", "")]
        [InlineData("-25", "")]
        [InlineData("12.34", "")]
        [InlineData("0.00000001", "")]
        [InlineData("10.000000001", "")]
        [InlineData("12e3", "")]
        [InlineData("123.", ".")]
        [InlineData("12.34.56", ".56")]
        [InlineData("12.3x", "x")]
        public void IsNumber(string text, string remainingText)
        {
            var number = new Number();

            Assert.True(number.Match(text).Success());
            Assert.Equal(remainingText, number.Match(text).RemainingText());
        }

        [Fact]
        public void ReturnsAValidMatchForANumberThatBegginsWithZeroAndOnlyTheFirstDIgitIsConsumed()
        {
            var number = new Number();

            Assert.True(number.Match("07").Success());
            Assert.Equal("7", number.Match("07").RemainingText());
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData(".153", ".153")]
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
