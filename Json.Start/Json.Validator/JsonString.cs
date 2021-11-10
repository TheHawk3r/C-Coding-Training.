using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            if (input == null)
            {
                return false;
            }

            if (input.Contains('\n') || input.Contains("\r"))
            {
                return false;
            }

            if (input.Contains(@"\x"))
            {
                return false;
            }

            if (input.EndsWith("\\\""))
            {
                return false;
            }

            return (input.EndsWith('\"') && input.LastIndexOf('\"') != 0)
                && input.StartsWith('\"')
                && input != string.Empty;
        }
    }
}
