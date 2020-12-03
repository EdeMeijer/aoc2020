using Aoc2020.Lib;
using Xunit;

namespace Aoc2020
{
    public class Day3Test
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
            
            var result = new Day3Resolver(lines).Resolve(3, 1);
            
            Assert.Equal(7, result);
        }
    }
}
