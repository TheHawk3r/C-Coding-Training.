using System;

namespace Range
{
    class Character : IPattern
    {
        readonly char pattern;

        public Character(char pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            if (!string.IsNullOrEmpty(text) && text[0] == pattern)
            {
                return new Match(text[1..], true);
            }

            return new Match(text, false);
        }
    }
}
