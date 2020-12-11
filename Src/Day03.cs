using System;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    internal static class Day03
    {
        internal static void Part1()
        {
            var input = Input.Lines(3);
            Console.WriteLine(new Day03Solver(input).GetNumTrees(3, 1));
        }
        
        internal static void Part2()
        {
            var input = Input.Lines(3);
            var slopes = new (int right, int down)[]
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };
            
            var resolver = new Day03Solver(input);
            var result = slopes
                .Select(tup => resolver.GetNumTrees(tup.right, tup.down))
                .Aggregate(1L, (a, b) => a * b);
            
            Console.WriteLine(result);
        }
    }
}
