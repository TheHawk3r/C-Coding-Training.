using Xunit;

namespace Range
{
    public class TextTheories
    {
        [Theory]
        [InlineData("true", "")]
        [InlineData("trueX", "X")]
        public void ReturnsAValidMatchWhenTheTextStartsWithThePrefixTrue(string text, string remainingText)
        {
            var t = new Text("true");

            Assert.True(t.Match(text).Success());
            Assert.Equal(remainingText, t.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("false", "false")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ReturnsAnInvalidMatchWhenTheTextDoesNotStartWithThePrefixTrue(string text, string remainingText)
        {
            var t = new Text("true");

            Assert.False(t.Match(text).Success());
            Assert.Equal(remainingText, t.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("false", "")]
        [InlineData("falseX", "X")]
        public void ReturnsAValidMatchWhenTheTextStartsWithThePrefixFalse(string text, string remainingText)
        {
            var f = new Text("false");

            Assert.True(f.Match(text).Success());
            Assert.Equal(remainingText, f.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("true", "true")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ReturnsAnInvalidMatchWhenTheTextDoesNotStartWithTHePrefixFalse(string text, string remainingText)
        {
            var f = new Text("false");

            Assert.False(f.Match(text).Success());
            Assert.Equal(remainingText, f.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("true", "true")]
        [InlineData("false", "false")]
        public void ReturnsAValidMatchIfThePrefixIsEmpty(string text, string remainingText)
        {
            var empty = new Text("");

            Assert.True(empty.Match(text).Success());
            Assert.Equal(remainingText, empty.Match(text).RemainingText());
        }

        [Theory]
        [InlineData(null, null)]
        public void ReturnsAnInvalidMatchIfThePrefixIsEmptyAndTheTextisNull(string text, string remainingText)
        {
            var empty = new Text("");

            Assert.False(empty.Match(text).Success());
            Assert.Equal(remainingText, empty.Match(text).RemainingText());
        }
    }
}
