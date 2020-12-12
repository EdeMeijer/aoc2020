using System;
using Aoc2020.Lib;

namespace Aoc2020
{
    internal static class Day12
    {
        internal static void Part1()
        {
            var input = Input.Lines(12);

            var angle = 0;
            var y = 0;
            var x = 0;

            foreach (var line in input)
            {
                var op = line[0];
                var value = int.Parse(line[1..]);
                switch (op)
                {
                    case 'N':
                        y -= value;
                        break;
                    case 'S':
                        y += value;
                        break;
                    case 'E':
                        x += value;
                        break;
                    case 'W':
                        x -= value;
                        break;
                    case 'L':
                        angle = (angle + value) % 360;
                        break;
                    case 'R':
                        angle = (angle + (360 - value)) % 360;
                        break;
                    case 'F':
                        if (angle == 0 || angle == 180)
                        {
                            x += value * (angle == 180 ? -1 : 1);
                        }
                        else
                        {
                            y += value * (angle == 90 ? -1 : 1);
                        }

                        break;
                    default:
                        throw new Exception("Unsupported");
                }

                if (angle < 0 || angle > 270 || angle % 90 != 0)
                {
                    throw new Exception("Invalid angle");
                }
            }
            
            Console.WriteLine(Math.Abs(x) + Math.Abs(y));
        }

        internal static void Part2()
        {
            var input = Input.Lines(12);
            
            var y = 0L;
            var x = 0L;
            var wx = 10;
            var wy = -1;

            void Turn(int deg)
            {
                var newWx = deg switch
                {
                    0 => wx,
                    90 => wy,
                    180 => -wx,
                    270 => -wy
                };
                wy = deg switch
                {
                    0 => wy,
                    90 => -wx,
                    180 => -wy,
                    270 => wx
                };
                wx = newWx;
            }

            foreach (var line in input)
            {
                var op = line[0];
                var value = int.Parse(line[1..]);
                switch (op)
                {
                    case 'N':
                        wy -= value;
                        break;
                    case 'S':
                        wy += value;
                        break;
                    case 'E':
                        wx += value;
                        break;
                    case 'W':
                        wx -= value;
                        break;
                    case 'L':
                        Turn(value);
                        break;
                    case 'R':
                        Turn(360 - value);
                        break;
                    case 'F':
                        x += value * wx;
                        y += value * wy;
                        break;
                    default:
                        throw new Exception("Unsupported");
                }
            }
            
            Console.WriteLine(Math.Abs(x) + Math.Abs(y));
        }
    }
}
