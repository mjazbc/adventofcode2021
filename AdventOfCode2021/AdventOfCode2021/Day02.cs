using aoc_core;

namespace AdventOfCode2021
{
    internal class Day02 : AdventPuzzle
    {
        private List<(string, int)> directions = new();
        public override void ParseInput()
        {
            directions = Input.AsCustomTypeEnumerable(x => (x.Split()[0], int.Parse(x.Split()[1]))).ToList();
        }
        public override string SolveFirstPuzzle()
        {
            var (horizontal, depth) = (0, 0);
            foreach(var direction in directions)
            {
                switch (direction.Item1) { 
                    case "forward": horizontal += direction.Item2; break;
                    case "up": depth -= direction.Item2; break;
                    case "down": depth += direction.Item2; break;
                }
            }

            return (horizontal * depth).ToString();

        }

        public override string SolveSecondPuzzle()
        {
            var (horizontal, depth, aim) = (0, 0, 0);
            foreach (var direction in directions)
            {
                switch (direction.Item1)
                {
                    case "forward": horizontal += direction.Item2; depth += aim * direction.Item2; break;
                    case "up": aim -= direction.Item2; break;
                    case "down": aim += direction.Item2; break;
                }
            }

            return (horizontal * depth).ToString();
        }
    }
}