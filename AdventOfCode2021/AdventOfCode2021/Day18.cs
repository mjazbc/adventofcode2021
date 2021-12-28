using aoc_core;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    internal class Day18 : AdventPuzzle
    {
        string flattened = "";
        public override string SolveFirstPuzzle()
        {
            var input = Input.AsStringArray();
            var tree = BuildTree(input[0]);

            foreach (var line in input.Skip(1))
            {
                tree = SumTrees(tree, BuildTree(line));
            }
            
            return Magnitude(tree).ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var input = Input.AsStringArray();
            var tree = BuildTree(input[0]);
            flattened = "";
            int max = 0;

            for(int i = 0; i< input.Length; i++)
            {
                for(int j = 0; j < input.Length; j++)
                {
                    if (i == j)
                        continue;

                    var first = BuildTree(input[i]);
                    var second = BuildTree(input[j]);

                    var m = Magnitude(SumTrees(first, second));
                    if(m > max)
                    {
                        max = m;
                    }


                    m = Magnitude(SumTrees(second, first));
                    if (m > max)
                    {
                        max = m;
                    }

                }
            }

            return max.ToString();
        }


        private Pair SumTrees(Pair tree, Pair second)
        {
            tree = new Pair()
            {
                LeftNode = tree,
                RightNode = second
            };

            bool exploded = false;
            bool split = false;

            do
            {
                exploded = Explode(ref tree);
                if (exploded)
                    continue;

                split = Split(tree);

            } while (exploded || split);

            return tree;
        }

        public bool Explode(ref Pair tree)
        {
            var exploded = FindExplodeNode(tree, 0);
            if (exploded == null) {
                flattened = "";
                return false;
            }

            var flat = "[" + flattened + "]";
            var leftPart = flat.Substring(0, exploded.Value.length);
            var rightPart = flat.Substring(exploded.Value.length);
            string explode = "[" + exploded.Value.left + "," + exploded.Value.right + "]";
            rightPart = rightPart.Substring(explode.Length);

            var m = Regex.Match(leftPart, @"\d{1,2}", RegexOptions.RightToLeft);
            if (m.Success)
            {
                var newVal = int.Parse(m.Value) + exploded.Value.left;
                leftPart = leftPart.Substring(0, m.Index) + newVal + leftPart.Substring(m.Index + m.Length);
            }

            m = Regex.Match(rightPart, @"\d{1,2}");
            if (m.Success)
            {
                var newVal = int.Parse(m.Value) + exploded.Value.right;
                rightPart = rightPart.Substring(0, m.Index) + newVal + rightPart.Substring(m.Index + m.Length);
            }

            flat = leftPart + "0" + rightPart;
            flattened = "";

            tree = BuildTree(flat);
            return true;
        }

        public bool Split(Pair tree)
        {
            if(tree == null)
                return false;

            if(tree.Value.HasValue && tree.Value.Value >= 10)
            {
                var val = tree.Value.Value;
                tree.Value = null;
                tree.LeftNode = new Pair { Value = (int)Math.Floor(val / 2.0)};
                tree.RightNode = new Pair { Value = (int)Math.Ceiling(val / 2.0) };

                return true;
            }

            bool isSplit = Split(tree.LeftNode) || Split(tree.RightNode);

            return isSplit;
        }

        public int Magnitude(Pair tree)
        {
            if(tree.Value.HasValue)
                return tree.Value.Value;

            return 3 * Magnitude(tree.LeftNode) + 2 * Magnitude(tree.RightNode);
        }
        public (int? left, int? right, int length)? FindExplodeNode(Pair tree, int depth)
        {
            if (depth == 4 && tree?.RightNode?.Value != null && tree?.LeftNode?.Value != null)
            {
                var values = (tree.LeftNode.Value.Value, tree.RightNode.Value.Value, flattened.Length);
                flattened += tree.LeftNode.Value.Value + ","+ tree.RightNode.Value.Value;

                return values;
            }

            (int? left, int? right, int _)? pair = null;
            if (tree.LeftNode != null) 
            {
                if (tree.LeftNode.Value.HasValue)
                {
                    flattened += tree.LeftNode.Value.ToString();
                }
                else
                {
                    flattened += "[";
                    var result = FindExplodeNode(tree.LeftNode, depth + 1);
                    if (pair == null)
                        pair = result;
                    //if (pair != null)
                    //    return pair;
                    flattened += "]";
                }
            }

            flattened += ",";

            if (tree.RightNode != null)
            {
                if (tree.RightNode.Value.HasValue)
                {
                    flattened += tree.RightNode.Value.ToString();
                }
                else
                {
                    flattened += "[";
                    var result = FindExplodeNode(tree.RightNode, depth + 1);
                    if(pair == null)
                        pair = result;
                    flattened += "]";
                }
            }

            return pair;
        }

        public Pair BuildTree(string input)
        { 
            if (int.TryParse(input, out int value))
                return new Pair { Value = value };

            var pair = new Pair();
            input = input.Substring(1, input.Length - 2);
            int outerComma = FindComma(input);
            if (outerComma < 0)
                throw new Exception("could not parse");

            pair.LeftNode = BuildTree(input.Substring(0, outerComma));
            pair.RightNode = BuildTree(input.Substring(outerComma + 1));

            return pair;
        }

        public int FindComma(string input)
        {
            int start = 0;
            while (true) { 
               
                var commaIdx = input.IndexOf(',', start);
                var leftPart = input.Substring(0, commaIdx);
                var righPart = input.Substring(commaIdx + 1);

                if (leftPart.Count(x => x == '[') == leftPart.Count(x => x == ']') && righPart.Count(x => x == '[') == righPart.Count(x => x == ']'))
                    return commaIdx;

                start = commaIdx +1;
            }
        }
    }

    class Pair
    {
        public Pair? LeftNode { get; set; }
        public Pair? RightNode { get; set; }
        public int? Value { get; set; }

    }
}