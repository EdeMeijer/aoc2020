using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020.Lib.Day04
{
    public static class PassportValidator
    {
        private static readonly HashSet<string> _requiredFields = new HashSet<string>
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"
        };
        
        private static readonly HashSet<string> _eyeColors = new HashSet<string>
        {
            "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
        };
        
        private static readonly Regex _heightPattern = new Regex(@"^(\d+)(cm|in)$");
        private static readonly Regex _hexColorPattern = new Regex(@"^#[0-9a-f]{6}$");
        private static readonly Regex _pidPattern = new Regex(@"^[0-9]{9}$");

        public static bool IsValid(Dictionary<string, string> passport)
        {
            return HasRequiredFields(passport) && passport.All(Kvp => IsValidValue(Kvp.Key, Kvp.Value));
        }

        public static bool HasRequiredFields(Dictionary<string, string> passport)
        {
            return passport.Keys.Intersect(_requiredFields).Count() == _requiredFields.Count;
        }

        private static bool IsValidValue(string field, string value)
        {
            return field switch
            {
                "byr" => IsIntInRange(value, 1920, 2002),
                "iyr" => IsIntInRange(value, 2010, 2020),
                "eyr" => IsIntInRange(value, 2020, 2030),
                "hgt" => IsValidHeight(value),
                "hcl" => _hexColorPattern.IsMatch(value),
                "ecl" => _eyeColors.Contains(value),
                "pid" => _pidPattern.IsMatch(value),
                _ => true
            };
        }

        private static bool IsIntInRange(string value, int min, int max)
        {
            if (int.TryParse(value, out var parsed))
            {
                return parsed >= min && parsed <= max;
            }

            return false;
        }

        private static bool IsValidHeight(string value)
        {
            var match = _heightPattern.Match(value);
            if (!match.Success)
            {
                return false;
            }

            var numeric = int.Parse(match.Groups[1].Value);
            var isCm = match.Groups[2].Value == "cm";
            var min = isCm ? 150 : 59;
            var max = isCm ? 193 : 76;
            return numeric >= min && numeric <= max;
        }
    }
}
