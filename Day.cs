namespace AdventOfCode2022;

public abstract class Day
{
    public string[] PuzzleInput { get; set; }

    public virtual void SolvePartA()
    {
        Console.WriteLine("A: Not yet implemented");
    }

    public virtual void SolvePartB()
    {
        Console.WriteLine("B: Not yet implemented");
    }

    public virtual void ReadPuzzleInput(string inputFilePath)
    {
        this.PuzzleInput = File.ReadAllLines(inputFilePath);
    }
}