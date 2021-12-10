namespace Range
{
    class List : IPattern
    {
        private readonly IPattern pattern;

        public List(IPattern element, IPattern separator)
        {
            this.pattern = new Many(new Sequence(new Choice(element, separator), element));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
