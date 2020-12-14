using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    public static class Day13
    {
        public static void Part1()
        {
            var start = 1000340;
            var input =
                "13,x,x,x,x,x,x,37,x,x,x,x,x,401,x,x,x,x,x,x,x,x,x,x,x,x,x,17,x,x,x,x,19,x,x,x,23,x,x,x,x,x,29,x,613,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41";

            var earliest = input.Split(',')
                .Where(id => id != "x")
                .Select(int.Parse)
                .Select(id =>
                {
                    var cycle = start / id + (start % id == 0 ? 0 : 1);
                    return (id, departure: cycle * id);
                })
                .OrderBy(tup => tup.departure)
                .First();

            var waitTime = earliest.departure - start;

            Console.WriteLine(earliest.id * waitTime);
        }

        public static void Part2()
        {
            var input =
                "13,x,x,x,x,x,x,37,x,x,x,x,x,401,x,x,x,x,x,x,x,x,x,x,x,x,x,17,x,x,x,x,19,x,x,x,23,x,x,x,x,x,29,x,613,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41";
            
            var entries = input.Split(',')
                .Select((id, i) => (id, i))
                .Where(tup => tup.id != "x")
                .Select(tup => (id: long.Parse(tup.id!), offset: tup.i))
                .ToList();

            var start = 0L;
            var factor = entries[0].id;
            foreach (var (id, offset) in entries.Skip(1))
            {
                for (var multiplier = 0;; multiplier ++)
                {
                    var newStart = start + factor * multiplier;
                    if ((newStart + offset) % id == 0)
                    {
                        start = newStart;
                        break;
                    }
                }

                factor *= id;
            }

            Console.WriteLine(start);
        }
    }
}
