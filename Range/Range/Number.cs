namespace Range
{
    public class Number : IPattern
    {
        private readonly IPattern pattern;

        public Number()
        {
            pattern = new Choice(
                new Sequence(new Optional(new Character('-')), new OneOrMore(new Range('1', '9')), new Many(new Range('0', '9'))),
                new Sequence(new Optional(new Character('-')), new Range('0', '9')));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
