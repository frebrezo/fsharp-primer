using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpDotNetCoreExamples
{
    public class Program
    {
        public static int Add(IEnumerable<int> numbers)
        {
            // The problem with C# is that numbers could be null, so usually
            //      the code has to account for it.
            if (numbers == null)
                return 0;

            // Imperative
            var result = 0;
            foreach (var num in numbers)
            {
                result += num;
            }
            return result;
            // Linq. C# way of doing functional programming.
            //return numbers?.Any() ?? false ? numbers.Aggregate((x, y) => x + y) : 0;
            //return numbers?.Any() ?? false ? numbers.Sum() : 0;
        }

        public static void AddSample()
        {
            var result = 0;

            // Array
            var numbersArray = new[] { 10, 13, 1, 2, 4, 5 };
            Console.WriteLine($"[{string.Join(',', numbersArray)}]");

            result = Add(numbersArray);
            Console.WriteLine(result);

            // List
            var numbersList = new List<int> { 10, 13, 1, 2, 4, 5 };
            Console.WriteLine($"[{string.Join(',', numbersList)}]");

            result = Add(numbersList);
            Console.WriteLine(result);

            // Singleton
            numbersList = new List<int> {10};
            Console.WriteLine($"[{string.Join(',', numbersList)}]");

            result = Add(numbersList);
            Console.WriteLine(result);

            // Empty
            numbersList = new List<int>();
            Console.WriteLine($"[{string.Join(',', numbersList)}]");

            result = Add(numbersList);
            Console.WriteLine(result);
        }

        public static List<int> GenerateFibonacciNumbers(int numValues)
        {
            if (numValues == 0)
                return new List<int>();
            if (numValues == 1)
                return new List<int> {1};

            var fibonnaciSeq = new List<int> { 1, 1 };
            for (var i = 2; i < numValues; i++)
            {
                var prior = fibonnaciSeq[fibonnaciSeq.Count - 2];
                var last = fibonnaciSeq[fibonnaciSeq.Count - 1];
                fibonnaciSeq.Add(prior + last);
            }
            return fibonnaciSeq;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            AddSample();
            var fibonacciSeq = GenerateFibonacciNumbers(10);
            Console.WriteLine($"[{string.Join(',', fibonacciSeq)}]");
        }
    }
}
