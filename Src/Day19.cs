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

        private static bool IsMatch(string input, Dictionary<int, Rule> ruleSet)
        {
            var (match, remaining) = ApplyRule(input, ruleSet, 0);
            return match && remaining == "";
        }

        private static (bool match, string remaining) ApplyRule(string input, Dictionary<int, Rule> ruleSet, int ruleId)
        {
            var rule = ruleSet[ruleId];

            if (rule.Terminal != null)
            {
                return (input.Length > 0 && input[0] == rule.Terminal.Value, input[1..]);
            }

            foreach (var subRule in rule.SubRules!)
            {
                var (subMatch, subRemaining) = ApplySubRule(input, ruleSet, subRule);
                if (subMatch)
                {
                    return (true, subRemaining);
                }
            }

            return (false, "");
        }

        private static (bool match, string remaining) ApplySubRule(
            string input, 
            Dictionary<int, Rule> ruleSet,
            int[] ruleIds
        )
        {
            foreach (var ruleId in ruleIds)
            {
                var (match, remaining) = ApplyRule(input, ruleSet, ruleId);
                if (!match)
                {
                    return (false, "");
                }

                input = remaining;
            }

            return (true, input);
        }

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
