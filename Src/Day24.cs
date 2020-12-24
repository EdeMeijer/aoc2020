using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day24
    {
        private static readonly Regex _pattern = new Regex(@"^([sn]?[we])+$");

        public static void Part1()
        {
            var input = Input.Lines(24);

            var offsets = new Dictionary<string, (int y, int x)>
            {
                ["w"] = (-1, 0),
                ["e"] = (1, 0),
                ["sw"] = (-1, 1),
                ["se"] = (0, 1),
                ["nw"] = (0, -1),
                ["ne"] = (1, -1)
            };

            var instructions = input
                .Select(line => _pattern.Match(line))
                .Select(match => match.Groups[1].Captures.Select(c => offsets[c.Value]))
                .ToList();

            var blackTiles = new HashSet<(int, int)>();

            foreach (var instruction in instructions)
            {
                var tile = instruction.Aggregate((y: 0, x: 0), (cur, step) => (cur.y + step.y, cur.x + step.x));
                if (blackTiles.Contains(tile))
                {
                    blackTiles.Remove(tile);
                }
                else
                {
                    blackTiles.Add(tile);
                }
            }

            Console.WriteLine(blackTiles.Count);
        }
    }
}
