namespace AdventOfCode2022.Day1;

public sealed class Day1 : Day
{
    public Day1()
    {
        this.ReadPuzzleInput("Day1\\PuzzleInputDay1.txt");
    }

    public override void SolvePartA()
    {
        IEnumerable<Elf> allElves = this.ParsePuzzleInputToElves();

        Console.WriteLine(allElves.MaxBy(x => x.TotalCalories)!.TotalCalories);
    }

    public override void SolvePartB()
    {
        IEnumerable<Elf> allElves = this.ParsePuzzleInputToElves().OrderByDescending(x => x.TotalCalories);

        var top3Elves = allElves.Take(3);

        Console.WriteLine(top3Elves.Sum(x => x.TotalCalories));
    }

    private IEnumerable<Elf> ParsePuzzleInputToElves()
    {
        List<string> theStrings = new();
        foreach (string s in this.PuzzleInput)
        {
            if (string.IsNullOrEmpty(s))
            {
                yield return new Elf(theStrings.ToList());
                theStrings.Clear();
            }
            else
            {
                theStrings.Add(s);
            }
        }

        yield return new Elf(theStrings);
    }

    private record Elf(IEnumerable<string> Calories)
    {
        private IEnumerable<int> CaloriesAsIntegers => this.Calories.Select(x => int.TryParse(x, out int theValue) ? theValue : 0);

        public int TotalCalories => this.CaloriesAsIntegers.Sum();
    }
}