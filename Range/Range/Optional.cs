namespace Range
{
    class Optional : IPattern
    {
        readonly IPattern pattern;

        public Optional(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            return new Match(pattern.Match(text).RemainingText(), true);
        }
    }
}
