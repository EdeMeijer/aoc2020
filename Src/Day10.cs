using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day10
    {
        public static void Part1()
        {
            var adapters = Input.Lines(10).Select(int.Parse).ToImmutableList().Sort();

            var deltas = adapters.Prepend(0).Zip(adapters.Add(adapters.Max() + 3))
                .Select(tup => tup.Second - tup.First)
                .GroupBy(delta => delta)
                .ToDictionary(group => group.Key, group => group.Count());

            Console.WriteLine(deltas[1] * deltas[3]);
        }

        public static void Part2()
        {
            var input =  Input.Lines(10);
            var adapters = input.Select(int.Parse).ToImmutableList();
            var deviceRating = adapters.Max() + 3;
            adapters = adapters.Add(deviceRating);

            var cache = new Dictionary<int, long>();

            long Calc(int inputRating)
            {
                if (inputRating == deviceRating)
                {
                    return 1;
                }

                if (cache.TryGetValue(inputRating, out var cachedResult))
                {
                    return cachedResult;
                }

                var result = adapters
                    .Where(rating =>
                    {
                        var delta = rating - inputRating;
                        return delta > 0 && delta <= 3;
                    })
                    .Select(Calc)
                    .Sum();

                cache.Add(inputRating, result);
                return result;
            }

            Console.WriteLine(Calc(0));
        }
    }
}
