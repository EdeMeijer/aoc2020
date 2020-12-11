using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Aoc2020.Lib.Day08
{
    public class HandheldVm
    {
        private readonly ImmutableList<HandheldInstruction> _program;
        private int _pointer;
        private readonly Dictionary<Opcode, Action<int>> ops;

        public int Accumulator { get ; private set; }
        public bool Terminated => _pointer >= _program.Count;

        public HandheldVm(IEnumerable<HandheldInstruction> program)
        {
            _program = program.ToImmutableList();
            
            ops = new Dictionary<Opcode, Action<int>>
            {
                [Opcode.nop] = Nop,
                [Opcode.acc] = Acc,
                [Opcode.jmp] = Jmp
            };
        }

        public int Step()
        {
            if (Terminated)
            {
                throw new ApplicationException("Program terminated");
            }
            
            var pointer = _pointer;
            var instruction = _program[pointer];
            var op = ops[instruction.Opcode];
            op(instruction.Value);

            return pointer;
        }

        private void Nop(int value)
        {
            _pointer ++;
        }
        
        private void Acc(int value)
        {
            Accumulator += value;
            _pointer ++;
        }
        
        private void Jmp(int value)
        {
            _pointer += value;
        }
    }
}
