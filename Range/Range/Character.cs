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

        public bool Match(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return text[0] == pattern;
        }

        IMatch IPattern.Match(string text)
        {
            return new Match(text, this.pattern);
        }
    }
}
