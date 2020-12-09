using Aoc2020.Lib.Day9;
using Xunit;

namespace Aoc2020
{
    public class Day9Test
    {
        [Fact]
        public void TestPart1()
        {
            var input = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";
            var lines = input.Split('\n');

            var result = Day9Solver.Part1(lines, 5);
            
            Assert.Equal(127, result);
        }
    }
}
