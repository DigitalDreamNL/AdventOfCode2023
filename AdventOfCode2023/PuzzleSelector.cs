using Spectre.Console;

namespace AdventOfCode2023;

public static class PuzzleSelector
{
    private static readonly List<int> Days = new() { 1, 4, 5, 7, 8 };

    public static Puzzle SelectPuzzle()
    {
        var day = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Please select a day")
                .PageSize(10)
                .AddChoices(Days)
                .HighlightStyle(new Style(Color.Green))
        );

        return day switch
        {
            1 => new Day1.Day1(),
            4 => new Day4.Day4(),
            5 => new Day5.Day5(),
            7 => new Day7.Day7(),
            8 => new Day8.Day8(),
            _ => throw new Exception($"Puzzle for day {day} not found")
        };
    }
}