using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            if (input == null)
            {
                return false;
            }

            return input.StartsWith('\"')
                && input.EndsWith('\"')
                && input != string.Empty;
        }
    }
}
