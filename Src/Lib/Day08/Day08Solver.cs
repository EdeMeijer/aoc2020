using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Aoc2020.Lib.Day08
{
    public static class Day08Solver
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

        public static int Part2(string[] input)
        {
            var program = input.Select(HandheldInstruction.Parse).ToImmutableList();

            for (var line = 0; line < program.Count; line ++)
            {
                var instruction = program[line];
                if (instruction.Opcode == Opcode.acc)
                {
                    continue;
                }

                var variantInstr = instruction.WithOpcode(instruction.Opcode == Opcode.jmp ? Opcode.nop : Opcode.jmp);
                var variantProgram = program.SetItem(line, variantInstr);

                // Run the program until it terminates (fail on loop)
                var vm = new HandheldVm(variantProgram);
                var executed = new HashSet<int>();
                var didLoop = false;

                while (!vm.Terminated)
                {
                    if (!executed.Add(vm.Step()))
                    {
                        didLoop = true;
                        break;
                    }
                }

                if (!didLoop)
                {
                    return vm.Accumulator;
                }
            }

            throw new ApplicationException("No solution found");
        }
    }
}
