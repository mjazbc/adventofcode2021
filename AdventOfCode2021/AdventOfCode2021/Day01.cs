using aoc_core;

namespace AdventOfCode2021
{
    internal class Day01 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var depths = Input.AsIntArray();

            return depths.Zip(depths[1..])
                .Count(x => x.Second > x.First)
                .ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var depths = Input.AsIntArray();

            var depthSums = depths.Zip(depths[1..], depths[2..])
                .Select(x => x.First + x.Second + x.Third)
                .ToArray();
            
            return depthSums.Zip(depthSums[1..])
                .Count(x => x.Second > x.First)
                .ToString();
        }
    }
}