namespace Range
{
    public class Number : IPattern
    {
        private readonly IPattern pattern;

        public Number()
        {
            Range oneNine = new Range('1', '9');
            Range digit = new Range('0', '9');
            Choice exponentChoice = new Choice(new Character('e'), new Character('E'));
            var digitNumber = new Sequence(new Optional(new Character('-')), digit);
            var digitsFractionalNumber = new Sequence(
                new Optional(new Character('-')), new OneOrMore(oneNine), new Many(digit), new Character('.'), new OneOrMore(digit));
            var digitFractionalNumber = new Sequence(
                    new Optional(new Character('-')), new Character('0'), new Character('.'), new OneOrMore(digit));
            var digitsNumber = new Sequence(
                    new Optional(new Character('-')), new OneOrMore(oneNine), new Many(digit));
            var digitsNumberDigitExponent = new Sequence(
                    new Optional(new Character('-')), new OneOrMore(oneNine), new Many(digit), exponentChoice, new Character('0'));
            var digitsNumberDigitsExponent = new Sequence(
                    new Optional(new Character('-')), new OneOrMore(oneNine), new Many(digit), exponentChoice, new OneOrMore(oneNine), new Many(digit));
            var digitNumberDigitExponent = new Sequence(
                    new Optional(new Character('-')), digit, exponentChoice, new Character('0'));
            var digitNumberDigitsExponent = new Sequence(
                    new Optional(new Character('-')), digit, exponentChoice, new OneOrMore(oneNine), new Many(digit));

            pattern = new Choice(
                digitsFractionalNumber,
                digitFractionalNumber,
                digitsNumber,
                digitsNumberDigitExponent,
                digitsNumberDigitsExponent,
                digitNumber,
                digitNumberDigitExponent,
                digitNumberDigitsExponent);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
