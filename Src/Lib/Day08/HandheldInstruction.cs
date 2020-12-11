using System;

namespace Aoc2020.Lib.Day08
{
    public class HandheldInstruction
    {
        public Opcode Opcode { get; }
        public int Value { get; }

        public HandheldInstruction(Opcode opcode, int value)
        {
            Opcode = opcode;
            Value = value;
        }

        public static HandheldInstruction Parse(string input)
        {
            var parts = input.Split(' ');
            if (!Enum.TryParse<Opcode>(parts[0], out var opcode))
            {
                throw new ArgumentException();
            }
            return new HandheldInstruction(opcode, int.Parse(parts[1]));
        }

        public HandheldInstruction WithOpcode(Opcode opcode)
        {
            return new HandheldInstruction(opcode, Value);
        }
    }

    public enum Opcode
    {
        nop,
        acc,
        jmp
    }
}
