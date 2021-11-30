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
            var match = new Match(text);

            if (text[0] == pattern)
            {
                return new Match(match.RemainingText());
            }

            return match;
        }
    }
}
