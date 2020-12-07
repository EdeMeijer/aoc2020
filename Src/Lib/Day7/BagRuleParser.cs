using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020.Lib.Day7
{
    public static class BagRuleParser
    {
        private static readonly Regex _pattern = new Regex(@"([^ ]+ [^ ]+)[^\d]+(?:(\d+) ([^ ]+ [^ ]+)[^\d]+)+");

        public static BagRule Parse(string rule)
        {
            var match = _pattern.Match(rule);

            return new BagRule(
                match.Groups[1].Value,
                match.Groups[2].Captures
                    .Zip(match.Groups[3].Captures)
                    .ToDictionary(tup => tup.Second!.Value, tup => int.Parse(tup.First!.Value))
            );
        }
    }
}
