using Xunit;

namespace Range
{
    public class JsonStringTheories
    {
        [Theory]

        [InlineData("\"ab\\u532f se\\u213d\"", "")]
        [InlineData("\"abc\"", "")]
        [InlineData("\"a\"\"", "\"")]
        [InlineData("\"⛅⚾\"", "")]
        [InlineData("\"\\\"a\\\" b\"", "")]
        [InlineData("\"a \\\\ b\"", "")]
        [InlineData("\"a \\/ b\"", "")]
        [InlineData("\"a \\b b\"", "")]
        [InlineData("\"a \\r b\"", "")]
        [InlineData("\"a \\t b\"", "")]
        [InlineData("\"a \\f b\"", "")]
        [InlineData("\"a \\n b\"", "")]
        [InlineData("\"\"", "")]
        public void IsJsonString(string text, string remainingText)
        {
            var jsonString = new JsonString();

            Assert.True(jsonString.Match(text).Success());
            Assert.Equal(remainingText, jsonString.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("\"a\\abc\"", "\"a\\abc\"")]
        [InlineData("\"a\nb\rc\"", "\"a\nb\rc\"")]
        [InlineData("\"a\\x\"", "\"a\\x\"")]
        [InlineData("\"a\\u\"", "\"a\\u\"")]
        [InlineData("\"a\\u123\"", "\"a\\u123\"")]
        [InlineData("\"abc", "\"abc")]
        [InlineData("abc\"", "abc\"")]
        [InlineData("\"a\\\"", "\"a\\\"")]
        [InlineData("a", "a")]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void IsNotJsonString(string text, string remainingText)
        {
            var jsonString = new JsonString();

            Assert.False(jsonString.Match(text).Success());
            Assert.Equal(remainingText, jsonString.Match(text).RemainingText());
        }
    }
}
