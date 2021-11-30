namespace Range
{
    class Choice
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
    }
}
