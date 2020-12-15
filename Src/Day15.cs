using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    public static class Day15
    {
        public static void Part1()
        {
            var input = "0,20,7,16,1,18,15";
            var startingNumbers = input.Split(',').Select(long.Parse).ToList();
            var speakIndices = new Dictionary<long, List<int>>();

            var prev = 0L;
            for (var i = 0; i < 2020; i ++)
            {
                long speak;
                if (i < startingNumbers.Count)
                {
                    speak = startingNumbers[i];
                }
                else
                {
                    var indices = speakIndices[prev];
                    if (indices.Count == 1 && indices[0] == i - 1)
                    {
                        speak = 0;
                    }
                    else
                    {
                        speak = indices[^1] - indices[^2];
                    }
                }

                if (!speakIndices.ContainsKey(speak))
                {
                    speakIndices[speak] = new List<int>();
                }
                speakIndices[speak].Add(i);
                prev = speak;
            }
            
            Console.WriteLine(prev);
        }
    }
}
