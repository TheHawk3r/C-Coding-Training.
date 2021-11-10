using System;

namespace Json
{
    public static class JsonString
    {
        private const int HexNumberLength = 6;

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

            if (StringEndsWithAFinishedHexNumber(input))
            {
                return false;
            }

            return (input.EndsWith('\"') && input.LastIndexOf('\"') != 0)
                && input.StartsWith('\"')
                && input != string.Empty;
        }

        public static bool StringEndsWithAFinishedHexNumber(string input)
        {
            const bool f = false;
            if (input == null)
            {
                return false;
            }

            return input.LastIndexOf("\\u") >= input.Length - HexNumberLength
                && input.LastIndexOf("\\u") != -1
                ? input.LastIndexOf("\\u") >= input.Length - HexNumberLength
                : f;
        }
    }
}
