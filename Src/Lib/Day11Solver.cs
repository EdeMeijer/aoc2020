using System;
using System.Linq;

namespace Aoc2020.Lib
{
    public static class Day11Solver
    {
        public static int Part1(string[] input) => Solve(input, CountOccupiedAdjacentSeats, 4);

        public static int Part2(string[] input) => Solve(input, CountOccupiedVisibleSeats, 5);

        public static Matrix<char> BuildState(string[] input)
        {
            var height = input.Length;
            var width = input[0].Length;
            var seats = input.SelectMany(line => line.ToCharArray());
            return new Matrix<char>(height, width, seats);
        }

        private static int Solve(string[] input, Func<Matrix<char>, int, int, int> scanFn, int tolerance)
        {
            var state = BuildState(input);

            for (;;)
            {
                var nextState = state.Clone();
                for (var x = 0; x < state.Width; x ++)
                {
                    for (var y = 0; y < state.Height; y ++)
                    {
                        if (state[y, x] == '.') continue;
                        nextState[y, x] = scanFn(state, y, x) switch
                        {
                            0 => '#',
                            var n when n >= tolerance => 'L',
                            _ => state[y, x]
                        };
                    }
                }

                if (nextState.Equals(state))
                {
                    break;
                }

                state = nextState;
            }

            return state.Values.Count(c => c == '#');
        }

        private static int CountOccupiedAdjacentSeats(Matrix<char> state, int y, int x)
        {
            var result = 0;
            for (var dx = -1; dx <= 1; dx ++)
            {
                for (var dy = -1; dy <= 1; dy ++)
                {
                    if (dx == 0 && dy == 0) continue;
                    try
                    {
                        if (state[y + dy, x + dx] == '#')
                        {
                            result ++;
                        }
                    }
                    catch (ArgumentException)
                    {
                        // Ignore out of bounds
                    }
                }
            }

            return result;
        }

        public static int CountOccupiedVisibleSeats(Matrix<char> state, int y, int x)
        {
            var result = 0;
            for (var dx = -1; dx <= 1; dx ++)
            {
                for (var dy = -1; dy <= 1; dy ++)
                {
                    if (dx == 0 && dy == 0) continue;
                    try
                    {
                        var yScan = y;
                        var xScan = x;
                        for (;;)
                        {
                            yScan += dy;
                            xScan += dx;
                            var cell = state[yScan, xScan];
                            if (cell == '#') result ++;
                            if (cell != '.') break;
                        }
                    }
                    catch (ArgumentException)
                    {
                        // Ignore out of bounds
                    }
                }
            }

            return result;
        }
    }
}
