using System;

namespace Json
{
    public static class JsonNumber
    {
        private const int AsciiDigitRangeMin = 47;
        private const int AsciiDigitRangeMax = 58;

        public static bool IsJsonNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            bool firstGroupOfConditions = !NumberEndsWithADot(input) && !NumberStartsWithADot(input);
            bool secondGropuOfConditions = FractionCanHaveLeadingZeros(input) && !NumberHasMultipleFractionPartss(input) && NumberCharactersAreValid(input);
            return firstGroupOfConditions && secondGropuOfConditions;
        }

        static bool NumberHasMultipleFractionPartss(string input)
        {
            int countOfDots = input.Length - input.Replace(".", "").Length;

            return countOfDots > 1;
        }

        static bool NumberEndsWithADot(string input)
        {
            return input[input.Length - 1] == '.';
        }

        static bool NumberStartsWithZero(string input)
        {
            return input.Length > 1 && input[0] == '0';
        }

        static bool FractionCanHaveLeadingZeros(string input)
        {
            return !(NumberStartsWithZero(input) && !input.Contains('.'));
        }

        static bool NumberStartsWithADot(string input)
        {
            return input.Length > 1 && input[0] == '.';
        }

        static bool NumberCharactersAreValid(string input)
        {
            for (int i = input[0] == '-' ? 1 : 0; i < input.Length; i++)
            {
                if (input[i] == '.')
                {
                    continue;
                }

                if (input[i] < AsciiDigitRangeMin || input[i] > AsciiDigitRangeMax)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
