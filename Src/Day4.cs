using System;
using System.Linq;
using Aoc2020.Lib;
using Aoc2020.Lib.Day4;

namespace Aoc2020
{
    internal static class Day4
    {
        internal static void Part1()
        {
            var input = Input.Read(4);
            var passports = PassportParser.Parse(input).ToList();
            var numValid = passports.Count(PassportValidator.HasRequiredFields);
            Console.WriteLine(numValid);
        }
        
        internal static void Part2()
        {
            var input = Input.Read(4);
            var passports = PassportParser.Parse(input).ToList();
            var numValid = passports.Count(PassportValidator.IsValid);
            Console.WriteLine(numValid);
        }
    }
}
