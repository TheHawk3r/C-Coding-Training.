namespace Range
{
    class Choice : IPattern
    {
        readonly IPattern[] patterns;

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
    }
}
