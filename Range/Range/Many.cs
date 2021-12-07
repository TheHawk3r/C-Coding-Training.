

namespace Range
{
    class Many : IPattern
    {
        readonly IPattern pattern;

        public Many(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            IMatch match = pattern.Match(text);

            while (true)
            {
                match = pattern.Match(match.RemainingText());
                if (!match.Success())
                {
                    return new Match(match.RemainingText(), true);
                }
            }
        }
    }
}
