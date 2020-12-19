using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day19
    {
        private static readonly Regex RulePattern =
            new Regex(@"^(\d+): (?:(?:(?:((?:\d+ ?)+)(?:\| )?)+)|(?:""([^""]+)""))$");

        private class Rule
        {
            public int[][]? SubRules { get; }
            public char? Terminal { get; }

            public Rule(int[][]? subRules, char? terminal)
            {
                SubRules = subRules;
                Terminal = terminal;
            }
        }

        public static void Part1()
        {
            var input = Input.Text(19);

            var parts = input.Split("\n\n");
            var rules = BuildRuleSet(parts[0].Split('\n'));
            var received = parts[1].Split('\n');

            Console.WriteLine(received.Count(line => IsMatch(line, rules)));
        }

        public static void Part2()
        {
            var input = Input.Text(19);

            var parts = input.Split("\n\n");
            var rules = BuildRuleSet(parts[0].Split('\n'));
            var received = parts[1].Split('\n');

            rules[8] = new Rule(new [] {new [] {42}, new [] {42, 8}}, null);
            rules[11] = new Rule(new [] {new [] {42, 31}, new [] {42, 11, 31}}, null);

            Console.WriteLine(received.Count(line => IsMatch(line, rules)));
        }

        private static bool IsMatch(string input, Dictionary<int, Rule> ruleSet) =>
            ApplyRule(input, ruleSet, 0, new int[0]);

        private static bool ApplyRule(string input, Dictionary<int, Rule> ruleSet, int ruleId, int[] continuation)
        {
            var rule = ruleSet[ruleId];

            if (rule.Terminal != null)
            {
                return input.Length > 0 &&
                       input[0] == rule.Terminal.Value &&
                       ApplyRuleList(input[1..], ruleSet, continuation);
            }

            return rule.SubRules!.Any(subRule => ApplyRuleList(input, ruleSet, subRule.Concat(continuation).ToArray()));
        }

        private static bool ApplyRuleList(string input, Dictionary<int, Rule> ruleSet, int[] ruleIds) =>
            ruleIds.Length == 0 ? input == "" :  ApplyRule(input, ruleSet, ruleIds[0], ruleIds[1..]);

        private static Dictionary<int, Rule> BuildRuleSet(string[] ruleInput)
        {
            var rules = ruleInput
                .Select(line => RulePattern.Match(line))
                .ToDictionary(
                    match => int.Parse(match.Groups[1].Value),
                    match =>
                    {
                        var subRules = match.Groups[2];
                        var terminal = match.Groups[3];

                        return new Rule(
                            subRules.Success
                                ? subRules.Captures
                                    .Select(sub => sub.Value.Trim().Split(' ').Select(int.Parse).ToArray())
                                    .ToArray()
                                : null,
                            terminal.Success ? terminal.Value[0] : (char?) null
                        );
                    }
                );
            return rules;
        }
    }
}
