using aoc_core;

namespace AdventOfCode2021
{
    internal class Day11 : AdventPuzzle
    {
        private readonly (int x, int y)[] adjecants = 
            new []{ (1, 0), (1, 1), (1, -1), (-1, 0), (-1, 1), (-1, -1), (0, 1), (0, -1) };
        public override string SolveFirstPuzzle()
        {
            var input = Input.AsStringArray();
            var energyLevels = new Dictionary<int, List<(int y, int x)>>();
            for(int i = 0; i<= 10; i++)
                energyLevels[i] = new List<(int y, int x)>();
            //Parse input to dictionary
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    int level = (int)char.GetNumericValue(input[y][x]);
                    energyLevels[level].Add((y, x));
                }
            }

            int flashes = 0;
            for (int step = 0; step < 100; step++)
            {
                var nines = energyLevels[9];
                for (int level = 9; level > 0; level--)
                {
                    energyLevels[level] = new List<(int y, int x)>(energyLevels[level - 1]);
                }
                energyLevels[0].Clear();
                energyLevels[10].AddRange(nines);


                while (energyLevels[10].Count > 0)
                {
                    foreach (var point in energyLevels[10].ToList())
                    {
                        flashes++;
                        energyLevels[10].Remove(point);
                        energyLevels[0].Add(point);

                        foreach (var adj in Adjecant(point.x, point.y, input[0].Length, input.Length))
                        {
                            var level = energyLevels.Single(x => x.Value.Contains(adj)).Key;
                            if (level < 10 && level > 0)
                            {
                                energyLevels[level].Remove(adj);
                                energyLevels[level + 1].Add(adj);
                            }
                        }
                    }
                }



                for (int y = 0; y < input.Length; y++)
                {
                    for (int x = 0; x < input[y].Length; x++)
                        Console.Write(energyLevels.Single(a => a.Value.Contains((y, x))).Key);

                    Console.WriteLine();
                }

                Console.WriteLine();
            }

            return flashes.ToString();
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

            var input = Input.AsStringArray();
            var energyLevels = new Dictionary<int, List<(int y, int x)>>();
            for (int i = 0; i <= 10; i++)
                energyLevels[i] = new List<(int y, int x)>();
            //Parse input to dictionary
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    int level = (int)char.GetNumericValue(input[y][x]);
                    energyLevels[level].Add((y, x));
                }
            }

            int step = 0;
            var total = input.Length * input[0].Length;
            while(energyLevels[0].Count < total)
            {
                var nines = energyLevels[9];
                for (int level = 9; level > 0; level--)
                {
                    energyLevels[level] = new List<(int y, int x)>(energyLevels[level - 1]);
                }
                energyLevels[0].Clear();
                energyLevels[10].AddRange(nines);


                while (energyLevels[10].Count > 0)
                {
                    foreach (var point in energyLevels[10].ToList())
                    {
                        energyLevels[10].Remove(point);
                        energyLevels[0].Add(point);

                        foreach (var adj in Adjecant(point.x, point.y, input[0].Length, input.Length))
                        {
                            var level = energyLevels.Single(x => x.Value.Contains(adj)).Key;
                            if (level < 10 && level > 0)
                            {
                                energyLevels[level].Remove(adj);
                                energyLevels[level + 1].Add(adj);
                            }
                        }
                    }
                }
                step++;
            }

            return step.ToString();
        }
    }
}