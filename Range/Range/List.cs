namespace Range
{
    class List : IPattern
    {
        private readonly IPattern pattern;

        public List(IPattern element, IPattern separator)
        {
            this.pattern = new Sequence(new Optional(element), new Many(new Sequence(separator, element)));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
