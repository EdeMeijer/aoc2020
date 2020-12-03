using System;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    internal static class Day3
    {
        internal static void Part1()
        {
            var input = Input.Read(3);
            Console.WriteLine(new Day3Resolver(input).Resolve(3, 1));
        }
        
        internal static void Part2()
        {
            var input = Input.Read(3);
            var slopes = new (int right, int down)[]
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };
            
            var resolver = new Day3Resolver(input);
            var result = slopes
                .Select(tup => resolver.Resolve(tup.right, tup.down))
                .Aggregate(1L, (a, b) => a * b);
            
            Console.WriteLine(result);
        }
    }
}
