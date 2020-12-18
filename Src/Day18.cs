using System;
using System.Linq;
using System.Text.RegularExpressions;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day18
    {
        private static readonly Regex NumberPattern = new Regex(@"^[0-9]+");
        
        public static void Part1()
        {
            var input = Input.Lines(18);
            Console.WriteLine(input.Sum(Solve));
        }
        
        private static long Solve(string expr)
        {
            var (result, _) = Eval(expr.Replace(" ", ""));
            return result;
        }

        private static (long value, string remaining) Eval(string input)
        {
            var result = 0L;
            char? op = null;

            while (input != "" && input[0] != ')')
            {
                long? value = null;
                if (input[0] == '(')
                {
                    (value, input) = Eval(input[1..]);
                    input = input[1..];
                }
                else if (input[0] == '*' || input[0] == '+')
                {
                    op = input[0];
                    input = input[1..];
                }
                else
                {
                    (value, input) = ReadNumber(input);
                }

                if (value != null)
                {
                    result = op switch
                    {
                        null => value.Value,
                        '*' => result * value.Value,
                        '+' => result + value.Value
                    };
                }
            }

            return (result, input);
        }

        private static (long value, string remaining) ReadNumber(string input)
        {
            var match = NumberPattern.Match(input);
            var number = match.Groups[0].Value;
            return (long.Parse(number), input[number.Length..]);
        }
    }
}
