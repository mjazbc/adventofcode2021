using aoc_core;
using System.Text;

namespace AdventOfCode2021
{
    internal class Day13 : AdventPuzzle
    {
        HashSet<(int x, int y)> dots = new();
        List<(string axis, int coordinate)> folds = new();
        public override void ParseInput()
        {
            dots = Input.AsStringArray()
                .TakeWhile(x => !string.IsNullOrEmpty(x))
                .Select(x => (x: int.Parse(x.Split(',')[0]), y: int.Parse(x.Split(',')[1])))
                .ToHashSet();

            folds = Input.AsStringArray()
                .Where(x => x.StartsWith("fold along "))
                .Select(x => x.Replace("fold along ", "").Split('='))
                .Select(x => (axis: x[0], coordinate: int.Parse(x[1])))
                .ToList();
        }
        public override string SolveFirstPuzzle()
        {
            var foldedDots = FoldDots(new HashSet<(int x, int y)>(dots), folds.Take(1));
            return foldedDots.Count().ToString();
        }

        private static HashSet<(int x, int y)> FoldDots(HashSet<(int x, int y)> dots, IEnumerable<(string axis, int coordinate)> folds)
        {
            int xsize = dots.Max(dot => dot.x);
            int ysize = dots.Max(dot => dot.y);
            foreach (var (axis, coordinate) in folds)
            {
                var newDots = new HashSet<(int x, int y)>();

                if (axis == "y")
                {
                    foreach (var (x, y) in dots)
                    {
                        if (y > coordinate)
                            newDots.Add((x, ysize - y));
                        else
                            newDots.Add((x, y));

                    }

                    ysize = (ysize / 2) - 1;
                }
                else if (axis == "x")
                {
                    foreach (var (x, y) in dots)
                    {
                        if (x > coordinate)
                            newDots.Add((xsize - x, y));
                        else
                            newDots.Add((x, y));

                    }

                    xsize = (xsize / 2) - 1;
                }

                dots = newDots;
            }

            return dots;
        }

        private static string PrintDots(HashSet<(int x, int y)> dots)
        {
            StringBuilder sb = new StringBuilder();
            int xsize = dots.Max(dot => dot.x);
            int ysize = dots.Max(dot => dot.y);
            sb.AppendLine();
            for (int y = 0; y <= ysize; y++)
            {
                for (int x = 0; x <= xsize; x++)
                {
                    if (dots.Contains((x, y)))
                       sb.Append('#');
                    else
                       sb.Append(' ');
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var foldedDots = FoldDots(new HashSet<(int x, int y)>(dots), folds);
            return PrintDots(foldedDots);
        }
    }
}