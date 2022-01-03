using System;
using Range;

namespace JsonValidator
{
    class Program
    {
        static void Main(string[] args)
            {
            if (args.Length == 0)
            {
                Console.WriteLine("Please input the path to the file that needs to be validated.");
                return;
            }

            var jsonValue = new Value();
            string text = System.IO.File.ReadAllText(args[0]);
            bool isValidJsonFile = jsonValue.Match(text).Success() && jsonValue.Match(text).RemainingText() == string.Empty;

            Console.WriteLine(isValidJsonFile);
        }
    }
}
