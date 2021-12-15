namespace Range
{
    public class Number : IPattern
    {
        private readonly IPattern pattern;

        public Number()
        {
            Range oneNine = new Range('1', '9');
            Range digit = new Range('0', '9');

            var digits = new OneOrMore(digit);
            var integer = new Sequence(new Optional(new Character('-')), new Choice(new Sequence(oneNine, digits), digit));
            var fraction = new Sequence(new Character('.'), digits);
            var exponent = new Sequence(new Any("eE"), new Optional(new Any("+-")), digits);

            pattern = new Sequence(integer, new Optional(fraction), new Optional(exponent));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
