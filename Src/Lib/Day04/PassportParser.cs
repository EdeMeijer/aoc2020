using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020.Lib.Day04
{
    public static class PassportParser
    {
        private static readonly Regex pattern = new Regex(@"([a-z]{3}):([^\s]+)");

        public static IEnumerable<Dictionary<string, string>> Parse(string batch) =>
            batch
                .Split("\n\n")
                .Select(passport => pattern.Matches(passport)
                    .ToDictionary(match => match.Groups[1].Value, match => match.Groups[2].Value)
                );
    }
}
