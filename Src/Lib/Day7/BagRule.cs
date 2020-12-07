using System.Collections.Generic;
using System.Collections.Immutable;

namespace Aoc2020.Lib.Day7
{
    public sealed class BagRule
    {
        public string Attributes { get; }
        public ImmutableDictionary<string, int> Contents { get; }

        public BagRule(string attributes, IDictionary<string, int> Contents)
        {
            Attributes = attributes;
            this.Contents = Contents.ToImmutableDictionary();
        }

        private bool Equals(BagRule other)
        {
            return Attributes == other.Attributes;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is BagRule other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Attributes != null ? Attributes.GetHashCode() : 0);
        }
    }
}
