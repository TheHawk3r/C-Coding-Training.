namespace Range
{
    public class Number : IPattern
    {
        private readonly IPattern pattern;

        public Number()
        {
            Range oneNine = new Range('1', '9');
            Range digit = new Range('0', '9');
            Sequence fraction = new Sequence(new Character('.'), new OneOrMore(digit));

            var integer = new Choice(new Sequence(new Character('-'), oneNine, new Many(digit)), new Sequence(new Character('-'), digit), new Sequence(oneNine, new Many(digit)), new Sequence(digit));
            var exponent = new Choice(
                new Sequence(new Character('E'), new Optional(new Choice(new Character('+'), new Character('-'))), new OneOrMore(digit)),
                new Sequence(new Character('e'), new Optional(new Choice(new Character('+'), new Character('-'))), new OneOrMore(digit)));

            pattern = new Sequence(integer, new Optional(fraction), new Optional(exponent));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
