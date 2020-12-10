using System;
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
    }
}
