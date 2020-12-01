using System;
using System.Linq;
using aoc2020.lib;

namespace aoc2020
{
    internal static class Day1
    {
        internal static void Run()
        {
            var input = Input.Read(1);
            var numbers = input.Select(int.Parse).ToList();

            foreach (var number in numbers)
            {
                foreach (var number2 in numbers.Where(number2 => number + number2 == 2020))
                {
                    Console.WriteLine(number * number2);
                    return;
                }
            }
        }
    }
}
