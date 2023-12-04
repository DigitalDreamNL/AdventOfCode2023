using AdventOfCode2023.Day4;
using FluentAssertions;

namespace AdventOfCode2023.Test.Day4;

public class NumbersTests
{
    private readonly List<string> _input = new()
    {
        "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
        "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
        "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
        "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
        "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
        "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
    };

    private readonly List<int> _matches = new()
    {
        4, 2, 2, 1, 0, 0
    };

    private readonly List<int> _score = new()
    {
        8, 2, 2, 1, 0, 0
    };

    [Fact]
    public void TestInitialization()
    {
        var card = new ScratchCard(_input[3]);

        card.WinningNumbers.Should().BeEquivalentTo(new List<int>
        {
            41, 92, 73, 84, 69
        });
        card.MyNumbers.Should().BeEquivalentTo(new List<int>
        {
            59, 84, 76, 51, 58, 5, 54, 83
        });

        card.CalculateScore().Should().Be(1);
    }

    [Fact]
    public void CalculateMatches()
    {
        for (var i = 0; i < 6; i++)
        {
            new ScratchCard(_input[i]).CalculateMatches().Should().Be(_matches[i]);
        }
    }

    [Fact]
    public void CalculateScore()
    {
        for (var i = 0; i < 6; i++)
        {
            new ScratchCard(_input[i]).CalculateScore().Should().Be(_score[i]);
        }
    }
}