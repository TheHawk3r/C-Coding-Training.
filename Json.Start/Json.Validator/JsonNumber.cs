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

            bool firstGroupOfConditions = !NumberEndsWithADot(input)
                && !NumberStartsWithADot(input)
                && !NumberHasMultipleExponents(input)
                && NumberExponentIsComplete(input);
            bool secondGropuOfConditions = FractionCanHaveLeadingZeros(input)
                && !NumberHasMultipleFractionParts(input)
                && NumberExponentIsAfterFraction(input, input.Contains('.'), input.Contains('e') || input.Contains('E'))
                && CheckNumberCharactersAreValid(input);
            return firstGroupOfConditions && secondGropuOfConditions;
        }

        static bool NumberExponentIsAfterFraction(string input, bool hasFraction, bool hasExponent)
        {
            if (!hasFraction || !hasExponent)
            {
                return true;
            }

            input = input.ToLower();
            int indexOfFraction = input.IndexOf('.');
            int indexOfExponent = input.IndexOf('e');

            return indexOfExponent > indexOfFraction;
        }

        static bool NumberExponentIsComplete(string input)
        {
            input = input.ToLower();
            int indexOfExponent = input.IndexOf('e');
            if (indexOfExponent == input.Length - 1)
            {
                return false;
            }

            if ((input[indexOfExponent + 1] == '+' || input[indexOfExponent + 1] == '-') && input.Length - 1 == indexOfExponent + 1)
            {
                return false;
            }

            return true;
        }

        static bool NumberHasMultipleFractionParts(string input)
        {
            int countOfDots = input.Length - input.Replace(".", "").Length;

            return countOfDots > 1;
        }

        static bool NumberHasMultipleExponents(string input)
        {
            input = input.ToLower();
            int countOfDots = input.Length - input.Replace("e", "").Length;

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

        static bool CheckNumberCharactersAreValid(string input)
        {
            for (int i = input[0] == '-' ? 1 : 0; i < input.Length; i++)
            {
                if (input[i] == '.' || input[i] == 'e' || input[i] == 'E')
                {
                    continue;
                }

                if (input[i] == '-' || input[i] == '+')
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
