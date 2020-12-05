using Aoc2020.Lib;
using Xunit;

namespace Aoc2020
{
    public class Day5Test
    {
        [Fact]
        public void TestCalcSeat()
        {
            Assert.Equal((70, 7), Day5Calculator.CalcSeat("BFFFBBFRRR"));
            Assert.Equal((14, 7), Day5Calculator.CalcSeat("FFFBBBFRRR"));
            Assert.Equal((102, 4), Day5Calculator.CalcSeat("BBFFBBFRLL"));
        }
    }
}
