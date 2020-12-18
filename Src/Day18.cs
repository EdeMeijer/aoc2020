using System;
using System.Collections.Generic;
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
            Console.WriteLine(input.Sum(line => Solve(line, false)));
        }
        
        public static void Part2()
        {
            var input = Input.Lines(18);
            Console.WriteLine(input.Sum(line => Solve(line, true)));
        }
        
        private static long Solve(string expr, bool additionPrecedence)
        {
            var (result, _) = Eval(expr.Replace(" ", ""), additionPrecedence);
            return result;
        }

        private static (long value, string remaining) Eval(string input, bool additionPrecedence)
        {
            var values = new List<long>();
            var ops = new List<char>();

            while (input != "" && input[0] != ')')
            {
                if (input[0] == '(')
                {
                    var (value, remaining) = Eval(input[1..], additionPrecedence);
                    input = remaining[1..];
                    values.Add(value);
                }
                else if (input[0] == '*' || input[0] == '+')
                {
                    ops.Add(input[0]);
                    input = input[1..];
                }
                else
                {
                    var (value, remaining) = ReadNumber(input);
                    input = remaining;
                    values.Add(value);
                }
            }

            while (values.Count > 1)
            {
                var opIdx = additionPrecedence ? Math.Max(ops.IndexOf('+'), 0) : 0;
                var op = ops[opIdx];
                ops.RemoveAt(opIdx);

                var v1 = values[opIdx + 1];
                values.RemoveAt(opIdx + 1);
                var v0 = values[opIdx];

                var combined = op == '+' ? v0 + v1 : v0 * v1;
                values[opIdx] = combined;
            }

            return (values[0], input);
        }

        private static (long value, string remaining) ReadNumber(string input)
        {
            var match = NumberPattern.Match(input);
            var number = match.Groups[0].Value;
            return (long.Parse(number), input[number.Length..]);
        }
    }
}
