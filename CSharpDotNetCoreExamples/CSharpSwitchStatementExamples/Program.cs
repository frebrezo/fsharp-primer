using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSwitchStatementExamples
{
    public class Program
    {
        public static bool IsZip(int num) =>
            !IsZoom(num) && num % 3 == 0;

        public static bool IsZap(int num) =>
            !IsZoom(num) && num % 5 == 0;

        public static bool IsZoom(int num) =>
            num % 3 == 0 && num % 5 == 0;

        public static string ZipZapZoom(int num)
        {
            // Switch statements don't work like this in C#.
            //switch (IsZip(num))
            //{
            //    case true: return "Zip";
            //    case false:
            //        switch (IsZap(num))
            //        {
            //            case true: "Zap";
            //            case false:
            //                switch (IsZoom(num))
            //                {
            //                    case true: "Zoom";
            //                    case false: "Invalid";
            //                }
            //        }
            //}
            if (IsZip(num)) return "Zip";
            if (IsZap(num)) return "Zap";
            if (IsZoom(num)) return "Zoom";
            return "Invalid";
        }

        public static void PlayZipZapZoom(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
            {
                Console.WriteLine("Empty list. Can't play Zip Zap Zoom.");
                return;
            }

            foreach (var number in numbers)
            {
                var result = ZipZapZoom(number);
                Console.WriteLine($"{number} {result}");
            }
        }

        public static string ToCsvString(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();

            for (var i = 0; i < numbers.Count; ++i)
            {
                sb.Append(numbers[i]);
                if (i < numbers.Count - 1) sb.Append(",");
            }

            return sb.ToString();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            PlayZipZapZoom(new List<int> { 9, 10, 15, 19 });
            PlayZipZapZoom(new List<int>());
            PlayZipZapZoom(null);

            string result = null;

            result = ToCsvString(new List<int> {9, 10, 15, 19});
            Console.WriteLine(result);

            result = ToCsvString(new List<int> { 9 });
            Console.WriteLine(result);

            result = ToCsvString(new List<int>());
            Console.WriteLine(result);

            result = ToCsvString(null);
            Console.WriteLine(result);
        }
    }
}
