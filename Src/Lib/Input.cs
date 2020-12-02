using System;
using System.IO;

namespace Aoc2020.Lib
{
    internal static class Input
    {
        internal static string[] Read(int day)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var file = $"{dir}/../../../input/day{day}";
            return File.ReadAllLines(file);
        }
    }
}
