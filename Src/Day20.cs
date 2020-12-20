using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day20
    {
        private class Tile : IEquatable<Tile>
        {
            public long Id { get; }
            public IMatrix<char> Data { get; }

            public Tile(long id, IMatrix<char> data)
            {
                Id = id;
                Data = data;
            }

            public Tile RotateCw() => new Tile(Id, Data.RotateCw());

            public Tile FlipHorizontal() => new Tile(Id, Data.FlipHorizontal());

            public bool Equals(Tile? other)
            {
                return other != null &&
                       other.Id == Id &&
                       other.Data.Equals(Data);
            }
        }

        public static void Part1()
        {
            var input = Input.Text(20);

            var tiles = input.Split("\n\n")
                .Select(t => t.Split(':'))
                .Select(p => new Tile(
                    long.Parse(p[0].Split(' ')[1]),
                    new Matrix<char>(10, 10, p[1].Replace("\n", ""))
                ))
                .ToImmutableList();

            var totalDim = (int) Math.Sqrt(tiles.Count);
            var emptyResult = new Matrix<Tile>(totalDim, totalDim, (Tile) null);

            var solution = Solve(emptyResult, 0, 0, tiles);
            if (solution == null)
            {
                throw new ApplicationException("Could not solve");
            }

            var checksum = solution[0, 0].Id *
                           solution[0, totalDim - 1].Id *
                           solution[totalDim - 1, 0].Id *
                           solution[totalDim - 1, totalDim - 1].Id;

            Console.WriteLine(checksum);
        }

        private static  IMatrix<Tile>? Solve(IMatrix<Tile> solution, int y, int x, ImmutableList<Tile> remainingTiles)
        {
            foreach (var (tile, i) in remainingTiles.Select((t, i) => (t, i)))
            {
                foreach (var variant in CreateTileVariants(tile))
                {
                    if (TileFits(solution, y, x, variant))
                    {
                        var nextSolution = solution.With(y, x, variant);
                        var nextY = y;
                        var nextX = x + 1;
                        if (nextX == solution.Width)
                        {
                            nextX = 0;
                            nextY ++;
                            if (nextY == solution.Height)
                            {
                                return nextSolution;
                            }
                        }

                        var fullSolution = Solve(nextSolution, nextY, nextX, remainingTiles.RemoveAt(i));
                        if (fullSolution != null)
                        {
                            return fullSolution;
                        }
                    }
                }
            }

            return null;
        }

        private static IEnumerable<Tile> CreateTileVariants(Tile tile)
        {
            var variant = tile;
            for (var o = 0; o < 8; o ++)
            {
                if (o == 4)
                {
                    variant = tile.FlipHorizontal();
                }

                yield return variant;
                variant = variant.RotateCw();
            }
        }

        private static bool TileFits(IMatrix<Tile> solution, int y, int x, Tile tile)
        {
            if (y > 0 && !solution[y - 1, x].Data.Row(tile.Data.Height - 1).SequenceEqual(tile.Data.Row(0)))
            {
                return false;
            }

            if (x > 0 && !solution[y, x - 1].Data.Col(tile.Data.Width - 1).SequenceEqual(tile.Data.Col(0)))
            {
                return false;
            }

            return true;
        }
    }
}
