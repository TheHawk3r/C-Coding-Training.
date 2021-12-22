namespace Range
{
    class Choice : IPattern
    {
        private IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            IMatch match = new Match(text, false);
            foreach (IPattern pattern in patterns)
            {
                match = pattern.Match(text);
                if (match.Success())
                {
                    return match;
                }
            }

            return match;
        }

        public void Add(IPattern pattern)
        {
            IPattern[] newPatterns = new IPattern[patterns.Length + 1];

            newPatterns[patterns.Length] = pattern;
            patterns.CopyTo(newPatterns, 0);

            patterns = newPatterns;
        }
    }
}
