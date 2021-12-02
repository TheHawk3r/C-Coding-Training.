namespace Range
{
    class Match : IMatch
    {
        readonly string remainingText;
        readonly bool success;

        public Match(string remainingText, bool success)
        {
            this.remainingText = remainingText;
            this.success = success;
        }

        public string RemainingText()
        {
            return remainingText;
        }

        public bool Success()
        {
            return this.success;
        }
    }
}
