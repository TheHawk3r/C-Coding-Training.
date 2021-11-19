using System;

namespace Json
{
    public static class JsonNumber
    {
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
            if (integerPart.StartsWith('0') && integerPart.Length > 1)
            {
                return false;
            }

            return integerPart.Length >= 1 && CheckDigits(integerPart[0] == '-' ? integerPart[1..] : integerPart);
        }

        static string IntegerPart(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex == -1 && exponentIndex == -1)
            {
                return input;
            }

            if (dotIndex == -1)
            {
                return input[..exponentIndex];
            }

            return input[..dotIndex];
        }

        static bool IsValidFraction(string fractionPart)
        {
            if (fractionPart == ".")
            {
                return false;
            }

            return fractionPart == string.Empty || CheckDigits(fractionPart[1..]);
        }

        static string FractionPart(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex == -1)
            {
                return "";
            }

            if (exponentIndex == -1)
            {
                return input[dotIndex..];
            }

            return input[dotIndex..exponentIndex];
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

            exponentPart = exponentPart[1..];

            return CheckDigits(exponentPart[0] == '-' || exponentPart[0] == '+' ? exponentPart[1..] : exponentPart);
        }

        static string ExponentPart(string input, int exponentIndex)
        {
            if (exponentIndex == -1)
            {
                return "";
            }

            return input[exponentIndex..];
        }

        static bool ExponentSignIsValid(char sign)
        {
            return char.IsDigit(sign) || sign == '-' || sign == '+';
        }

        static bool CheckDigits(string partToValidate)
        {
            for (int i = 0; i < partToValidate.Length; i++)
            {
                if (!char.IsDigit(partToValidate[i]))
                {
                    return false;
                }
            }

            return partToValidate.Length >= 1;
        }
    }
}
