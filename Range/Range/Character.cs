using System;

namespace Range
{
    interface IMatch
    {
        bool Success();

        string RemainingText();
    }

    interface IPattern
    {
        IMatch Match(string text);
    }

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
