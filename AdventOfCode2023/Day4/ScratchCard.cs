namespace AdventOfCode2023.Day4;

public class ScratchCard
{
    public List<int> MyNumbers { get; } = new();
    public List<int> WinningNumbers { get; } = new();

    public ScratchCard(string input)
    {
        var parts = GetNumbersPart(input);
        var numbers = SplitNumberParts(parts);
        ParseNumbers(WinningNumbers, numbers[0]);
        ParseNumbers(MyNumbers, numbers[1]);
    }

    private static string GetNumbersPart(string input)
    {
        return input.Replace("  ", " ").Split(": ")[1];
    }

    private static string[] SplitNumberParts(string parts)
    {
        return parts.Split(" | ");
    }

    private static void ParseNumbers(ICollection<int> list, string numbersString)
    {
        var numbers = numbersString.Split(" ");
        foreach (var number in numbers)
        {
            list.Add(int.Parse(number));
        }
    }

    public int CalculateMatches()
    {
        return MyNumbers.Count(number => WinningNumbers.Contains(number));
    }

    public int CalculateScore()
    {
        var matches = CalculateMatches();

        return matches == 0 ? 0 : (int)Math.Pow(2, matches - 1);
    }
}