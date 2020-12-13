using System;
using System.Linq;

namespace Aoc2020
{
    public static class Day13
    {
        public static void Part1()
        {
            var start = 1000340;
            var busIds = "13,x,x,x,x,x,x,37,x,x,x,x,x,401,x,x,x,x,x,x,x,x,x,x,x,x,x,17,x,x,x,x,19,x,x,x,23,x,x,x,x,x,29,x,613,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41";
            
            var earliest = busIds.Split(',')
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
    }
}
