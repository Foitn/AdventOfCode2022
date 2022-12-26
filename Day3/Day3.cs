using Microsoft.VisualBasic;

namespace AdventOfCode2022.Day3;

public sealed class Day3 : Day
{
    public Day3()
    {
        this.ReadPuzzleInput("Day3\\PuzzleInputDay3.txt");
    }

    public override void SolvePartA()
    {
        List<RuckSack> sacks = this.PuzzleInput.Select(x => new RuckSack(x)).ToList();

        int theValue = sacks.Sum(x =>
        {
            int theValue = x.CharThatExistsInBoth();
            return theValue;
        });

        Console.WriteLine(theValue);
    }

    public override void SolvePartB()
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        int theValue = 0;

        var theGroups = this.PuzzleInput.Chunk(3);
        foreach (string[] theGroup in theGroups)
        {
            foreach (char c in alphabet)
            {
                if (theGroup.Where(x => x.Contains(c)) is { } theGroupLocal && theGroupLocal.Count() == 3)
                {
                    theValue += alphabet.IndexOf(c) + 1;
                }
            }
        }

        Console.WriteLine(theValue);
    }
}

file class RuckSack
{

    public RuckSack(string input)
    {
        this.LeftPart = input[..(input.Length / 2)];
        this.RightPart = input[(input.Length / 2)..];
    }

    public string LeftPart { get; set; }

    public string RightPart { get; set; }

    public int CharThatExistsInBoth()
    {
        char theChar = ' ';
        const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        foreach (char ch in alphabet.Where(ch => this.LeftPart.Contains(ch) && this.RightPart.Contains(ch)))
        {
            theChar = ch;
            break;
        }
        
        return alphabet.IndexOf(theChar)+1;
    }
}
