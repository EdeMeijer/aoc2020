using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day21
    {
        private static readonly Regex _inputPattern = new Regex(@"^(?:([a-z]+) )+\(contains (?:([a-z]+)[^a-z]+)+$");

        public static void Part1And2()
        {
            var input = Input.Lines(21);

            var examples = input
                .Select(line => _inputPattern.Match(line))
                .Select(match => (
                    match.Groups[1].Captures.Select(c => c.Value).ToHashSet(),
                    match.Groups[2].Captures.Select(c => c.Value).ToHashSet()
                ))
                .ToList();

            var ingredients = examples.SelectMany(ex => ex.Item1).ToHashSet();
            var allergens = examples.SelectMany(ex => ex.Item2).ToHashSet();

            // Make a list of possible ingredients per allergen
            var options = allergens.ToDictionary(allergen => allergen, allergen =>
            {
                var matchingExamples = examples
                    .Where(example => example.Item2.Contains(allergen))
                    .Select(example => example.Item1).ToList();

                return matchingExamples
                    .SelectMany(example => example)
                    .GroupBy(ingredient => ingredient)
                    .Where(group => group.Count() == matchingExamples.Count)
                    .Select(group => group.Key)
                    .ToList();
            });

            bool IsValidPartialSolution(ImmutableDictionary<string, string> solution)
            {
                foreach (var example in examples)
                {
                    foreach (var allergen in example.Item2)
                    {
                        if (solution.TryGetValue(allergen, out var ingredient))
                        {
                            if (!example.Item1.Contains(ingredient))
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }

            ImmutableDictionary<string, string>? Solve(
                ImmutableHashSet<string> remainingIngredients,
                ImmutableList<string> remainingAllergens,
                ImmutableDictionary<string, string> solutionSoFar
            )
            {
                var allergen = remainingAllergens[0];
                var nextRemainingAllergens = remainingAllergens.RemoveAt(0);
                foreach (var ingredient in options[allergen].Where(remainingIngredients.Contains))
                {
                    var candidate = solutionSoFar.SetItem(allergen, ingredient);
                    if (!IsValidPartialSolution(candidate))
                    {
                        continue;
                    }

                    if (nextRemainingAllergens.Count == 0)
                    {
                        return candidate;
                    }

                    var solution = Solve(
                        remainingIngredients.Remove(ingredient),
                        nextRemainingAllergens,
                        candidate
                    );
                    if (solution != null)
                    {
                        return solution;
                    }
                }

                return null;
            }

            var mapping = Solve(
                ingredients.ToImmutableHashSet(),
                allergens.ToImmutableList(),
                ImmutableDictionary<string, string>.Empty
            )!;

            var part1Solution = examples.Sum(example => example.Item1.Except(mapping.Values).Count());

            Console.WriteLine(part1Solution);

            var part2Solution = string.Join(',', mapping.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value));
            
            Console.WriteLine(part2Solution);
        }
    }
}
