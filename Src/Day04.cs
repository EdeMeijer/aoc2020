using System;
using System.Linq;
using Aoc2020.Lib;
using Aoc2020.Lib.Day04;

namespace Aoc2020
{
    internal static class Day04
    {
        internal static void Part1()
        {
            var input = Input.Text(4);
            var passports = PassportParser.Parse(input).ToList();
            var numValid = passports.Count(PassportValidator.HasRequiredFields);
            Console.WriteLine(numValid);
        }
        
        internal static void Part2()
        {
            var input = Input.Text(4);
            var passports = PassportParser.Parse(input).ToList();
            var numValid = passports.Count(PassportValidator.IsValid);
            Console.WriteLine(numValid);
        }
    }
}
