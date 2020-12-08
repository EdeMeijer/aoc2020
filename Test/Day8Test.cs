using Aoc2020.Lib.Day8;
using Xunit;

namespace Aoc2020
{
    public class Day8Test
    {
        [Fact]
        public void TestPart1()
        {
            var input = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

            var lines = input.Split('\n');
            var result = Day8Solver.Part1(lines);
            
            Assert.Equal(5, result);
        }
        
        [Fact]
        public void TestPart2()
        {
            var input = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

            var lines = input.Split('\n');
            var result = Day8Solver.Part2(lines);
            
            Assert.Equal(8, result);
        }
    }
}
