using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day23
    {
        public static void Part1()
        {
            var input = "135468729";

            var cups = input.ToCharArray().Select(c => int.Parse(c.ToString())).ToList();
            var cupMin = cups.Min();
            var cupMax = cups.Max();

            var current = cups[0];

            void Move()
            {
                var curIdx = cups.Select((c, i) => (c, i)).First(tup => tup.c == current).i;

                var taken = new List<int>();
                for (var i = 0; i < 3; i ++)
                {
                    taken.Add(cups[(curIdx + 1 + i) % cups.Count]);
                }

                foreach (var cup in taken)
                {
                    cups.Remove(cup);
                }

                var destination = current - 1;
                while (!cups.Contains(destination))
                {
                    destination --;
                    if (destination <= cupMin - 1)
                    {
                        destination = cupMax;
                    }
                }
                
                var destIdx = cups.Select((c, i) => (c, i)).First(tup => tup.c == destination).i;
                cups.InsertRange(destIdx + 1, taken);
                
                curIdx = cups.Select((c, i) => (c, i)).First(tup => tup.c == current).i;
                current = cups[(curIdx + 1) % cups.Count];
            }

            for (var i = 0; i < 100; i ++)
            {
                Move();
            }
            
            var oneIdx = cups.Select((c, i) => (c, i)).First(tup => tup.c == 1).i;
            var result = new List<int>();
            for (var offset = 1; offset < cups.Count; offset ++)
            {
                result.Add(cups[(oneIdx + offset) % cups.Count]);
            }
            
            Console.WriteLine(string.Join("", result));
        }
    }
}
