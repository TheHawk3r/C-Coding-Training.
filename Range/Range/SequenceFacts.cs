using Xunit;

namespace Range
{
    public class SequenceFacts
    {
        [Theory]
        [InlineData("abcd", "cd")]
        public void SequanceObjectReturnsAValidMatchFromAStringThatContainsTheSequanceAB(string text, string remainingText)
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            Assert.True(ab.Match(text).Success());
            Assert.Equal(remainingText, ab.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("ax", "ax")]
        [InlineData("def", "def")]
        [InlineData("", "")]

        public void SequanceObjectReturnsAInvalidMatchFromATextThatDoesNotContainTheSequanceAB(string text, string remainingText)
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'));

            Assert.False(ab.Match(text).Success());
            Assert.Equal(remainingText, ab.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("abcd", "d")]
        public void SequanceObjectReturnsAValidMatchFromAStringThatContainsTheSequanceABC(string text, string remainingText)
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'),
                new Character('c'));

            Assert.True(ab.Match(text).Success());
            Assert.Equal(remainingText, ab.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("def", "def")]
        [InlineData("abx", "abx")]
        [InlineData("", "")]
        [InlineData(null, null)]

        public void SequanceObjectReturnsAInvalidMatchFromAStringThatDoesNotContainTheSequanceABc(string text, string remainingText)
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b'),
                new Character('c'));
            Assert.False(ab.Match(text).Success());
            Assert.Equal(remainingText, ab.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("u1234", "")]
        [InlineData("uabcdef", "ef")]
        [InlineData("uB005 ab", " ab")]
        public void SequanceObjectReturnsAValidMatchFromAStringThatContainsAUnicodeHexNumberSequance(string text, string remainingText)
        {
            var hex = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));
            var hexSeq = new Sequence(
                new Character('u'),
                new Sequence(
                    hex,
                    hex,
                    hex,
                    hex));

            Assert.True(hexSeq.Match(text).Success());
            Assert.Equal(remainingText, hexSeq.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("abc", "abc")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void SequanceObjectReturnsAInvalidMatchFromAStringThatContainsAUnicodeHexNumberSequance(string text, string remainingText)
        {
            var hex = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));
            var hexSeq = new Sequence(
                new Character('u'),
                new Sequence(
                    hex,
                    hex,
                    hex,
                    hex));

            Assert.False(hexSeq.Match(text).Success());
            Assert.Equal(remainingText, hexSeq.Match(text).RemainingText());
        }
    }
}
