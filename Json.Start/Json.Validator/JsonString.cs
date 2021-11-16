using System;

namespace Json
{
    public static class JsonString
    {
        private const int UnicodeEscapedHexNumberLength = 6;

        public static bool IsJsonString(string input)
        {
            if (input == null)
            {
                return false;
            }

            return InputIsDoubleQuoted(input) && CheckInputHasValidCharacters(input) && InputContainsValidEscapeCharacters(input);
        }

        public static bool InputContainsValidEscapeCharacters(string input)
        {
            if (input == null)
            {
                return false;
            }

            const string validEscapeCharacters = "\"\\/bfnrtu";
            for (int i = 1; i < input.Length - 1; i++)
            {
                if (input[i] == '\"' && input[i - 1] != '\\')
                {
                    return false;
                }

                if (input[i] == '\\' && (!validEscapeCharacters.Contains(input[i + 1]) || i + 1 == input.Length - 1) && input[i - 1] != '\\')
                {
                    return false;
                }
            }

            return !input.Contains("\\u") || InputContainsFinishedHexNumbers(input);
        }

        public static bool InputContainsFinishedHexNumbers(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            int currentUnicodeHexIndex = input.IndexOf("\\u");

            return InputContainsFinishedHexNumbers(input, currentUnicodeHexIndex);
        }

        public static bool InputContainsFinishedHexNumbers(string input, int index)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            for (int i = index + 2; i < index + UnicodeEscapedHexNumberLength; i++)
            {
                bool isHexLetter = (input[i] >= 'A' && input[i] <= 'F') || (input[i] >= 'a' && input[i] <= 'f');
                if (!char.IsDigit(input[i]) && !isHexLetter)
                {
                    return false;
                }
            }

            index = input.IndexOf("\\u", index + UnicodeEscapedHexNumberLength);

            if (index == -1)
            {
                return true;
            }

            InputContainsFinishedHexNumbers(input, index);
            return true;
        }

        public static bool CheckInputHasValidCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            const int min = 32;
            const int max = char.MaxValue;
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] < min || input[i] > max)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool InputIsDoubleQuoted(string input)
        {
            if (input == null)
            {
                return false;
            }

            return input.StartsWith("\"") && input.EndsWith('\"') && input.Length > 1;
        }
    }
}
