using aoc_core;

namespace AdventOfCode2021
{
    internal class Day09 : AdventPuzzle
    {
        private readonly (int x, int y)[] adjecants = new []{ (1, 0), (-1, 0), (0, 1), (0, -1) };
        public override string SolveFirstPuzzle()
        {
            var map = Input.AsNumericMatrix();
            var lowPoints = FindLowPoints(map); 

            return lowPoints.Select(x => map[x.y, x.x] + 1).Sum().ToString();
        }

        public IEnumerable<(int y, int x)> Adjecant(int x, int y, int xLen, int yLen)
        {
            foreach(var adj in adjecants)
            {
                int newX = x + adj.x;
                int newY = y + adj.y;

                if(newX >= 0 && newY >= 0 && newX < xLen && newY < yLen)
                    yield return (newY, newX);
            }
        }

        public override string SolveSecondPuzzle()
        {
            var map = Input.AsNumericMatrix();
            var lowPoints = FindLowPoints(map);

            Stack<(int y, int x)> pointsToCheck = new Stack<(int y, int x)>();
            HashSet<(int y, int x)> pointsChecked = new HashSet<(int y, int x)>();
            int[] basins = new int[lowPoints.Count];
            int basinNum = 0;
            foreach (var lowPoint in lowPoints)
            {
                pointsToCheck.Push(lowPoint);
                basins[basinNum]++;

                while (pointsToCheck.Count > 0)
                {
                    var currPoint = pointsToCheck.Pop();
                    pointsChecked.Add(currPoint);
                    foreach (var adj in Adjecant(currPoint.x, currPoint.y, map.GetLength(1), map.GetLength(0)))
                    {
                        if (!pointsChecked.Contains(adj) && map[adj.y, adj.x] < 9)
                        {
                            basins[basinNum]++;
                            pointsToCheck.Push(adj);
                        }

                        pointsChecked.Add(adj);
                    }
                }

                basinNum++;
            }

            var ordered = basins.OrderByDescending(x => x).Take(3).ToArray();

            return (ordered[0] * ordered[1] * ordered[2]).ToString();

        }

        private HashSet<(int y, int x)> FindLowPoints(double[,] map)
        {
            HashSet<(int y, int x)> lowPoints = new HashSet<(int y, int x)>();  
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    var currPoint = (int)map[y, x];
                    bool isLowest = Adjecant(x, y, map.GetLength(1), map.GetLength(0))
                        .All(adj => map[adj.y, adj.x] > currPoint);
                    if (isLowest)
                        lowPoints.Add((y, x));
                }
            }

            return lowPoints;
        }
    }
}