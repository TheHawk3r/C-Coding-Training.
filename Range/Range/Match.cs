namespace Range
{
    class Match : IMatch
    {
        readonly string text;
        readonly char pattern;
        readonly char start;
        readonly char end;

        public Match(string text, char pattern = '-', char start = '-', char end = '-')
        {
            this.text = text;
            this.pattern = pattern;

            if (start == end)
            {
                return;
            }

            this.start = start;
            this.end = end;
        }

        public string RemainingText()
        {
            if (this.Success())
            {
                return text[1..];
            }

            return text;
        }

        public bool Success()
        {
            if (this.start == this.end)
            {
                return !string.IsNullOrEmpty(text) && text[0] == pattern;
            }

            return !string.IsNullOrEmpty(text)
            && text[0] >= this.start
            && text[0] <= this.end;
        }
    }
}
