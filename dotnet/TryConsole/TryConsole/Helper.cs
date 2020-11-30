using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace TryConsole
{
    public static class Helper
    {
        private static readonly Dictionary<int, int> FibonacciMemo = new Dictionary<int, int>();

        /// <summary>
        /// Requirement:
        ///     Write a program that outputs all possibilities to put + or - or nothing
        ///     between the numbers 1, 2, …, 9 (in this order) such that the result is
        ///     always 100. For example: 1 + 2 + 34 – 5 + 67 – 8 + 9 = 100.
        /// Solution:
        ///     Brute force method.
        /// </summary>
        public static IList<string> Expression100()
        {
            var operators = new string[] { "+", "-", string.Empty };
            var computer = new DataTable();
            var result = new List<string>();
            foreach (var o1 in operators)
            {
                foreach (var o2 in operators)
                {
                    foreach (var o3 in operators)
                    {
                        foreach (var o4 in operators)
                        {
                            foreach (var o5 in operators)
                            {
                                foreach (var o6 in operators)
                                {
                                    foreach (var o7 in operators)
                                    {
                                        foreach (var o8 in operators)
                                        {
                                            var expression = $"1{o1}2{o2}3{o3}4{o4}5{o5}6{o6}7{o7}8{o8}9";
                                            if ((int)computer.Compute(expression, string.Empty) == 100)
                                            {
                                                result.Add(expression);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static int Fibonacci(int n)
        {
            if (FibonacciMemo.ContainsKey(n))
            {
                return FibonacciMemo[n];
            }

            var result = n <= 2
                ? 1
                : Fibonacci(n - 1) + Fibonacci(n - 2);

            FibonacciMemo.Add(n, result);
            return result;
        }

        public static void Overwrite(string filePath, string content)
        {
            using (var writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine(content);
            }
        }

        public static async IAsyncEnumerable<int> GenerateSequence(int n)
        {
            for (int i = 0; i < n; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }
    }
}
