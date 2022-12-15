namespace AdventOfCode2022.Day2;

public sealed class Day2 : Day
{
    public Day2()
    {
        this.ReadPuzzleInput("Day2\\PuzzleInputDay2.txt");
    }

    public override void SolvePartA()
    {
        List<Round> rounds = this.PuzzleInput.Select(x => new Round(x)).ToList();

        int totalCount = rounds.Sum(x => x.PointsForLastHand());

        Console.WriteLine(totalCount);
    }

    public override void SolvePartB()
    {
        List<Round> rounds = this.PuzzleInput.Select(x => new Round(x)).ToList();

        int totalCount = rounds.Sum(x => x.PointsForOutcome());

        Console.WriteLine(totalCount);
    }
}

file enum Hand
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}
file enum Outcome
{
    Loss = 0,
    Draw = 3,
    Win = 6
}

file static class HandExtensions
{
    public static int PointsForHandVsHand(this Hand hand, Hand otherHand) =>
        (int)hand.DoesHandWin(otherHand) + (int)hand;

    public static Outcome DoesHandWin(this Hand hand, Hand otherHand) =>
        (hand, otherHand) switch
        {
            (Hand.Rock, Hand.Rock) => Outcome.Draw,
            (Hand.Rock, Hand.Paper) => Outcome.Loss,
            (Hand.Rock, Hand.Scissors) => Outcome.Win,
            (Hand.Paper, Hand.Rock) => Outcome.Win,
            (Hand.Paper, Hand.Paper) => Outcome.Draw,
            (Hand.Paper, Hand.Scissors) => Outcome.Loss,
            (Hand.Scissors, Hand.Rock) => Outcome.Loss,
            (Hand.Scissors, Hand.Paper) => Outcome.Win,
            (Hand.Scissors, Hand.Scissors) => Outcome.Draw,
            _ => throw new InvalidOperationException()
        };

    public static Hand DetermineHandBasedOnOutcome(this Hand hand, Outcome outcome)
    {
        Hand[] possibleHands = Enum.GetValues<Hand>();

        foreach (Hand possibleHand in possibleHands)
        {
            if (possibleHand.DoesHandWin(hand) == outcome)
            {
                return possibleHand;
            }
        }

        return Hand.Paper;
    }
}

file class Round
{
    public Round(string inputString)
    {
        this.FirstHand = this.GetHandFromValue(inputString[0]);
        this.LastHand = this.GetHandFromValue(inputString[2]);
        this.Outcome = this.GetOutcomeFromValue(inputString[2]);
    }

    public Hand FirstHand { get; }
    public Hand LastHand { get; }
    public Outcome Outcome { get; }

    public int PointsForLastHand() => this.LastHand.PointsForHandVsHand(this.FirstHand);

    public int PointsForOutcome() => (int)this.FirstHand.DetermineHandBasedOnOutcome(this.Outcome) + (int)this.Outcome;

    private Hand GetHandFromValue(char input) =>
        input switch
        {
            'A' => Hand.Rock,
            'B' => Hand.Paper,
            'C' => Hand.Scissors,
            'X' => Hand.Rock,
            'Y' => Hand.Paper,
            'Z' => Hand.Scissors,
            _ => throw new InvalidOperationException()
        };

    private Outcome GetOutcomeFromValue(char input) =>
        input switch
        {
            'X' => Outcome.Loss,
            'Y' => Outcome.Draw,
            'Z' => Outcome.Win,
            _ => throw new InvalidOperationException()
        };
}