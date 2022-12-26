namespace AdventOfCode2022.Day4;

public sealed class Day4 : Day
{
    public Day4()
    {
        this.ReadPuzzleInput("Day4\\PuzzleInputDay4.txt");
    }

    public override void SolvePartA()
    {
        var thePairs = this.PuzzleInput.Select(x => new Pair(x));
        
        int numberOfFullyOverlappingPairs = thePairs.Count(x => x.First.IsPartOfRange(x.Second) || x.Second.IsPartOfRange(x.First));

        Console.WriteLine(numberOfFullyOverlappingPairs);
    }

    public override void SolvePartB()
    {
        //this.PuzzleInput = new[]
        //{
        //    "2-4,6-8",
        //    "2-3,4-5",
        //    "5-7,7-9",
        //    "2-8,3-7",
        //    "6-6,4-6",
        //    "2-6,4-8",
        //};

        var thePairs = this.PuzzleInput.Select(x => new Pair(x));

        int numberOfFullyOverlappingPairs = thePairs.Count(x => x.First.HasAtLeastOneOverlap(x.Second) || x.Second.HasAtLeastOneOverlap(x.First));

        Console.WriteLine(numberOfFullyOverlappingPairs);
    }
}

file class Pair
{

    public Pair(string input)
    {
        var theRanges = input.Split(',');
        var theFirstRange = theRanges[0].Split('-');
        var theSecondRange = theRanges[1].Split('-');

        this.First = new Range(int.Parse(theFirstRange[0]), int.Parse(theFirstRange[1]));
        this.Second = new Range(int.Parse(theSecondRange[0]), int.Parse(theSecondRange[1]));
    }

    public Range First { get; }
    public Range Second { get; }
}

file record Range(int Start, int End)
{
    public bool IsPartOfRange(Range theOtherRange)
    {
        return Start >= theOtherRange.Start && End <= theOtherRange.End;
    }

    public bool HasAtLeastOneOverlap(Range theOtherRange)
    {
        return 
            Start >= theOtherRange.Start && Start <= theOtherRange.End ||
            End >= theOtherRange.Start && End <= theOtherRange.End;
    }
}