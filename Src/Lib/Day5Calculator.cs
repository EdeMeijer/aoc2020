using System.Linq;

namespace Aoc2020.Lib
{
    public static class Day5Calculator
    {
        public static int CalcSeatId(string pass)
        {
            var (row, column) = CalcSeat(pass);
            return CalcSeatId(row, column);
        }
        
        public static int CalcSeatId(int row, int column)
        {
            return row * 8 + column;
        }

        public static (int row, int column) CalcSeat(string pass)
        {
            return (
                BinaryStrToInt(pass.Substring(0, 7), 'B'),
                BinaryStrToInt(pass.Substring(7), 'R')
            );
        }

        private static int BinaryStrToInt(string str, char oneChar)
        {
            return str.Reverse().Select((c, i) => (c == oneChar ? 1 : 0) * (1 << i)).Sum();
        }
    }
}
