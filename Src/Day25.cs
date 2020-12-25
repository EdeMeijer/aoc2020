using System;

namespace Aoc2020
{
    public static class Day25
    {
        public static void Part1()
        {
            var cardPubKey = 6929599;
            var doorPubKey = 2448427;

            Console.WriteLine(GetEncryptionKey(cardPubKey, FindLoopSize(doorPubKey)));
        }

        private static int FindLoopSize(long pubKey)
        {
            var value = 1L;
            var i = 0;
            for (;;)
            {
                value = Transform(value, 7);
                i ++;
                if (value == pubKey)
                {
                    return i;
                }
            }
        }

        private static long GetEncryptionKey(long pubKey, int loopSize)
        {
            var value = 1L;
            for (var i = 0; i < loopSize; i ++)
            {
                value = Transform(value, pubKey);
            }

            return value;
        }

        private static long Transform(long value, long subject) => value * subject % 20201227;
    }
}
