using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    public static class Day23
    {
        public static void Part1()
        {
            var input = "135468729";
            var cups = input.ToCharArray().Select(c => int.Parse(c.ToString())).ToList();
            var result = Play(cups, 100, cups.Count - 1);
            Console.WriteLine(string.Join("", result));
        }

        public static void Part2()
        {
            var input = "135468729";
            var cups = input.ToCharArray().Select(c => int.Parse(c.ToString())).ToList();

            var maxCup = cups.Max();

            cups.AddRange(Enumerable.Range(maxCup + 1, 1_000_000 - cups.Count));

            var result = Play(cups, 10_000_000, 2);
            Console.WriteLine((long)result[0] * result[1]);
        }

        private static List<int> Play(List<int> cups, int moves, int resultSize)
        {
            var cupMin = cups.Min();
            // Subtract min cup from all the cups so they are in base 0
            cups = cups.Select(c => c - cupMin).ToList();
            // The cup number is the index to an array, the value is the next cup

            var nextCups = new int[cups.Count];
            for (var i = 0; i < cups.Count; i ++)
            {
                var cup = cups[i];
                var next = cups[(i + 1) % cups.Count];
                nextCups[cup] = next;
            }

            var current = cups[0];

            void Move()
            {
                var takeFirst = nextCups[current];
                var takeSecond = nextCups[takeFirst];
                var takeLast = nextCups[takeSecond];
                var takenCups = new [] { takeFirst, takeSecond, takeLast };

                // remove by closing the loop
                nextCups[current] = nextCups[takeLast];

                var destination = current;
                for (;;)
                {
                    destination --;
                    if (destination == -1)
                    {
                        destination = cups.Count - 1;
                    }

                    if (!takenCups.Contains(destination))
                    {
                        break;
                    }
                }

                // insert taken cups
                nextCups[takeLast] = nextCups[destination];
                nextCups[destination] = takeFirst;

                current = nextCups[current];
            }

            for (var i = 0; i < moves; i ++)
            {
                Move();
            }

            var result = new List<int>();
            var cur = 0;
            for (var i = 0; i < resultSize; i ++)
            {
                cur = nextCups[cur];
                result.Add(cur + cupMin);
            }

            return result;
        }
    }
}
