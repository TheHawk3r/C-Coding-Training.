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
            var initialText = text;
            var match = new Match(text, false);
            foreach (IPattern pattern in patterns)
            {
                match = (Match)pattern.Match(text);
                text = pattern.Match(text).RemainingText();
                if (!match.Success())
                {
                    text = initialText;
                    return (Match)pattern.Match(text);
                }
            }

            return match;
        }
    }
}
