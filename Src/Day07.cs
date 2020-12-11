using System;
using System.Linq;
using Aoc2020.Lib;
using Aoc2020.Lib.Day07;

namespace Aoc2020
{
    internal static class Day07
    {
        internal static void Part1()
        {
            var input = Input.Lines(7);

            var policy = new BagPolicy(input.Select(BagRuleParser.Parse));

            var result = policy.GetRulesContaining("shiny gold").Count();
            
            Console.WriteLine(result);
        }

        internal static void Part2()
        {
            var input = Input.Lines(7);

            var policy = new BagPolicy(input.Select(BagRuleParser.Parse));

            var result = policy.GetTotalBagsContainedBy("shiny gold");
            
            Console.WriteLine(result);
        }
    }
}
