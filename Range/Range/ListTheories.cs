using Xunit;

namespace Range
{
    public class ListTheories
    {
        [Theory]
        [InlineData("1,2,3", "")]
        [InlineData("1,2", "")]
        [InlineData("1,2,3,", ",")]
        [InlineData("11a", "1a")]
        [InlineData("1a", "a")]
        [InlineData("abc", "abc")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void Test1(string text, string remainingText)
        {
            var a = new List(new Range('0', '9'), new Character(','));

            Assert.True(a.Match(text).Success());
            Assert.Equal(remainingText, a.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("2; 23  ;\n 333 \t; 32 \r; 45  \n; 52", "")]
        [InlineData("1; 22  ;\n 333 \t; 22 \r; 33", "")]
        [InlineData("1; 22  ;\n 333 \t; 22", "")]
        [InlineData("1 \n;", " \n;")]
        [InlineData("abc", "abc")]
        [InlineData(",abc", ",abc")]
        public void Test2(string text, string remainingText)
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);

            Assert.True(list.Match(text).Success());
            Assert.Equal(remainingText, list.Match(text).RemainingText());
        }
    }
}