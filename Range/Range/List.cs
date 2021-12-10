namespace Range
{
    class List : IPattern
    {
        private readonly IPattern pattern;

        public List(IPattern element, IPattern separator)
        {
            this.pattern = new Many(new Choice(new Sequence(element, separator, element, separator, element), new Sequence(element, separator, element), new Sequence(separator, element), element));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
