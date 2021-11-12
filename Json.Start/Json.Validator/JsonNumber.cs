using System;

namespace Json
{
    public static class JsonNumber
    {
        private const int AsciiDigitRangeMin = 47;
        private const int AsciiDigitRangeMax = 58;

        public static bool IsJsonNumber(string input)
        {
            if (input == null)
            {
                return false;
            }

            if (input == string.Empty)
            {
                return false;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < AsciiDigitRangeMin || input[i] > AsciiDigitRangeMax)
                {
                    return false;
                }
            }

            return input[0] > AsciiDigitRangeMin && input[0] < AsciiDigitRangeMax;
        }
    }
}
