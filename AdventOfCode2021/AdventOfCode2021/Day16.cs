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

            ProcessPacket(binarystring, 0, 0, 0);

            return versions.ToString();

        }


        private (int position, int value) ProcessPacket(string binarystring, int layer, int subpackets, int value)
        {
            int position = 0;
            int packetCount = 0;
            while (position < binarystring.Length && (subpackets == 0 || packetCount < subpackets))
            {

                if (layer == 0 && binarystring[position..].All(x => x == '0'))
                    return (position, value);

                int version = Convert.ToInt32(binarystring[position..(position + 3)], 2);
                versions += version;
                Console.WriteLine(version);
                position += 3;
                int packetType = Convert.ToInt32(binarystring[position..(position + 3)], 2);
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

                }
                else
                {
                    char lengthTypeId = binarystring[position++];
                    if (lengthTypeId == '0')
                    {
                        int totalLength = Convert.ToInt32(binarystring[position..(position + 15)], 2);
                        position += 15;
                        var tmp = ProcessPacket(binarystring[position..(position + totalLength)], layer+1, 0, value);
                        position += totalLength;
                    }
                    else
                    {
                        var packets = Convert.ToInt32(binarystring[position..(position + 11)], 2);
                        position += 11;
                        position += ProcessPacket(binarystring[position..], layer + 1, packets, value).position ;

                    }
                }

                packetCount++;
            }

            return (position, value);
        }

        public override string SolveSecondPuzzle()
        {
            throw new NotImplementedException();
        }
    }
}