namespace Aoc2020.Lib
{
    public class Day03Solver
    {
        private readonly string[] _map;
        private readonly int _height;
        private readonly int _width;
        
        public Day03Solver(string[] map)
        {
            _map = map;
            _height = map.Length;
            _width = map[0].Length;
        }

        public int GetNumTrees(int right, int down)
        {
            var y = 0;
            var x = 0;
            var trees = 0;

            while (y < _height)
            {
                if (_map[y][x] == '#')
                {
                    trees ++;
                }
                
                y += down;
                x = (x + right) % _width;
            }

            return trees;
        }
    }
}
