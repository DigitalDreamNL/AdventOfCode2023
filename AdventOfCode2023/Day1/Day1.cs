using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day1;

public class Day1 : Puzzle
{
    protected override int Day => 1;

    private const string PatternOnlyDigits = @"([0-9])";
    private const string PatternWithLetters = @"([0-9]|one|two|three|four|five|six|seven|eight|nine)";

    public override async Task Solve()
    {
        AdventOfCodeConsole.WritePuzzleHeader(1);
        await SolvePartOne();
        await SolvePartTwo();
    }
    
    public override async Task SolvePartOne()
    {
        var answer = await CalculateSumOfCalibrationValues(PatternOnlyDigits);
        AdventOfCodeConsole.WritePuzzlePartOneAnswer();
        AdventOfCodeConsole.WriteAnswer(answer.ToString());
    }

    public override async Task SolvePartTwo()
    {
        var answer = await CalculateSumOfCalibrationValues(PatternWithLetters);
        AdventOfCodeConsole.WritePuzzlePartTwoAnswer();
        AdventOfCodeConsole.WriteAnswer(answer.ToString());
    }

    private async Task<int> CalculateSumOfCalibrationValues(string pattern)
    {
        var sumOfCalibrationValues = 0;
        var input = ReadInput();
        await foreach (var line in input)
        {
            var firstDigit = GetFirstDigit(line, pattern);
            var lastDigit = GetLastDigit(line, pattern);
            var calibrationValue = firstDigit * 10 + lastDigit;
            sumOfCalibrationValues += calibrationValue;
        }

        return sumOfCalibrationValues;
    }

    private static int GetFirstDigit(string line, string pattern)
    {
        var matchesFromLeft = Regex.Matches(line, pattern);
        return ParseNumber(matchesFromLeft[0].Value);
    }

    private static int GetLastDigit(string line, string pattern)
    {
        var matchesFromLeft = Regex.Matches(line, pattern, RegexOptions.RightToLeft);
        return ParseNumber(matchesFromLeft[0].Value);
    }

    private static int ParseNumber(string number)
    {
        return number switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            _ => int.Parse(number)
        };
    }
}