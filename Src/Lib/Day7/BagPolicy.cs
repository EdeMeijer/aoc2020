using System.Collections.Generic;

namespace Aoc2020.Lib.Day7
{
    public class BagPolicy
    {
        private readonly Dictionary<string, List<BagRule>> _reverseIndex = new Dictionary<string, List<BagRule>>();

        public BagPolicy(IEnumerable<BagRule> rules)
        {
            foreach (var rule in rules)
            {
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
    }
}
