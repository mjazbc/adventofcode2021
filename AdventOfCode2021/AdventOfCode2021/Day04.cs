using aoc_core;

namespace AdventOfCode2021
{
    internal class Day04 : AdventPuzzle
    {

        public override string SolveFirstPuzzle()
        {
            var boardInput = Input.AsCustomType(ParseBingoBoard);
            return PlayBingo(boardInput, 1);
        }

        public override string SolveSecondPuzzle()
        {
            var boardInput = Input.AsCustomType(ParseBingoBoard);
            return PlayBingo(boardInput, boardInput.Boards!.Count);
        }

        private static string PlayBingo(BingoBoard boardInput, int numOfWinners)
        {
            Dictionary<int, Dictionary<int, int>> calledRows = new();
            Dictionary<int, Dictionary<int, int>> calledColumns = new();
            HashSet<int> wonBoards = new HashSet<int>();

            for (int i = 0; i < boardInput.Boards!.Count; i++)
            {
                calledRows[i] = new Dictionary<int, int>();
                calledColumns[i] = new Dictionary<int, int>();
            }

            foreach (var number in boardInput.Numbers!)
            {
                int boardNum = 0;
                foreach (var board in boardInput.Boards!)
                {
                    if (wonBoards.Contains(boardNum))
                    {
                        boardNum++;
                        continue;
                    }

                    for (int y = 0; y < 5; y++)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            if (board[y][x] == number)
                            {
                                board[y][x] = -1;

                                if (!calledRows[boardNum].ContainsKey(y))
                                    calledRows[boardNum][y] = 0;

                                if (!calledColumns[boardNum].ContainsKey(x))
                                    calledColumns[boardNum][x] = 0;

                                calledRows[boardNum][y]++;
                                calledColumns[boardNum][x]++;

                                if (calledRows[boardNum][y] == 5 || calledColumns[boardNum][x] == 5)
                                {
                                    wonBoards.Add(boardNum);
                                    if (wonBoards.Count == numOfWinners)
                                        return (board.SelectMany(x => x).Where(x => x > 0).Sum() * number).ToString();
                                }
                            }
                        }
                    }
                    boardNum++;
                }
            }

            throw new Exception("Result not found.");
        }

        public BingoBoard ParseBingoBoard(string inputText)
        {
            var lines = inputText.Split(Environment.NewLine);
            var board = new BingoBoard
            {
                Numbers = lines.First().Split(',').Select(int.Parse).ToArray(),
                Boards = new List<int[][]>()
            };

            var boardArray = new int[5][];
            int row = 0;
            foreach (var line in lines.Skip(2))
            {
                if (string.IsNullOrEmpty(line))
                {
                    board.Boards.Add(boardArray);
                    boardArray = new int[5][];
                    row = 0;
                    continue;
                }

                boardArray[row++] = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }
            board.Boards.Add(boardArray);

            return board;
        }
    }
    class BingoBoard
    {
        public int[]? Numbers { get; set; } 

        public List<int[][]>? Boards { get; set; }  
    }
}