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

            public bool Equals(Tile? other)
            {
                return other != null &&
                       other.Id == Id &&
                       other.Data.Equals(Data);
            }
        }

        public static void Part1()
        {
            var arrangedTiles = SolveTileArrangement();

            var checksum = arrangedTiles[0, 0].Id *
                           arrangedTiles[0, arrangedTiles.Width - 1].Id *
                           arrangedTiles[arrangedTiles.Height - 1, 0].Id *
                           arrangedTiles[arrangedTiles.Height - 1, arrangedTiles.Width - 1].Id;

            Console.WriteLine(checksum);
        }

        public static void Part2()
        {
            var arrangedTiles = SolveTileArrangement();
            var imageSize = arrangedTiles.Height * (arrangedTiles[0, 0].Data.Height - 2);

            var image = new Matrix<char>(imageSize, imageSize, GetTotalImagePixels(arrangedTiles));

            var seaMonster = new Matrix<char>(
                3,
                20,
                "                  # #    ##    ##    ### #  #  #  #  #  #   "
            );

            var totalPounds = image.Values.Count(v => v == '#');
            var monsterPounds = seaMonster.Values.Count(v => v == '#');

            foreach (var monsterVariant in GetTransformations(seaMonster))
            {
                var monsters = 0;
                for (var oy = 0; oy <= image.Height - monsterVariant.Height; oy ++)
                {
                    for (var ox = 0; ox <= image.Width - monsterVariant.Width; ox ++)
                    {
                        var slice = image.Slice(oy, monsterVariant.Height, ox, monsterVariant.Width);
                        if (IsSeaMonster(slice, monsterVariant))
                        {
                            monsters ++;
                        }
                    }
                }

                if (monsters > 0)
                {
                    Console.WriteLine(totalPounds - monsterPounds * monsters);
                    break;
                }
            }
        }

        private static bool IsSeaMonster(IMatrix<char> seaSlice, IMatrix<char> monster)
        {
            for (var y = 0; y < seaSlice.Height; y ++)
            {
                for (var x = 0; x < seaSlice.Width; x ++)
                {
                    if (monster[y, x] == '#' && seaSlice[y, x] != '#')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static IEnumerable<char> GetTotalImagePixels(IMatrix<Tile> arrangedTiles)
        {
            for (var ty = 0; ty < arrangedTiles.Height; ty ++)
            {
                for (var y = 1; y < 9; y ++)
                {
                    for (var tx = 0; tx < arrangedTiles.Width; tx ++)
                    {
                        var tile = arrangedTiles[ty, tx].Data;
                        for (var x = 1; x < 9; x ++)
                        {
                            yield return tile[y, x];
                        }
                    }
                }
            }
        }

        private static IMatrix<Tile> SolveTileArrangement()
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

            return solution;
        }

        private static IMatrix<Tile>? Solve(IMatrix<Tile> solution, int y, int x, ImmutableList<Tile> remainingTiles)
        {
            foreach (var (tile, i) in remainingTiles.Select((t, i) => (t, i)))
            {
                foreach (var variant in GetTransformations(tile.Data))
                {
                    var variantTile = new Tile(tile.Id, variant);
                    if (TileFits(solution, y, x, variantTile))
                    {
                        var nextSolution = solution.With(y, x, variantTile);
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

        private static IEnumerable<IMatrix<char>> GetTransformations(IMatrix<char> tile)
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
