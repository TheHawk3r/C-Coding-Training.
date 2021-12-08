namespace Range
{
    class OneOrMore : IPattern
    {
        private readonly IPattern pattern;

        public OneOrMore(IPattern pattern)
        {
            this.pattern = new Many(pattern);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text).RemainingText() == text
                ? new Match(text, false)
                : pattern.Match(text);
        }
    }
}
