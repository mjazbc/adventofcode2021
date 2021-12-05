using aoc_core;
using System.Drawing;

namespace AdventOfCode2021
{
    internal class Day05 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var lines = Input.AsCustomTypeEnumerable(l => new Line(l))
                .Where(l => l.start.X == l.end.X || l.start.Y == l.end.Y);

            return CountIntersections(lines);
        }

        public override string SolveSecondPuzzle()
        {
            var lines = Input.AsCustomTypeEnumerable(l => new Line(l));
            return CountIntersections(lines);
        }

        private static string CountIntersections(IEnumerable<Line> lines)
        {
            Dictionary<Point, int> points = new();

            foreach (var line in lines)
            {
                foreach (var point in line.GeneratePoints())
                {
                    if (!points.ContainsKey(point))
                        points.Add(point, 0);

                    points[point]++;
                }
            }

            return points.Values.Where(v => v > 1).Count().ToString();
        }
    }

    class Line
    {
        public Point start;
        public Point end;
        public Line(string lineString)
        {
            var points = lineString.Split(" -> ");

            start = new Point(int.Parse(points[0].Split(',')[0]), int.Parse(points[0].Split(',')[1]));
            end = new Point(int.Parse(points[1].Split(',')[0]), int.Parse(points[1].Split(',')[1]));
        }

        public IEnumerable<Point> GeneratePoints()
        {
            var xDiff = Math.Sign(end.X - start.X);
            var yDiff = Math.Sign(end.Y - start.Y);
            var current = start;

            yield return current;

            while (current != end)
            {
                current.X += xDiff;
                current.Y += yDiff;

                yield return current;
            }
        }
    }
}