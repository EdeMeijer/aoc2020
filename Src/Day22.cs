using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Aoc2020.Lib;

namespace Aoc2020
{
    using GameState = Dictionary<int, ImmutableList<int>>;

    public static class Day22
    {
        public static void Part1()
        {
            Console.WriteLine(PlayGame(GetInitialState(), PlayRoundNormal).score);
        }

        public static void Part2()
        {
            Console.WriteLine(PlayGame(GetInitialState(), PlayRoundRecursive).score);
        }

        private static GameState GetInitialState()
        {
            return Input.Text(22)
                .Split("\n\n")
                .Select(part => part.Split('\n').Skip(1).Select(int.Parse).ToImmutableList())
                .Select((deck, i) => (deck, i))
                .ToDictionary(tup => tup.i, tup => tup.deck);
        }

        private static (int winner, int score) PlayGame(GameState state, Func<GameState, GameState> playRound)
        {
            var previousStates = new HashSet<string>();
            while (state.Count > 1)
            {
                state = playRound(state);
                if (!previousStates.Add(StateToString(state)))
                {
                    return (0, 0);
                }
            }

            var winnerDeck = state.Values.First()!;
            var score = winnerDeck.Reverse().Select((card, i) => card * (i + 1)).Sum();
            return (state.Keys.First(), score);
        }

        private static GameState PlayRoundNormal(GameState state)
        {
            var topCards = state
                .Select(kvp => (player: kvp.Key, card: kvp.Value.First()))
                .ToList();

            var newState = state.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.RemoveAt(0));

            var winner = topCards
                .OrderByDescending(tup => tup.card)
                .Select(tup => tup.player)
                .First();


            newState[winner] = newState[winner]
                .AddRange(topCards.Select(tup => tup.card).OrderByDescending(card => card));

            newState = newState.Where(kvp => kvp.Value.Any()).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return newState;
        }

        private static GameState PlayRoundRecursive(GameState state)
        {
            var topCards = state
                .Select(kvp => (player: kvp.Key, card: kvp.Value.First()))
                .ToList();

            var newState = state.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.RemoveAt(0));

            var recurseState = state
                .Where(kvp => kvp.Value.Count - 1 >= kvp.Value.First())
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Skip(1).Take(kvp.Value.First()).ToImmutableList());

            int winner;
            if (recurseState.Count == state.Count)
            {
                (winner, _) = PlayGame(recurseState, PlayRoundRecursive);
            }
            else
            {
                winner = topCards
                    .OrderByDescending(tup => tup.card)
                    .Select(tup => tup.player)
                    .First();
            }

            newState[winner] = newState[winner]
                .AddRange(topCards.OrderBy(tup => tup.player == winner ? 0 : 1).Select(tup => tup.card));

            newState = newState.Where(kvp => kvp.Value.Any()).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return newState;
        }

        private static string StateToString(GameState state)
        {
            return string.Join(
                '\n',
                state
                    .OrderBy(kvp => kvp.Key)
                    .Select(kvp => $"{kvp.Key}:{string.Join(',', kvp.Value)}")
            );
        }
    }
}
