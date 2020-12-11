using System;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    internal static class Day05
    {
        internal static void Part1()
        {
            var input = Input.Lines(5);
            var maxId = input.Select(Day5Calculator.CalcSeatId).Max();
            Console.WriteLine(maxId);
        }

        internal static void Part2()
        {
            var input = Input.Lines(5);

            // Build set of all possible seat numbers
            var allSeats = Enumerable.Range(1, 126)
                .SelectMany(row => Enumerable.Range(0, 8).Select(col => Day5Calculator.CalcSeatId(row, col)))
                .ToHashSet();

            // Scanned seats
            var foundSeats = input.Select(Day5Calculator.CalcSeatId).ToHashSet();

            var remaining = allSeats.Except(foundSeats);

            // Find seat that has an ID +1 and -1 of it in the list of scanned seats
            var mine = remaining
                .First(id => foundSeats.Contains(id - 1) && foundSeats.Contains(id + 1));

            Console.WriteLine(mine);
        }
    }
}
