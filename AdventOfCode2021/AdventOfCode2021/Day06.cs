using aoc_core;

namespace AdventOfCode2021
{
    internal class Day06 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var lanternFish = Input.AsIntArray(",").ToList();
            int days = 80;
            for(int day = 1; day <= days; day++)
            {
                int listLength = lanternFish.Count;
                for(int i = 0; i < listLength; i++)
                {
                    var current = lanternFish[i]; 
                    if(current == 0)
                    {
                        lanternFish[i] = 6;
                        lanternFish.Add(8);
                    }
                    else
                    {
                        lanternFish[i] = current - 1;
                    }
                }
            }
            return lanternFish.Count.ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var lanternFish = Input.AsIntArray(",").ToList();
            int days = 256;

            Dictionary<int, long> ages = lanternFish
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => (long) x.Count());

            for(int i = 0; i < 9; i++)
            {
                if (!ages.ContainsKey(i))
                    ages[i] = 0;
            }


            for (int i = 0; i < days; i++)
            {
                long spawning = ages[0];

                for (int fishAge = 0; fishAge < 8; fishAge++)
                {
                    ages[fishAge] = ages[fishAge + 1];
                }

                ages[8] = spawning;
                ages[6] += spawning;    
            }


            return ages.Values.Sum().ToString();
        }
    }
}