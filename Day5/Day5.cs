using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day5;

public sealed class Day5 : Day
{
    public Day5()
    {
        this.ReadPuzzleInput("Day5\\PuzzleInputDay5.txt");
    }

    public override void SolvePartA()
    {
        Boxes theBoxes = new(this.PuzzleInput.TakeWhile(x => !string.IsNullOrEmpty(x)));
        Console.WriteLine(theBoxes);

        var theInstructions = this.PuzzleInput.SkipWhile(x => !string.IsNullOrWhiteSpace(x)).Skip(1).Select(x => new Instruction(x));

        foreach (Instruction instruction in theInstructions)
        {
            for (int i = 0; i < instruction.Amount; i++)
            {
                theBoxes.MoveBox(instruction.Start, instruction.Stop);
            }
        }

        Console.WriteLine(theBoxes);

        Console.WriteLine(string.Join("",theBoxes.GetHighestBoxes().Select(x=> x.Letter)));
    }

    public override void SolvePartB()
    {
    }
}

file class Boxes
{
    private Regex boxRegex = new Regex(@"\[(?<theLetter>\S)\]");
    private List<List<Box>> theBoxes = new();

    private int numberOfPiles;

    public Boxes(IEnumerable<string> boxOverview)
    {
        numberOfPiles = boxOverview.Last().Length / 4 +1;
        for (int i = 0; i < numberOfPiles; i++)
        {
            theBoxes.Add(new List<Box>());

        }

        int counter = 0;
        foreach (string boxLine in boxOverview.Reverse().Skip(1))
        {
            for (int i = 0; i < numberOfPiles; i++)
            {
                if (this.boxRegex.Match(boxLine[(i * 4)..(i * 4 + 3)]) is Match { Success: true } theMatch)
                {
                    theBoxes[i].Add(new Box(theMatch.Groups["theLetter"].Value));
                }
            }
            counter++;
        }
    }

    public void MoveBox(int from, int to)
    {
        from = from - 1;
        to = to - 1;
        if (this.theBoxes[from].Count > 0)
        {
            Box theBoxToReplace = theBoxes[from].Last();
            theBoxes[from].RemoveAt(theBoxes[from].Count - 1);
            this.theBoxes[to].Add(theBoxToReplace);
        }
    }

    public IEnumerable<Box> GetHighestBoxes()
    {
        foreach (var stack in this.theBoxes)
        {
            yield return stack.Last();
        }
    }

    public override string ToString()
    {
        StringBuilder builder = new();

        for (int i = 0; i < numberOfPiles; i++)
        {
            builder.Append($" {i+1}  ");
        }

        builder.AppendLine();

        for (int i = 0; i < theBoxes.Max(x=>x.Count); i++)
        {
            for (int j = 0; j < numberOfPiles; j++)
            {
                if (this.theBoxes[j].Count > i && this.theBoxes[j][i].Letter is { } theLetter && !string.IsNullOrWhiteSpace(theLetter))
                {
                    builder.Append($"[{theLetter}]");
                }
                else
                {
                    builder.Append("   ");
                }
                builder.Append(" ");
            }
            builder.AppendLine();
        }

        return builder.ToString();
    }
}

file class Instruction
{
    //move 6 from 2 to 1
    private Regex instructionRegex = new(@"move (?<amount>\d*) from (?<start>\d*) to (?<stop>\d*)");

    public Instruction(string theInstructionAsString)
    {
        if (instructionRegex.Match(theInstructionAsString) is { } theMatch)
        {
            Amount = int.Parse(theMatch.Groups["amount"].Value);
            Start = int.Parse(theMatch.Groups["start"].Value);
            Stop = int.Parse(theMatch.Groups["stop"].Value);
        }
    }

    public int Amount { get; }
    public int Start { get; }
    public int Stop { get; }

}

file record Box(string? Letter);