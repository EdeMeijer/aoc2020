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

            var errorRate = nearby
                .Sum(ticket => ticket.Sum(entry => rules.Values.Any(rule => rule(entry)) ? 0 : entry));

            Console.WriteLine(errorRate);
        }

        public static void Part2()
        {
            var input = Input.Text(16);
            var parts = input.Split("\n\n");

            var rules = ParseRules( parts[0].Split('\n'));
            var mine = ParseTicket(parts[1].Split('\n')[1]);
            var nearby = parts[2].Split('\n').Skip(1).Select(ParseTicket).ToList();

            var validNearby = nearby
                .Where(ticket => ticket.All(entry => rules.Values.Any(rule => rule(entry))))
                .ToList();

            var possible = new Dictionary<int, List<string>>();
            foreach (var i in Enumerable.Range(0, rules.Count))
            {
                possible[i] = new List<string>();
                foreach (var (name, predicate) in rules)
                {
                    if (validNearby.All(ticket => predicate(ticket[i])))
                    {
                        possible[i].Add(name);
                    }
                }
            }

            var order = new string[rules.Count];
            for (var i = 0; i < rules.Count; i ++)
            {
                var next = possible.First(r => r.Value.Count == 1);
                var rule = next.Value.First();
                order[next.Key] = rule;
                foreach (var list in possible.Values)
                {
                    list.Remove(rule);
                }
            }

            var result = order
                .Select((rule, i) => (rule, mine[i]))
                .Where(tup => tup.rule.StartsWith("departure"))
                .Aggregate(1L, (a, b) => a * b.Item2);

            Console.WriteLine(result);
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
