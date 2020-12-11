using Aoc2020.Lib;
using Xunit;

namespace Aoc2020
{
    public class Day11Test
    {
        [Fact]
        public void TestPart1()
        {
            var input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

            var lines = input.Split('\n');

            var result = Day11Solver.Part1(lines);
            
            Assert.Equal(37, result);
        }
    }
}
