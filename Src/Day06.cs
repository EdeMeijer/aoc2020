using System;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    internal static class Day06
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

            var result = input
                .Split("\n\n")
                .Sum(group =>
                {
                    var groupSize = group.Split('\n').Length;
                    var answers = group.ToCharArray().Where(c => c != '\n').ToList();
                    return answers.ToHashSet()
                        .Count(uniqAnswer => answers.Count(a => a == uniqAnswer) == groupSize);
                });

            Console.WriteLine(result);
        }
    }
}
