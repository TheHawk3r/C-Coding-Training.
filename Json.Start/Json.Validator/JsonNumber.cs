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

            return CheckDigits(integerPart[0] == '-' ? 1 : 0, integerPart);
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

            return CheckDigits(1, fractionPart);
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

            return CheckDigits(Two, exponentPart);
        }

        static bool PartIsEmpty(string partToValidate)
        {
            return 
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

        static bool CheckDigits(int index, string partToValidate)
        {
            for (int i = index; i < partToValidate.Length; i++)
            {
                if (!char.IsDigit(partToValidate[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
