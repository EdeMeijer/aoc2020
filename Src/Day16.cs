using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day16
    {
        private static readonly Regex RulePattern = new Regex(@"^([^:]+): (\d+)\-(\d+) or (\d+)\-(\d+)$");

        public static void Part1()
        {
            var input = Input.Text(16);
            var parts = input.Split("\n\n");

            var rules = ParseRules( parts[0].Split('\n'));
            var nearby = parts[2].Split('\n').Skip(1).Select(ParseTicket).ToList();

            var errorRate = 0;
            foreach (var ticket in nearby)
            {
                foreach (var entry in ticket)
                {
                    if (!rules.Any(rule => rule.Value.Invoke(entry)))
                    {
                        errorRate += entry;
                    }
                }
            }

            Console.WriteLine(errorRate);
        }

        private static Dictionary<string, Func<int, bool>> ParseRules(IEnumerable<string> lines)
        {
            return lines
                .Select(line => RulePattern.Match(line))
                .Select(m => (
                        m.Groups[1].Value,
                        int.Parse(m.Groups[2].Value),
                        int.Parse(m.Groups[3].Value),
                        int.Parse(m.Groups[4].Value),
                        int.Parse(m.Groups[5].Value)
                    )
                )
                .ToDictionary(
                    tup => tup.Value,
                    tup =>
                    {
                        var (_, min1, max1, min2, max2) = tup;
                        Func<int, bool> res = v => (v >= min1 && v <= max1) || (v >= min2 && v <= max2);
                        return res;
                    });
        }

        private static List<int> ParseTicket(string line) => line.Split(',').Select(int.Parse).ToList();
    }
}
