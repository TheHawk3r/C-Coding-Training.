namespace Range
{
    class Range : IPattern
    {
        readonly char start;
        readonly char end;

        public Range(char start, char end)
        {
            this.start = start;
            this.end = end;
        }

        public bool Match(string text)
        {
            return !string.IsNullOrEmpty(text)
                && text[0] >= this.start
                && text[0] <= this.end;
        }

        IMatch IPattern.Match(string text)
        {
            if (!string.IsNullOrEmpty(text)
            && text[0] >= this.start
            && text[0] <= this.end)
            {
                return new Match(text[1..], true);
            }

            return new Match(text, false);
        }
    }
}
