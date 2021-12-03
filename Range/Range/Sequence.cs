namespace Range
{
    class Sequence : IPattern
    {
        readonly IPattern[] patterns;

        public Sequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            IMatch match = new Match(text, false);
            foreach (IPattern pattern in patterns)
            {
                match = pattern.Match(match.RemainingText());
                if (!match.Success())
                {
                    return pattern.Match(text);
                }
            }

            return match;
        }
    }
}
