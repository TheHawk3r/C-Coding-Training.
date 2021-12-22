using System;

namespace Range
{
    class Choice : IPattern
    {
        private IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            IMatch match = new Match(text, false);
            foreach (IPattern pattern in patterns)
            {
                match = pattern.Match(text);
                if (match.Success())
                {
                    return match;
                }
            }

            return match;
        }

        public void Add(IPattern pattern)
        {
            Array.Resize(ref patterns, patterns.Length + 1);
            patterns[patterns.Length - 1] = pattern;
        }
    }
}
