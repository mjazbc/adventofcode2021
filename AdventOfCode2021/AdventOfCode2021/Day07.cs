using aoc_core;

namespace AdventOfCode2021
{
    internal class Day07 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var positions = Input.AsIntArray(",");
            var maxPosition = positions.Max();
            var minFuel = int.MaxValue;
  

            for(int i = 0; i < maxPosition; i++)
            {
                var fuel = positions.Sum(pos => Math.Abs(pos - i));
                if(fuel < minFuel) 
                { 
                    minFuel = fuel;

                }
            }

            return minFuel.ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var positions = Input.AsIntArray(",");
            var maxPosition = positions.Max();
            var minFuel = int.MaxValue;


            for (int i = 0; i < maxPosition; i++)
            {
                var fuel = positions.Sum(pos => CalculateFuel(Math.Abs(pos - i)));
                if (fuel < minFuel)
                {
                    minFuel = fuel;
                }
            }

            return minFuel.ToString();
        }

        private static int CalculateFuel(int n) => n * (1 + n) / 2;
    }
}