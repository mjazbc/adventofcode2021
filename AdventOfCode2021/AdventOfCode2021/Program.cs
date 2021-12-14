using aoc_core;

string inputPath = "./inputs/";

int day = 13;

string dayName = $"Day{day:00}";

string puzzleClassName = $"AdventOfCode2021.{dayName}";
var t = Type.GetType(puzzleClassName);

if (t == null)
    throw new Exception(puzzleClassName + " not founnd.");

var puzzle = Activator.CreateInstance(t) as AdventPuzzle;
puzzle!.Input.LoadFromFile(Path.Combine(inputPath, $"{dayName}.txt"));
puzzle.ParseInput();

puzzle.Solve(Puzzle.Both);
