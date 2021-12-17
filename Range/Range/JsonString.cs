namespace Range
{
    public class JsonString : IPattern
    {
        private readonly IPattern pattern;

        public JsonString()
        {
            Range digit = new Range('0', '9');
            var hex = new Choice(digit, new Range('A', 'F'), new Range('a', 'f'));
            var escape = new Choice(new Any("\"\\/bfnrt"), new Sequence(new Character('u'), hex, hex, hex, hex));
            var character = new Choice(
                new Range('\u0020', '\u0021'),
                new Range('\u0023', '\u005b'),
                new Range('\u005d', char.MaxValue),
                new Sequence(new Character('\\'), escape));
            var characters = new Choice(new OneOrMore(character));
            pattern = new Sequence(new Character('\"'), characters, new Character('\"'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
