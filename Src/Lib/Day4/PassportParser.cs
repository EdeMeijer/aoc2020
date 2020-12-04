using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020.Lib.Day4
{
    public static class PassportParser
    {
        private static readonly Regex pattern = new Regex(@"([a-z]{3}):([^\s]+)");

        public static IEnumerable<Dictionary<string, string>> Parse(string[] batch)
        {
            var passport = new Dictionary<string, string>();
            foreach (var line in batch)
            {
                if (line.Trim() == "")
                {
                    // End of passport
                    yield return passport;
                    passport = new Dictionary<string, string>();
                }

                var matches = pattern.Matches(line);
                foreach (Match match in matches)
                {
                    passport.Add(match.Groups[1].Value, match.Groups[2].Value);
                }
            }

            if (passport.Any())
            {
                yield return passport;
            }
        }
    }
}
