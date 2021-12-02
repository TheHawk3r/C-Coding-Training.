namespace Range
{
    class Choice : IPattern
    {
        readonly IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public bool Match(string text)
        {
            foreach (IPattern pattern in patterns)
            {
                if (pattern.Match(text).Success())
                {
                    return true;
                }
            }

            return false;
        }

        IMatch IPattern.Match(string text)
        {
            var match = new Match(text, false);
            foreach (IPattern pattern in patterns)
            {
                match = (Match)pattern.Match(text);
                if (match.Success())
                {
                    return match;
                }
            }

            return match;
        }
    }
}
