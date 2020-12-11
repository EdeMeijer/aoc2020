using System;
using System.Linq;

namespace Aoc2020.Lib
{
    public static class Day11Solver
    {
        public static int Part1(string[] input)
        {
            var height = input.Length;
            var width = input[0].Length;
            var seats = input.SelectMany(line => line.ToCharArray());
            var state = new Matrix<char>(height, width, seats);

            for (;;)
            {
                var nextState = state.Clone();
                for (var x = 0; x < width; x ++)
                {
                    for (var y = 0; y < height; y ++)
                    {
                        if (state[y, x] == '.') continue;
                        nextState[y, x] = CountOccupiedAdjacentSeats(state, y, x) switch
                        {
                            0 => '#',
                            var n when n >= 4 => 'L',
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
    }
}
