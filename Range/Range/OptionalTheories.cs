using Xunit;

namespace Range
{
    public class OptionalTheories
    {
        [Theory]
        [InlineData("abc", "bc")]
        [InlineData("aabc", "abc")]
        [InlineData("bc", "bc")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ReturnsAMatchWithATextInWitchTheCharacterPatternIsConsumedOnce(string text, string remainingText)
        {
            var a = new Optional(new Character('a'));

            Assert.True(a.Match(text).Success());
            Assert.Equal(remainingText, a.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("123", "123")]
        [InlineData("-123", "123")]
        [InlineData("--123", "-123")]
        public void ReturnsAMAtchWithATextInWitchTheSignPatternIsConsumedOnce(string text, string remainingText)
        {
            var sign = new Optional(new Character('-'));

            Assert.True(sign.Match(text).Success());
            Assert.Equal(remainingText, sign.Match(text).RemainingText());
        }
    }
}
