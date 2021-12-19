using aoc_core;

namespace AdventOfCode2021
{
    internal class Day16 : AdventPuzzle
    {
        long versions = 0;
        public override string SolveFirstPuzzle()
        {
            string binarystring = String.Join(String.Empty, Input.AsString()
                .Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            ProcessPacket(binarystring, 0, 0);

            return versions.ToString();
        }

        private (int position, List<long> values) ProcessPacket(string binarystring, int layer, int subpackets)
        {
            int position = 0;
            int packetCount = 0;
            int packetType = 0;
            List<long> values = new List<long>();
            while (position < binarystring.Length && (subpackets == 0 || packetCount < subpackets))
            {

                if (layer == 0 && binarystring[position..].All(x => x == '0'))
                    return (position, values);

                int version = Convert.ToInt32(binarystring[position..(position + 3)], 2);
                versions += version;

                position += 3;
                packetType = Convert.ToInt32(binarystring[position..(position + 3)], 2);
                position += 3;

                if (packetType == 4)
                {
                    char stopBit = '1';
                    string literalNum = "";
                    do
                    {
                        stopBit = binarystring[position++];
                        literalNum += binarystring[position..(position + 4)];
                        position += 4;

                    }
                    while (stopBit != '0');

                    long literal = Convert.ToInt64(literalNum, 2);
                    values.Add(literal);

                }
                else
                {
                    char lengthTypeId = binarystring[position++];
                    if (lengthTypeId == '0')
                    {
                        int totalLength = Convert.ToInt32(binarystring[position..(position + 15)], 2);
                        position += 15;
                        var tmp = ProcessPacket(binarystring[position..(position + totalLength)], layer + 1, 0);
                        position += totalLength;
                        values.AddRange(AggregateValues(packetType, tmp.values));
                    }
                    else
                    {
                        var packets = Convert.ToInt32(binarystring[position..(position + 11)], 2);
                        position += 11;
                        var tmp = ProcessPacket(binarystring[position..], layer + 1, packets);
                        position += tmp.position;
                        values.AddRange(AggregateValues(packetType, tmp.values));
                    }
                }

                packetCount++;
            }

            return (position, values);
        }

        private static List<long> AggregateValues(int packetType, List<long> values)
        {
            switch (packetType)
            {
                case 0: return new List<long>() { values.Sum() };
                case 1: return new() { values.Aggregate((a, x) => a * x) };
                case 2: return new() { values.Min() };
                case 3: return new() { values.Max() };
                case 4: return values;
                case 5: return new() { values[0] > values[1] ? 1 : 0 };
                case 6: return new() { values[0] < values[1] ? 1 : 0 };
                case 7: return new() { values[0] == values[1] ? 1 : 0 };
                default: throw new Exception("dont know what to return");
            }
        }

        public override string SolveSecondPuzzle()
        {
            string binarystring = String.Join(String.Empty, Input.AsString()
                .Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            (_, List<long> vals) = ProcessPacket(binarystring, 0, 0);

            return vals.Single().ToString();
        }
    }
}