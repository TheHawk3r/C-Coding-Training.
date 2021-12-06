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
            return !string.IsNullOrEmpty(text) && accepted.Contains(text[0])
            ? new Match(text[1..], true)
            : new Match(text, false);
        }
    }
}
