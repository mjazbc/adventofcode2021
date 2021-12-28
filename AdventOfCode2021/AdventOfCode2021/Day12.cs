using aoc_core;

namespace AdventOfCode2021
{
    internal class Day12 : AdventPuzzle
    {
        private readonly Dictionary<string, List<string>> nodes = new();
        public override void ParseInput()
        {
            var input = Input.AsStringArray().Select(x => x.Split('-'));
            foreach (var node in input)
            {
                if (!nodes.ContainsKey(node[0]))
                    nodes[node[0]] = new List<string>();
                if (!nodes.ContainsKey(node[1]))
                    nodes[node[1]] = new List<string>();

                if (node[1] != "start" && node[0] != "end")
                    nodes[node[0]].Add(node[1]);

                if (node[0] != "start" && node[1] != "end")
                    nodes[node[1]].Add(node[0]);
            }
        }
        public override string SolveFirstPuzzle() => CountPaths(PathContainsSmallCave);

        public override string SolveSecondPuzzle() => CountPaths(PathContainsOneSmallCaveTwice);

        private string CountPaths(Func<List<string>, string, bool> smallCaveCondition)
        {
            var pathsToVisit = new Stack<List<string>>();
            pathsToVisit.Push(new List<string> { "start" });
            HashSet<List<string>> visited = new();

            while (pathsToVisit.Count > 0)
            {
                var path = pathsToVisit.Pop();

                if (!visited.Contains(path))
                {
                    visited.Add(path);
                    foreach (var nextNode in nodes[path.Last()])
                    {
                        if (IsSmallCave(nextNode))
                        {
                            if (smallCaveCondition(path, nextNode))
                                continue;
                        }

                        var newPath = new List<string>(path)
                        {
                            nextNode
                        };

                        pathsToVisit.Push(newPath);
                    }
                }
            }

            return visited.Count(x => x.Contains("end")).ToString();
        }

        //Part one small cave condition
        private static bool PathContainsSmallCave(List<string> path, string nextNode) => path.Contains(nextNode);

        //Part two small cave condition
        private static bool PathContainsOneSmallCaveTwice(List<string> path, string nextNode)
        {
            return path.Where(IsSmallCave)
                .GroupBy(x => x)
                .Count(x => x.Count() == 2) == 1 && PathContainsSmallCave(path, nextNode);
        }

        private static bool IsSmallCave(string node) => node.ToLower() == node;
    }
}