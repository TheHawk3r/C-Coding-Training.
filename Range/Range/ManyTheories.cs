using Xunit;

namespace Range
{
    public class ManyTheories
    {
        [Theory]
        [InlineData("abc", "bc")]
        [InlineData("aaaabc", "bc")]
        [InlineData("bc", "bc")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ReturnsAMatchWithATextInWitchTheCharacterPatternIsConsumed(string text, string remainingText)
        {
            var a = new Many(new Character('a'));
            Assert.True(a.Match(text).Success());
            Assert.Equal(remainingText, a.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("12345ab123", "ab123")]
        [InlineData("53221345645bb123", "bb123")]
        [InlineData("ab", "ab")]
        public void ReturnsAMatchWithATextInWitchTheRangePatternIsConsumed(string text, string remainingText)
        {
            var digits = new Many(new Range('0', '9'));
            Assert.True(digits.Match(text).Success());
            Assert.Equal(remainingText, digits.Match(text).RemainingText());
        }
    }
}
