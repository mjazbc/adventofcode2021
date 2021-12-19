using aoc_core;
using System.Text;

namespace AdventOfCode2021
{
    internal class Day14 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var polymer = Input.AsStringArray().First();
            var replacements = Input.AsStringArray().Skip(2).Select(x => x.Split(" -> ")).ToDictionary(x=>x[0], x=>x[1]);

            for(int i = 0; i< 10; i++)
            {
                string newPolymer = "";
                foreach (var (first, second) in polymer.Zip(polymer[1..]))
                {
                    newPolymer = newPolymer + first + replacements[first + "" + second];
                }

                polymer = newPolymer + polymer.Last();
            }

            var groups = polymer.GroupBy(x => x).Select(x => x.Count());
            return (groups.Max() - groups.Min()).ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var polymer = Input.AsStringArray().First();
            var replacements = Input.AsStringArray()
                .Skip(2)
                .Select(x => x.Split(" -> "))
                .ToDictionary(x => (x[0][0], x[0][1]),x => x[1].First());

            var pairs = new Dictionary<(char, char), long>();
            foreach (var pair in polymer.Zip(polymer[1..]))
            {
                AddToDict(pairs, 1, pair);
            }

            for (int i = 0; i < 40; i++)
            {
                var pairsCopy = new Dictionary<(char, char), long>();
                foreach (var key in pairs.Where(x => x.Value > 0).Select(x => x.Key).ToList())
                {
                    var num = pairs[key];
                    var replacement = replacements[key];
                    AddToDict(pairsCopy, num, (key.Item1, replacement));
                    AddToDict(pairsCopy, num, (replacement, key.Item2));
                }

                pairs = pairsCopy;
            }

            Dictionary<char, long> counts = Count(polymer, pairs);

            return (counts.Values.Max() - counts.Values.Where(x => x > 0).Min()).ToString();
        }

        private static Dictionary<char, long> Count(string polymer, Dictionary<(char, char), long> pairs)
        {
            var counts = new Dictionary<char, long>();
            foreach (var pair in pairs)
            {
                AddToDict(counts, pair.Value, pair.Key.Item1);
            }
            counts[polymer.Last()]++;
            return counts;
        }

        private static void AddToDict<T>(Dictionary<T, long> dict, long num, T key)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, num);
            }
            else
            {
                dict[key] += num;
            }
        }
    }
}