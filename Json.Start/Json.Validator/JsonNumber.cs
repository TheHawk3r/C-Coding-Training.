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

            if (NumberStartsWithZeroOrADot_NonFracionalNumber(input))
            {
                return false;
            }

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

            return input[0] > AsciiDigitRangeMin && input[0] < AsciiDigitRangeMax || input[0] == '-';
        }

        static bool NumberStartsWithZeroOrADot_NonFracionalNumber(string input)
        {
            bool inputLengthIsBiggerThenOneAndFirstInputIndexEqualsCharacterZeroAndDoesNotContainDot = input.Length > 1 && input[0] == '0' && !input.Contains('.');
            bool inputLengthIsBiggerThenOneAndFirstInputIndexEqualsCharacterDot = input.Length > 1 && input[0] == '.';
            return inputLengthIsBiggerThenOneAndFirstInputIndexEqualsCharacterZeroAndDoesNotContainDot || inputLengthIsBiggerThenOneAndFirstInputIndexEqualsCharacterDot;
        }
    }
}
