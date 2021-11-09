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

            return (input.EndsWith('\"') && input.LastIndexOf('\"') != 0)
                && input.StartsWith('\"')
                && input != string.Empty;
        }
    }
}
