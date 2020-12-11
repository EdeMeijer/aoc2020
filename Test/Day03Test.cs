using Aoc2020.Lib;
using Xunit;

namespace Aoc2020
{
    public class Day03Test
    {
        [Fact]
        public void TestResolver()
        {
            var input = @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";
            var lines = input.Split('\n');
            
            var result = new Day03Solver(lines).GetNumTrees(3, 1);
            
            Assert.Equal(7, result);
        }
    }
}
