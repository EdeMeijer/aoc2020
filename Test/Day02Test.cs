using Xunit;

namespace Aoc2020
{
    public class Day02Test
    {
        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", true)]
        public void TestPasswordAndPolicyLineValidPart1(string line, bool expected)
        {
            Assert.Equal(expected, Day02.isValidPart1(line));
        }
        
        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", false)]
        public void TestPasswordAndPolicyLineValidPart2(string line, bool expected)
        {
            Assert.Equal(expected, Day02.isValidPart2(line));
        }
    }
}
