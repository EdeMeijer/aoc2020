using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day17
    {
        private class CoordComparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y) => x.SequenceEqual(y);

            public int GetHashCode(int[] obj) => obj.Length == 3
                ? HashCode.Combine(obj[0], obj[1], obj[2])
                : HashCode.Combine(obj[0], obj[1], obj[2], obj[3]);
        }

        private static readonly CoordComparer _coordComparer = new CoordComparer();

        public static void Part1() => Simulate(3);

        public static void Part2() => Simulate(4);

        private static void Simulate(int nDims) => Console.WriteLine(
            Enumerable.Range(0, 6)
                .Aggregate(LoadInitialState(nDims), (state, _) => Step(state))
                .Count
        );

        private static HashSet<int[]> LoadInitialState(int nDims) => Input.Lines(17)
            .SelectMany((line, y) =>
                line.ToCharArray()
                    .Select((s, x) => (s, x))
                    .Where(t => t.s == '#')
                    .Select(t => nDims == 3 ? new [] {t.x, y, 0} : new [] { t.x, y, 0, 0})
            )
            .ToHashSet(_coordComparer);

        private static HashSet<int[]> Step(HashSet<int[]> state) => ScanBlock(ExtendBoundingBox(GetBoundingBox(state)))
            .Where(coord => ShouldActivate(state.Contains(coord), CountActiveNeighbors(state, coord)))
            .ToHashSet(_coordComparer);

        private static bool ShouldActivate(in bool wasActive, in int activeNeighbors) =>
            activeNeighbors == 3 || (wasActive && activeNeighbors == 2);

        private static (int[] start, int[] end) GetBoundingBox(HashSet<int[]> state)
        {
            var min = state.First()[..];
            var max = min[..];

            foreach (var coord in state)
            {
                foreach (var (v, dim) in coord.Select((v, d) => (v, d)))
                {
                    min[dim] = Math.Min(min[dim], v);
                    max[dim] = Math.Max(max[dim], v);
                }
            }

            return (min, max);
        }

        private static int CountActiveNeighbors(HashSet<int[]> state, int[] coord) =>
            ScanBlock(ExtendBoundingBox((coord, coord)))
                .Count(scanCoord => !scanCoord.SequenceEqual(coord) && state.Contains(scanCoord));

        private static (int[] start, int[] end) ExtendBoundingBox((int[] start, int[] end) boundingBox)
        {
            var (start, end) = boundingBox;
            return (start.Select(v => v - 1).ToArray(), end.Select(v => v + 1).ToArray());
        }

        private static IEnumerable<int[]> ScanBlock((int[] start, int[] end) boundingBox) =>
            ScanBlock(boundingBox.start, boundingBox.end);

        private static IEnumerable<int[]> ScanBlock(int[] min, int[] max)
        {
            var cur = min[..];
            for (;;)
            {
                yield return cur[..];

                for (var dim = 0; dim < min.Length; dim ++)
                {
                    cur[dim] ++;
                    if (cur[dim] > max[dim]) cur[dim] = min[dim];
                    else break;
                }

                if (cur.SequenceEqual(min)) break;
            }
        }
    }
}
