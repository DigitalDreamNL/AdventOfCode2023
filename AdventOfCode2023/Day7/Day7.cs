namespace AdventOfCode2023.Day7;

public class Day7 : Puzzle
{
    protected override int Day => 7;

    private readonly List<Hand> _hands = new();
    public override async Task Solve()
    {
        await Initialize();
        await SolvePartOne();
        await SolvePartTwo();
    }

    private async Task Initialize()
    {
        var input = ReadInput();
        await foreach (var line in input)
        {
            _hands.Add(new Hand(line));
        }
    }

    public override Task SolvePartOne()
    {
        var totalScore = CalculateTotalScore();

        AdventOfCodeConsole.WritePuzzlePartOneAnswer();
        AdventOfCodeConsole.WriteAnswer(totalScore);
        return Task.CompletedTask;

    }

    public override Task SolvePartTwo()
    {
        _hands.ForEach(h => h.ApplyJokers());
        var totalScore = CalculateTotalScore();

        AdventOfCodeConsole.WritePuzzlePartOneAnswer();
        AdventOfCodeConsole.WriteAnswer(totalScore);
        return Task.CompletedTask;
    }

    private int CalculateTotalScore()
    {
        _hands.SortByResultAndLabel();
        var totalScore = 0;
        for (var i = _hands.Count; i >= 1; i--)
        {
            totalScore += i * _hands[i - 1].Bet;
        }

        return totalScore;
    }
}