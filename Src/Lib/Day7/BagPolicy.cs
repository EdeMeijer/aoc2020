using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day7
{
    public class BagPolicy
    {
        private readonly Dictionary<string, BagRule> _index = new Dictionary<string, BagRule>();
        private readonly Dictionary<string, List<BagRule>> _reverseIndex = new Dictionary<string, List<BagRule>>();

        public BagPolicy(IEnumerable<BagRule> rules)
        {
            foreach (var rule in rules)
            {
                _index[rule.Attributes] = rule;
                foreach (var attributes in rule.Contents.Keys)
                {
                    if (!_reverseIndex.ContainsKey(attributes))
                    {
                        _reverseIndex[attributes] = new List<BagRule>();
                    }

                    _reverseIndex[attributes].Add(rule);
                }
            }
        }

        public IEnumerable<BagRule> GetRulesContaining(string attributes)
        {
            var result = new HashSet<BagRule>();
            if (_reverseIndex.TryGetValue(attributes, out var rules))
            {
                result.UnionWith(rules);
                foreach (var rule in rules)
                {
                    result.UnionWith(GetRulesContaining(rule.Attributes));
                }
            }

            return result;
        }

        public long GetTotalBagsContainedBy(string attributes)
        {
            var result = 0L;
            if (_index.TryGetValue(attributes, out var rule))
            {
                result += rule.Contents.Sum(entry => entry.Value * (1 + GetTotalBagsContainedBy(entry.Key)));
            }

            return result;
        }
    }
}
