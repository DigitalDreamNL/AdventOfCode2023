namespace AdventOfCode2023.Day4;

public class ScratchCard
{
    
    public List<int> MyNumbers { get; } = new();
    public List<int> WinningNumbers { get; } = new();

    public ScratchCard(string input)
    {
        var parts = input.Replace("  ", " ").Split(": ");
        var numbers = parts[1].Split(" | ");
        
        ParseNumbers(WinningNumbers, numbers[0]);
        ParseNumbers(MyNumbers, numbers[1]);
    }

    private static void ParseNumbers(ICollection<int> list, string numbers)
    {
        var winningNumbers = numbers.Split(" ");
        foreach (var winningNumber in winningNumbers)
        {
            list.Add(int.Parse(winningNumber));
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