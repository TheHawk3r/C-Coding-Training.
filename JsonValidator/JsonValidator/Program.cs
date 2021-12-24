using System;
using Range;

namespace JsonValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonValue = new Value();
            string text = System.IO.File.ReadAllText(args[0]);
            Console.WriteLine($"{jsonValue.Match(text).Success()}");
        }
    }
}
