using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    public static class Day22
    {
        public static void Part1()
        {
            var input = Input.Text(22);

            var decks = input
                .Split("\n\n")
                .Select(part => part.Split('\n').Skip(1).Select(int.Parse).ToImmutableList())
                .Select((deck, i) => (deck, i))
                .ToDictionary(tup => tup.i, tup => tup.deck);

            while (decks.Count > 1)
            {
                decks = PlayRound(decks);
            }

            var winnerDeck = decks.Values.First()!;
            var score = winnerDeck.Reverse().Select((card, i) => card * (i + 1)).Sum();
            
            Console.WriteLine(score);
        }

        private static Dictionary<int, ImmutableList<int>> PlayRound(Dictionary<int, ImmutableList<int>> decks)
        {
            var topCards = decks
                .Select(kvp => (player: kvp.Key, card: kvp.Value.First()))
                .ToList();
            
            var winner = topCards
                .OrderByDescending(tup => tup.card)
                .Select(tup => tup.player)
                .First();

            var newDecks = decks.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.RemoveAt(0));
            
            newDecks[winner] = newDecks[winner].AddRange(topCards.Select(tup => tup.card).OrderByDescending(card => card));
            newDecks = newDecks.Where(kvp => kvp.Value.Any()).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return newDecks;
        }
    }
}
