using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day8
{
    public static class Day8Solver
    {
        public static int Part1(string[] input)
        {
            var vm = new HandheldVm(input.Select(HandheldInstruction.Parse));
            var executed = new HashSet<int>();

            for (;;)
            {
                var accumBefore = vm.Accumulator;
                if (!executed.Add(vm.Step()))
                {
                    return accumBefore;
                }
            }
        }
    }
}
