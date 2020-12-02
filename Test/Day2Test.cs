using Xunit;

namespace Aoc2020
{
    public class Day2Test
    {
        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", true)]
        public void TestPasswordAndPolicyLineValid(string line, bool expected)
        {
            Assert.Equal(expected, Day2.isValid(line));
        }
    }
}
