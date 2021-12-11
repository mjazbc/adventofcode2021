using aoc_core;

namespace AdventOfCode2021
{
    internal class Day08 : AdventPuzzle
    {

        public override string SolveFirstPuzzle()
        {
            return Input.AsStringArray()
                .Select(line => line.Split(" | ")[1])
                .SelectMany(x=>x.Split(' '))
                .Count(x => new[] { 2, 3, 4, 7 }.Contains(x.Length))
                .ToString();

        }

        public override string SolveSecondPuzzle()
        {
            var input = Input.AsStringArray();
            var sum = 0;
            foreach(var line in input)
            {
                var split = line.Split(" | ");
                var signals = new HashSet<HashSet<char>>(split[0].Split(' ').Select(x => new HashSet<char>(x.ToCharArray())));
                var digits = new HashSet<HashSet<char>>(split[1].Split(' ').Select(x => new HashSet<char>(x.ToCharArray())));

                var nums = new HashSet<char>[10];
                nums[1] = signals.Single(x => x.Count() == 2);
                signals.Remove(nums[1]);
                nums[4] = signals.Single(x => x.Count() == 4);
                signals.Remove(nums[4]);
                nums[7] = signals.Single(x => x.Count() == 3);
                signals.Remove(nums[7]);
                nums[8] = signals.Single(x => x.Count() == 7);
                signals.Remove(nums[8]);
                var topLine = nums[7].Except(nums[1]);
                nums[9] = signals.Single(x=> x.Intersect(nums[4].Union(topLine)).Count() == 5 && x.Count() == 6);
                signals.Remove(nums[9]);
                nums[3] = signals.Single(x => x.Except(nums[1]).Count() == 3 && x.Count() == 5);
                signals.Remove(nums[3]);
                nums[0] = signals.Single(x => (nums[1].Intersect(x)).Count() == 2 && x.Count() == 6);
                signals.Remove(nums[0]);
                nums[6] = signals.Single(x => x.Count() == 6);
                signals.Remove(nums[6]);
                nums[5] = signals.Single(x => x.Except(nums[6]).Count() == 0);
                signals.Remove(nums[5]);
                nums[2] = signals.Single();


                int fourdigitnum = 0;
                foreach(var x in digits)
                {
                    
                    for(int i = 0; i < nums.Length; i++)
                    {
                        
                        if (nums[i].SetEquals(x))
                        {
                            fourdigitnum = (fourdigitnum * 10) + i;
                            break;
                        }
                    }
                }

                sum += fourdigitnum;
            }

            return sum.ToString();
        }
    }
}