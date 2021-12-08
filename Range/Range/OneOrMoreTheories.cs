using Xunit;

namespace Range
{
    public class OneOrMoreTheories
    {
        [Theory]
        [InlineData("123", "")]
        [InlineData("1a", "a")]
        public void Test1(string text, string remainingText)
        {
            var a = new OneOrMore(new Range('0', '9'));

            Assert.True(a.Match(text).Success());
            Assert.Equal(remainingText, a.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("bc", "bc")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void Test2(string text, string remainingText)
        {
            var a = new OneOrMore(new Range('0', '9'));

            Assert.False(a.Match(text).Success());
            Assert.Equal(remainingText, a.Match(text).RemainingText());
        }
    }
}
