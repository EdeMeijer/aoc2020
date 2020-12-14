using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    internal static class Day14
    {
        internal static void Part1()
        {
            var input = Input.Lines(14);

            var mask = Enumerable.Repeat('X', 35).ToArray();
            var memory = new Dictionary<long, long>();

            foreach (var line in input)
            {
                var parts = line.Split(" = ");
                var cmd = parts[0];
                var arg = parts[1];
                
                if (cmd == "mask")
                {
                    mask = arg.ToCharArray();
                }
                else
                {
                    var addr = long.Parse(cmd[4..^1]);
                    var value = long.Parse(arg);

                    foreach (var (b, i) in mask.Reverse().Select((b, i) => (b, i)))
                    {
                        if (b == '1')
                        {
                            value |= 1L << i;
                        }

                        if (b == '0')
                        {
                            value &= ~(1L << i);
                        }
                    }

                    memory[addr] = value;
                }
            }

            var result = memory.Values.Sum();
            Console.WriteLine(result);
        }

        internal static void Part2()
        {
            var input = Input.Lines(14);
        }
    }
}
