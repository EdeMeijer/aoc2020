using System;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    internal static class Day1
    {
        internal static void Part1()
        {
            var input = Input.Read(1);
            var numbers = input.Select(int.Parse).ToList();

            foreach (var number in numbers)
            {
                foreach (var number2 in numbers.Where(n => number + n == 2020))
                {
                    Console.WriteLine(number * number2);
                    return;
                }
            }
        }
        
        internal static void Part2()
        {
            var input = Input.Read(1);
            var numbers = input.Select(int.Parse).ToList();

            foreach (var number in numbers)
            {
                foreach (var number2 in numbers.Where(n => number + n < 2020))
                {
                    foreach (var number3 in numbers.Where(n => number + number2 + n == 2020))
                    {
                        Console.WriteLine(number * number2 * number3);
                        return;
                    }
                }
            }
        }
    }
}
