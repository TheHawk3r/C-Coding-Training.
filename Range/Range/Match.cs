namespace Range
{
    class Match : IMatch
    {
        readonly string text;
        readonly char pattern;

        public Match(string text, char pattern)
        {
            this.text = text;
            this.pattern = pattern;
        }

        public string RemainingText()
        {
            return text[1..];
        }

        public bool Success()
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return text[0] == pattern;
        }
    }
}
