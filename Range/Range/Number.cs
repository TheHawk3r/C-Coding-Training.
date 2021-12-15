namespace Range
{
    public class Number : IPattern
    {
        private readonly IPattern pattern;

        public Number()
        {
            Range oneNine = new Range('1', '9');
            Range digit = new Range('0', '9');
            Choice signChoice = new Choice(new Character('+'), new Character('-'));
            Choice exponentChoice = new Choice(new Character('e'), new Character('E'));
            Sequence fractionPart = new Sequence(new Character('.'), new OneOrMore(digit));

            var digitNumber = new Sequence(
                new Optional(new Character('-')),
                digit);

            var digitsFractionalNumber = new Sequence(
                new Optional(new Character('-')),
                new OneOrMore(oneNine),
                new Many(digit),
                fractionPart);

            var zeroDigitFractionalNumber = new Sequence(
                    new Optional(new Character('-')),
                    new Character('0'),
                    fractionPart);

            var digitsNumber = new Sequence(
                    new Optional(new Character('-')),
                    new OneOrMore(oneNine),
                    new Many(digit));

            var digitsNumberZeroDigitExponent = new Sequence(
                    new Optional(new Character('-')),
                    new OneOrMore(oneNine),
                    new Many(digit),
                    new Optional(fractionPart),
                    exponentChoice,
                    new Optional(signChoice),
                    new Character('0'));

            var digitsNumberDigitsExponent = new Sequence(
                    new Optional(new Character('-')),
                    new OneOrMore(oneNine),
                    new Many(digit),
                    new Optional(fractionPart),
                    exponentChoice,
                    new Optional(signChoice),
                    new OneOrMore(oneNine),
                    new Many(digit));

            var digitNumberZeroDigitExponent = new Sequence(
                    new Optional(new Character('-')),
                    digit,
                    new Optional(fractionPart),
                    exponentChoice,
                    new Optional(signChoice),
                    new Character('0'));

            var digitNumberDigitsExponent = new Sequence(
                    new Optional(new Character('-')),
                    digit,
                    new Optional(fractionPart),
                    exponentChoice,
                    new Optional(signChoice),
                    new OneOrMore(oneNine),
                    new Many(digit));

            pattern = new Choice(
                digitsNumberZeroDigitExponent,
                digitsNumberDigitsExponent,
                digitNumberZeroDigitExponent,
                digitNumberDigitsExponent,
                digitsFractionalNumber,
                zeroDigitFractionalNumber,
                digitsNumber,
                digitNumber);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
