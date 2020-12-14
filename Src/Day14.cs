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

            var mask = new (char b, int i)[0];
            var memory = new Dictionary<long, long>();

            foreach (var line in input)
            {
                var parts = line.Split(" = ");
                var cmd = parts[0];
                var arg = parts[1];

                if (cmd == "mask")
                {
                    mask = arg.ToCharArray().Reverse().Select((b, i) => (b, i)).ToArray();
                }
                else
                {
                    var addr = long.Parse(cmd[4..^1]);
                    var value = long.Parse(arg);

                    addr = mask.Where(e => e.b == '1').Aggregate(addr, (current, e) => current | 1L << e.i);
                    var floats = mask.Where(e => e.b == 'X').Select(e => e.i).ToArray();

                    void ApplyFloatingBit(long addr, int[] bits)
                    {
                        if (bits.Length == 0)
                        {
                            memory[addr] = value;
                            return;
                        }

                        var i = bits[0];
                        var addr0 = addr | 1L << i;
                        var addr1 = addr & ~(1L << i);
                        ApplyFloatingBit(addr0, bits[1..]);
                        ApplyFloatingBit(addr1, bits[1..]);
                    }

                    ApplyFloatingBit(addr, floats);
                }
            }
            
            var result = memory.Values.Sum();
            Console.WriteLine(result);
        }
    }
}
