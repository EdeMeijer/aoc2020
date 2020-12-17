using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    using PointBag = HashSet<(int x, int y, int z)>;

    public static class Day17
    {
        private static readonly PointBag _offsets = new PointBag();

        static Day17()
        {
            for (var dx = -1; dx <= 1; dx ++)
            {
                for (var dy = -1; dy <= 1; dy ++)
                {
                    for (var dz = -1; dz <= 1; dz ++)
                    {
                        if (dx != 0 || dy != 0 || dz != 0)
                        {
                            _offsets.Add((dx, dy, dz));
                        }
                    }
                }
            }
        }

        public static void Part1()
        {
            var input = @".##.####
.#.....#
#.###.##
#####.##
#...##.#
#######.
##.#####
.##...#.";

            var state = input.Split('\n')
                .SelectMany((line, y) =>
                    line.ToCharArray()
                        .Select((s, x) => (s, x))
                        .Where(t => t.s == '#')
                        .Select(t => (t.x, y, z: 0))
                )
                .ToHashSet();

            for (var cycle = 0; cycle < 6; cycle ++)
            {
                state = Simulate(state);
            }

            Console.WriteLine(state.Count());
        }

        private static PointBag Simulate(PointBag state)
        {
            var result = new PointBag();

            // Determine simulation bounding box
            var xMin = state.Min(p => p.x) - 1;
            var xMax = state.Max(p => p.x) + 1;
            var yMin = state.Min(p => p.y) - 1;
            var yMax = state.Max(p => p.y) + 1;
            var zMin = state.Min(p => p.z) - 1;
            var zMax = state.Max(p => p.z) + 1;

            for (var x = xMin; x <= xMax; x ++)
            {
                for (var y = yMin; y <= yMax; y ++)
                {
                    for (var z = zMin; z <= zMax; z ++)
                    {
                        var wasActive = state.Contains((x, y, z));
                        var activeNeighbors = CountActiveNeighbors(state, x, y, z);
                        if (ShouldActivate(wasActive, activeNeighbors))
                        {
                            result.Add((x, y, z));
                        }
                    }
                }
            }

            return result;
        }

        private static int CountActiveNeighbors(PointBag state, int x, int y, int z)
        {
            var result = 0;

            for (var dx = -1; dx <= 1; dx ++)
            {
                for (var dy = -1; dy <= 1; dy ++)
                {
                    for (var dz = -1; dz <= 1; dz ++)
                    {
                        if (dx != 0 || dy != 0 || dz != 0)
                        {
                            if (state.Contains((x + dx, y + dy, z + dz)))
                            {
                                result ++;
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static bool ShouldActivate(in bool wasActive, in int activeNeighbors)
        {
            if (wasActive)
            {
                return activeNeighbors == 2 || activeNeighbors == 3;
            }

            return activeNeighbors == 3;
        }
    }
}
