using System;

namespace Json
{
    public static class JsonString
    {
        private const int HexNumberLength = 6;
        private const int JsonEscapeCharacters = 10;

        public static bool IsJsonString(string input)
        {
            if (input == null)
            {
                return false;
            }

            return InputIsDoubleQuoted(input) && CheckInputHasValidCharacters(input);
        }

        public static bool FirstGroupOfConditionsToValidateJsonString(string input)
        {
            return InputIsDoubleQuoted(input)
                && !InputHasControlCharacters(input)
                && !StringEndsWithAFinishedHexNumber(input);
        }

        public static bool SecondGroupOfConditionsToValidateJsonString(string input)
        {
            return !InputContainsUnrecognizedEscapeCharacters(input)
                && !InputEndsWithReverseSolidus(input);
        }

        public static bool InputEndsWithReverseSolidus(string input)
        {
            if (input == null)
            {
                return false;
            }

            return input.EndsWith("\\\"");
        }

        public static bool InputContainsUnrecognizedEscapeCharacters(string input)
        {
            if (input == null)
            {
                return false;
            }

            int escapeCharactersNotFoundCount = 0;
            char[] escapeCharacters = new[] { '"', '\\', '/', 'b', 'f', 'n', 'r', 't', 'u', ' ' };
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\')
                {
                    escapeCharactersNotFoundCount = 0;
                    foreach (char c in escapeCharacters)
                    {
                        escapeCharactersNotFoundCount += input[i + 1] == c ? 0 : 1;
                    }

                    if (escapeCharactersNotFoundCount >= JsonEscapeCharacters)
                    {
                        break;
                    }
                }
            }

            return escapeCharactersNotFoundCount >= JsonEscapeCharacters;
        }

        public static bool CheckInputHasValidCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            const int min = 32;
            const int max = 1114111;
            const int solidus = '\u002F';
            const int doubleQuotes = '\u0022';
            for (int i = 0; i < input.Length - 1; i++)
            {
                if ((input[i] < min && input[i] > max) && (input[i] == solidus || input[i] == doubleQuotes))
                {
                    return false;
                }
            }

            return true;
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

        public static bool InputIsDoubleQuoted(string input)
        {
            if (input == null)
            {
                return false;
            }

            return input.StartsWith("\"") && input.EndsWith('\"') && input.Length > 1;
        }

        public static bool InputIsNull(string input)
        {
            return input == null;
        }

        public static bool InputHasControlCharacters(string input)
        {
            if (input == null)
            {
                return false;
            }

            foreach (char c in input)
            {
                if (char.IsControl(c))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
