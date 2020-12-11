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

        [Fact]
        public void TestScan1()
        {
            var input = @".......#.
...#.....
.#.......
.........
..#L....#
....#....
.........
#........
...#.....";
            var state = Day11Solver.BuildState(input.Split('\n'));
            var result = Day11Solver.CountOccupiedVisibleSeats(state, 4, 3);
            Assert.Equal(8, result);
        }
        
        [Fact]
        public void TestScan2()
        {
            var input = @".............
.L.L.#.#.#.#.
.............";
            var state = Day11Solver.BuildState(input.Split('\n'));
            var result = Day11Solver.CountOccupiedVisibleSeats(state, 1, 1);
            Assert.Equal(1, result);
        }
        
        [Fact]
        public void TestScan3()
        {
            var input = @".##.##.
#.#.#.#
##...##
...L...
##...##
#.#.#.#
.##.##.";
            var state = Day11Solver.BuildState(input.Split('\n'));
            var result = Day11Solver.CountOccupiedVisibleSeats(state, 3, 3);
            Assert.Equal(0, result);
        }

        [Fact]
        public void TestPart2()
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

            var result = Day11Solver.Part2(lines);

            Assert.Equal(26, result);
        }
    }
}
