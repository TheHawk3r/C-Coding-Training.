using Xunit;

namespace Range
{
    public class RangeFacts
    {
        [Fact]
        public void RangeMatch()
        {
            var digit = new Range('a', 'f');

            Assert.True(digit.Match("abc"));
        }
    }
}
