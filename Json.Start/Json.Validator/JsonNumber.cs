using System;

namespace Json
{
    public static class JsonNumber
    {
        private const int Two = 2;

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
            if (integerPart.Length < 1)
            {
                return false;
            }

            if (integerPart.Length == 1)
            {
                return char.IsDigit(integerPart[0]);
            }

            if (integerPart[0] == '0')
            {
                return false;
            }

            for (int i = integerPart[0] == '-' ? 1 : 0; i < integerPart.Length; i++)
            {
                if (!char.IsDigit(integerPart[i]))
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

        static bool IsValidFraction(string fractionPart)
        {
            if (fractionPart == "")
            {
                return true;
            }

            if (fractionPart == ".")
            {
                return false;
            }

            for (int i = 1; i < fractionPart.Length; i++)
            {
                if (!char.IsDigit(fractionPart[i]))
                {
                    return false;
                }
            }

            return true;
        }

        static string FractionPart(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex == -1)
            {
                return "";
            }

            if (exponentIndex == -1)
            {
                return input.Remove(0, dotIndex);
            }

            input = input.Remove(exponentIndex);
            input = input.Remove(0, dotIndex);

            return input;
        }

        static bool IsValidExponent(string exponentPart)
        {
            if (exponentPart == "")
            {
                return true;
            }

            if (exponentPart.Length == 1 || !ExponentSignIsValid(exponentPart[1]))
            {
                return false;
            }

            if (exponentPart.Length == Two && (exponentPart[1] == '+' || exponentPart[1] == '-'))
            {
                return false;
            }

            for (int i = 2; i < exponentPart.Length; i++)
            {
                if (!char.IsDigit(exponentPart[i]))
                {
                    return false;
                }
            }

            return true;
        }

        static string ExponentPart(string input, int exponentIndex)
        {
            if (exponentIndex == -1)
            {
                return "";
            }

            return input.Remove(0, exponentIndex);
        }

        static bool ExponentSignIsValid(char sign)
        {
            return char.IsDigit(sign) || sign == '-' || sign == '+';
        }
    }
}
