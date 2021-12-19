using aoc_core;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    internal class Day17  : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var m = Regex.Matches(Input.AsString(), @"(-?\d*(\.{2})-?\d*)");
            var xrange = m[0].Value.Split("..").Select(int.Parse).ToArray();
            var yrange = m[1].Value.Split("..").Select(int.Parse).ToArray();

            int maxy = 0;
            for (int x = 1; x <= xrange[1]; x++)
            {
                for (int y = 1; y <= 1000; y++)
                {
                    var velocity = (x, y);
                    var path = LaunchProbe(xrange, yrange, velocity);
                    if (path == null)
                        continue;

                    var max = path.Max(point => point.y);
                    if(max > maxy)
                        maxy = max; 

                }
            }

            return maxy.ToString();
        }

        private static HashSet<(int x, int y)>? LaunchProbe(int[] xrange, int[] yrange, (int x, int y) velocity)
        {
            var point = (x: 0, y: 0);
            HashSet<(int x, int y)> result = new();

            while (point.x <= xrange[1] && point.y >= yrange[0])
            {
                point = (point.x + velocity.x, point.y + velocity.y);

                result.Add(point);
                if (point.x >= xrange[0] && point.x <= xrange[1] && point.y >= yrange[0] && point.y <= yrange[1])
                    return result ;

                if (velocity.x != 0)
                    velocity.x = velocity.x > 0 ? velocity.x - 1 : velocity.x + 1;

                velocity.y -= 1;
            }

            return null;

        }

        public override string SolveSecondPuzzle()
        {
            var m = Regex.Matches(Input.AsString(), @"(-?\d*(\.{2})-?\d*)");
            var xrange = m[0].Value.Split("..").Select(int.Parse).ToArray();
            var yrange = m[1].Value.Split("..").Select(int.Parse).ToArray();

            HashSet<(int x,int y)> result = new();
            
            //Choose large enough numbers so all successful trajectories shoud be included
            for (int x = 1; x <= xrange[1] * 2; x++)
            {
                for (int y = yrange[0] * 2; y <= 1000; y++)
                {
                    var velocity = (x, y);
                    var path = LaunchProbe(xrange, yrange, velocity);
                    if (path == null)
                        continue;

                    result.Add(velocity);
 
                }
            }

            return result.Count().ToString();
        }
    }
}