using aoc_core;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    internal class Day15 : AdventPuzzle
    {
        Dictionary<(int x, int y), int> inputNodes = new();
        private readonly (int x, int y)[] adjecants = new[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

        public override void ParseInput()
        {
            var inputMap = Input.AsNumericMatrix();
            for (int y = 0; y < inputMap.GetLength(0); y++)
            {
                for (int x = 0; x < inputMap.GetLength(1); x++)
                {
                    inputNodes[(x, y)] = (int)inputMap[x, y];
                }
            }
        }

        public override string SolveFirstPuzzle()
        {
            return RunDijkstra(inputNodes);
        }

        private string RunDijkstra(Dictionary<(int x, int y), int>  nodes)
        {
            var dist = new Dictionary<(int x, int y), int>
            {
                [(0, 0)] = 0
            };
            var prev = new Dictionary<(int x, int y), (int x, int y)?>();
            var q = new PriorityQueue<(int x, int y), int>();

            int xsize = nodes.Max(x => x.Key.x) + 1;
            int ysize = nodes.Max(x => x.Key.y) + 1;

            foreach (var node in nodes.Keys)
            {
                if (node != (0, 0))
                {
                    dist[node] = int.MaxValue;
                    prev[node] = null;
                }
                q.Enqueue(node, dist[node]);
            }

            while (q.Count > 0)
            {
                q.TryDequeue(out var u, out int p);
                if (p != dist[u])
                    continue;

                if (u == (xsize - 1, ysize - 1))
                    return dist[u].ToString();

                foreach (var v in Adjecant(u.x, u.y, xsize, ysize))
                {

                    var alt = dist[u] + nodes[v];
                    if (alt < dist[v])
                    {
                        dist[v] = alt;
                        prev[v] = u;
                        q.Enqueue(v, alt);

                    }
                }
            }
            throw new Exception("Path not found");
        }

        public IEnumerable<(int x, int y)> Adjecant(int x, int y, int xLen, int yLen)
        {
            foreach (var adj in adjecants)
            {
                int newX = x + adj.x;
                int newY = y + adj.y;

                if (newX >= 0 && newY >= 0 && newX < xLen && newY < yLen)
                    yield return (newX, newY);
            }
        }

        public override string SolveSecondPuzzle()
        {
            var largeNodes = new Dictionary<(int x, int y), int>();

            int xsize = inputNodes.Max(x => x.Key.x) + 1;
            int ysize = inputNodes.Max(x => x.Key.y) + 1;

            foreach (var node in inputNodes.Keys.ToList())
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        var value = (inputNodes[node] + i + j) % 9;
                        if (value == 0)
                            value = 9;

                        largeNodes[(node.x + i * xsize, node.y + j * ysize)] = value;
                    }  
                }
            }

            return RunDijkstra(largeNodes);
 
        }
    }
}