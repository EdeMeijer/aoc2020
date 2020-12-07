using System.Collections.Generic;
using System.Linq;
using Aoc2020.Lib.Day7;
using Xunit;

namespace Aoc2020
{
    public class Day7Test
    {
        [Fact]
        public void TestParser()
        {
            var input = "light red bags contain 1 bright white bag, 2 muted yellow bags.";

            var parsed = BagRuleParser.Parse(input);

            Assert.Equal("light red", parsed.Attributes);
            var expectedContents = new Dictionary<string, int>
            {
                ["bright white"] = 1,
                ["muted yellow"] = 2
            };

            Assert.Equal(expectedContents, parsed.Contents);
        }

        [Fact]
        public void TestPart1()
        {
            var input = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

            var lines = input.Split('\n');
            
            var policy = new BagPolicy(lines.Select(BagRuleParser.Parse));

            var result = policy.GetRulesContaining("shiny gold").Count();
            
            Assert.Equal(4, result);
        }
    }
}