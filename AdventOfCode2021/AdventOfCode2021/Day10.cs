using aoc_core;

namespace AdventOfCode2021
{
    internal class Day10 : AdventPuzzle
    {
        private Dictionary<char, char> brackets = new Dictionary<char, char>()
        {
            {'(', ')' },
            {'<', '>' },
            {'{', '}' },
            {'[', ']' }
        };

        private Dictionary<char, int> inCompleteScores = new Dictionary<char, int>()
        {
            {')', 3 },
            {'>', 57 },
            {'}', 1197 },
            {']', 25137 }
        };

        private Dictionary<char, int> completeScores = new Dictionary<char, int>()
        {
            {')', 1 },
            {'>', 4 },
            {'}', 3 },
            {']', 2 }
        };
        public override string SolveFirstPuzzle()
        {
            var input = Input.AsStringArray();
            int[] points = new int[input.Length];
            for(int inputLine = 0; inputLine < points.Length; inputLine++)
            {
                var line = input[inputLine];
                Stack<char> openBrackets = new();
                for(int i = 0; i < line.Length; i++)
                {
                    var bracket = line[i];
                    if(brackets.ContainsKey(bracket))
                        openBrackets.Push(bracket);
                    else
                    {
                        var open = openBrackets.Pop();
                        if (bracket != brackets[open]) { 
                            points[inputLine] = inCompleteScores[bracket];
                            break;
                        }

                    }
                }
            }

            return points.Sum().ToString();
        }

     
        public override string SolveSecondPuzzle()
        {
            var input = Input.AsStringArray();
            
            List<long> points = new();
            for (int inputLine = 0; inputLine < input.Length; inputLine++)
            {
                bool corrupted = false;
                var line = input[inputLine];
                Stack<char> openBrackets = new();
                for (int i = 0; i < line.Length; i++)
                {
                    var bracket = line[i];
                    if (brackets.ContainsKey(bracket))
                        openBrackets.Push(bracket);
                    else
                    {
                        var open = openBrackets.Pop();
                        if (bracket != brackets[open])
                        {
                            corrupted = true;
                            break;
                        }
                    }
                }

                if (corrupted)
                    continue;

                long score = 0;
                while (openBrackets.Count > 0)
                {
                    var br = openBrackets.Pop();
                    score *= 5;
                    score += completeScores[brackets[br]];
                }

                points.Add(score);
            }

            return points.OrderBy(x => x).Skip(points.Count / 2).First().ToString();
        }
    }
}