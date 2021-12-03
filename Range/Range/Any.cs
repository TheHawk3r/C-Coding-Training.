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
            if (string.IsNullOrEmpty(text))
            {
                return new Match(text, false);
            }

            const int index = 0;
            return Match(text, index);
        }

        public IMatch Match(string text, int index)
        {
            if (index >= this.accepted.Length)
            {
                return new Match(text, false);
            }

            if (text[0] == this.accepted[index])
            {
               return new Match(text[1..], true);
            }

            index++;

            return Match(text, index);
        }
    }
}
