using System;
using System.Linq;
using System.Text.RegularExpressions;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day2
    {
        private static readonly Regex entryPattern = new Regex(@"^(\d+)-(\d+) ([a-z]): (.*)$");

        internal static void Part1()
        {
            var input = Input.Read(2);
            Console.WriteLine(input.Count(isValidPart1));
        }

        internal static void Part2()
        {
            var input = Input.Read(2);
            Console.WriteLine(input.Count(isValidPart2));
        }

        public static bool isValidPart1(string entry)
        {
            var parsed = ParseEntry(entry);
            var policy = parsed.Policy;
            var charCount = parsed.Password.ToCharArray().Count(c => c == policy.Character);
            return charCount >= policy.Min && charCount <= policy.Max;
        }

        public static bool isValidPart2(string entry)
        {
            var parsed = ParseEntry(entry);
            var policy = parsed.Policy;
            var password = parsed.Password;
            return policy.Positions.Count(pos => passwordContainsAt(password, policy.Character, pos)) == 1;
        }

        private static bool passwordContainsAt(string password, char character, int position)
        {
            position --;
            if (position >= password.Length)
            {
                return false;
            }

            return password[position] == character;
        }

        private static Entry ParseEntry(string entry)
        {
            var match = entryPattern.Match(entry);
            if (!match.Success)
            {
                throw new ArgumentException($"Could not parse entry {entry}");
            }

            var policy = new Policy
            {
                Min = int.Parse(match.Groups[1].Value),
                Max = int.Parse(match.Groups[2].Value),
                Character = match.Groups[3].Value[0]
            };

            return new Entry
            {
                Password = match.Groups[4].Value,
                Policy = policy
            };
        }
    }

    public class Policy
    {
        public char Character { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int[] Positions => new [] { Min, Max };
    }

    public class Entry
    {
        public Policy Policy { get; set; }
        public string Password { get; set; }
    }
}
