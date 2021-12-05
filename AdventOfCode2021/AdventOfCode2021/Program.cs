using aoc_core;

string inputPath = "./inputs/";

int day = 5;

string dayName = $"Day{day:00}";

string puzzleClassName = $"{typeof(Program).Assembly.GetName().Name}.{dayName}";
var t = Type.GetType(puzzleClassName);

if (t == null)
    throw new Exception(puzzleClassName + " not founnd.");

var puzzle = Activator.CreateInstance(t) as AdventPuzzle;
puzzle!.Input.LoadFromFile(Path.Combine(inputPath, $"{dayName}.txt"));
puzzle.ParseInput();

puzzle.Solve(Puzzle.Both);
