using System;
using System.Linq;

namespace Aoc2020.Lib.Day9
{
    public class Day9Solver
    {
        public static long Part1(string[] input, int windowLength = 25)
        {
            var numbers = input.Select(long.Parse).ToArray();

            foreach (var (number, i) in numbers.Select((n, i) => (n, i)).Skip(windowLength))
            {
                var factors = Factorize(number, numbers[(i - windowLength) .. i]);
                if (factors == null)
                {
                    return number;
                }
            }

            throw new ApplicationException("No number found that could not be factored");
        }

        private static (long, long)? Factorize(long number, long[] window)
        {
            foreach (var n1 in window)
            {
                foreach (var n2 in window)
                {
                    if (n1 != n2 && n1 + n2 == number)
                    {
                        return (n1, n2);
                    }
                }
            }

            return null;
        }
    }
}
