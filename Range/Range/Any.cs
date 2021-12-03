using System;

namespace Range
{
    class Any : IPattern
    {
        readonly string accepted;

        public Any(string accepted)
        {
            this.accepted = accepted;
        }

        public IMatch Match(string text)
        {
            if (!string.IsNullOrEmpty(text) && accepted.Contains(text[0]))
            {
                return new Match(text[1..], true);
            }

            return new Match(text, false);
        }
    }
}
