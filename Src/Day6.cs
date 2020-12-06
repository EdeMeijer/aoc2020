using System;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    internal static class Day6
    {
        internal static void Part1()
        {
            var input = Input.Text(6);

            var result = input
                .Split("\n\n")
                .Sum(group => group.ToCharArray().Where(c => c != '\n').ToHashSet().Count);
            
            Console.WriteLine(result);
        }

        internal static void Part2()
        {
            var input = Input.Text(6);
        }
    }
}
