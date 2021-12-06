using System;

namespace Range
{
    class Text : IPattern
    {
        readonly string prefix;

        public Text(string prefix)
        {
            this.prefix = prefix;
        }

        public IMatch Match(string text)
        {
            return text?.StartsWith(this.prefix) == true
            ? new Match(text[this.prefix.Length..], true)
            : new Match(text, false);
        }
    }
}
