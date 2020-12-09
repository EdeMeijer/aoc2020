using System;
using System.Collections.Immutable;
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

        public static long Part2(string[] input, int windowLength = 25)
        {
            var numbers = input.Select(long.Parse).ToArray();
            var target = Part1(input, windowLength);

            var blockSize = 0;
            var sum = 0L;
            for (var i = 0; i < numbers.Length; i ++)
            {
                if (i > 0)
                {
                    // Move start of the block forward
                    sum -= numbers[i - 1];
                    blockSize --;
                }
                while (sum < target)
                {
                    // Include numbers
                    sum += numbers[i + blockSize];
                    blockSize ++;
                }

                while (sum > target)
                {
                    // Exclude numbers
                    blockSize --;
                    sum -= numbers[i + blockSize];
                }

                if (sum == target)
                {
                    var usedNumbers = numbers[i .. (i + blockSize)].ToImmutableSortedSet();
                    return usedNumbers.Min + usedNumbers.Max;
                }
            }
            
            throw new ApplicationException("Could not find result");
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
