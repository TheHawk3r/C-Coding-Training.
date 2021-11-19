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

            var dotIndex = input.IndexOf('.');
            var exponentIndex = input.IndexOfAny("eE".ToCharArray());

            return IsValidInteger(IntegerPart(input, dotIndex, exponentIndex))
            && IsValidFraction(FractionPart(input, dotIndex, exponentIndex))
            && IsValidExponent(ExponentPart(input, exponentIndex));
        }


        static bool IsValidInteger(string integerPart)
        {
            if (integerPart.Length == 1)
            {
                return char.IsDigit(integerPart[0]);
            }

            for (int i = integerPart[0] == '-' ? 1 : 0; i < integerPart.Length; i++)
            {
                if (integerPart[i] < AsciiDigitRangeMin || integerPart[i] > AsciiDigitRangeMax)
                {
                    return false;
                }
            }

            return true;
        }

        static string IntegerPart(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex == -1 && exponentIndex == -1)
            {
                return input;
            }

            if (dotIndex == -1)
            {
                return input.Remove(exponentIndex);
            }

            return input.Remove(dotIndex);
        }

        static bool IsValidFraction(string FractionPart)
        {
            for (int i = 0; i < FractionPart.Length; i++)
            {
                if (FractionPart[i] == '.' || FractionPart[i] == 'e' || FractionPart[i] == 'E')
                {
                    continue;
                }

                if (FractionPart[i] == '-' || FractionPart[i] == '+')
                {
                    continue;
                }

                if (FractionPart[i] < AsciiDigitRangeMin || FractionPart[i] > AsciiDigitRangeMax)
                {
                    return false;
                }
            }

            return true;
        }

        static bool NumberExponentIsAfterFraction(string input, bool hasFraction, bool hasExponent)
        {
            if (!hasFraction || !hasExponent)
            {
                return true;
            }

            bool firstGroupOfConditions =
               !NumberEndsWithADot(input)
               && !NumberStartsWithADot(input)
               && !NumberHasMultipleExponents(input)
               && NumberExponentIsComplete(input);

            bool secondGropuOfConditions =
                FractionCanHaveLeadingZeros(input)
                && !NumberHasMultipleFractionParts(input)
                && NumberExponentIsAfterFraction(input, input.Contains('.'), input.Contains('e') || input.Contains('E'))
                && CheckNumberCharactersAreValid(input);

            input = input.ToLower();
            int indexOfFraction = input.IndexOf('.');
            int indexOfExponent = input.IndexOf('e');

            return indexOfExponent > indexOfFraction;
        }

        static bool NumberHasTooManyPlusesOrMinuses(string input)
        {
            int countOfPluses = input.Length - input.Replace("+", "").Length;
            if (input[0] == '-')
            {
                input = input.Remove(0, 1);
            }

            int countOfMinuses = input.Length - input.Replace("-", "").Length;

            return countOfMinuses > 1 || countOfPluses > 1;
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

            return !NumberHasTooManyPlusesOrMinuses(input);
        }

        static bool NumberHasMultipleFractionParts(string input)
        {
            int countOfDots = input.Length - input.Replace(".", "").Length;

            return countOfDots > 1;
        }

        static bool NumberHasMultipleExponents(string input)
        {
            input = input.ToLower();
            int countOfExponents = input.Length - input.Replace("e", "").Length;

            return countOfExponents > 1;
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
