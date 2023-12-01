namespace AdventOfCode2023;

public abstract class Puzzle : IPuzzle
{
    protected abstract int Day { get; }
    protected IAsyncEnumerable<string> ReadInput() => File.ReadLinesAsync($"input/Day{Day}.txt");
    public abstract Task Solve();
    public abstract Task SolvePartOne();
    public abstract Task SolvePartTwo();
}