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

            var blackTiles = input
                .Select(line => _pattern.Match(line))
                .Select(match => match.Groups[1].Captures.Select(c => offsets[c.Value]))
                .Select(instr => instr.Aggregate((y: 0, x: 0), (cur, step) => (cur.y + step.y, cur.x + step.x)))
                .GroupBy(tile => tile)
                .Where(group => group.Count() % 2 == 1)
                .Select(group => group.Key)
                .ToHashSet();

            Console.WriteLine(blackTiles.Count);

            for (var i = 0; i < 100; i ++)
            {
                var newBlackTiles = new HashSet<(int y, int x)>();
                for (var y = blackTiles.Min(tile => tile.y) - 1; y <= blackTiles.Max(tile => tile.y) + 1; y ++)
                {
                    for (var x = blackTiles.Min(tile => tile.x) - 1; x <= blackTiles.Max(tile => tile.x) + 1; x ++)
                    {
                        var adjacent = offsets.Values
                            .Count(offset => blackTiles.Contains((y + offset.y, x + offset.x)));

                        if (adjacent == 2 || blackTiles.Contains((y, x)) && adjacent == 1)
                        {
                            newBlackTiles.Add((y, x));
                        }
                    }
                }

                blackTiles = newBlackTiles;
            }
            
            Console.WriteLine(blackTiles.Count);
        }
    }
}
