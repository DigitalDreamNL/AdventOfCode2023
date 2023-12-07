using AdventOfCode2023.Day7;
using FluentAssertions;

namespace AdventOfCode2023.Test.Day7;

public class HandTests
{
    [Fact]
    public void ParseHandTests()
    {
        var hand = new Hand("32T3K 765");
        hand.Bet.Should().Be(765);
        hand.Cards.Should().BeEquivalentTo(new List<int>
        {
            3, 2, 10, 3, 13
        });
        hand.Result.Should().Be(HandResult.OnePair);
    }

    [Fact]
    public void HandResultTests()
    {
        new Hand("KKKKK 1").Result.Should().Be(HandResult.FiveOfAKind);
        new Hand("22232 2").Result.Should().Be(HandResult.FourOfAKind);
        new Hand("QQ3Q3 3").Result.Should().Be(HandResult.FullHouse);
        new Hand("66KJ6 4").Result.Should().Be(HandResult.ThreeOfAKind);
        new Hand("8484J 5").Result.Should().Be(HandResult.TwoPair);
        new Hand("7847K 6").Result.Should().Be(HandResult.OnePair);
        new Hand("23456 7").Result.Should().Be(HandResult.HighCard);
    }

    [Fact]
    public void HandSortingTests()
    {
        var hands = new List<Hand>
        {
            new("24356 1"),
            new("45667 1"),
            new("2222A 1"),
            new("KQ874 1"),
            new("QJ456 1"),
            new("T4895 1"),
            new("TT345 1"),
            new("5674K 1"),
            new("34567 1"),
            new("34568 1"),
            new("56788 1"),
            new("7894T 1"),
            new("T8746 1"),
            new("J7465 1"),
            new("66666 1"),
        };

        hands.SortByResultAndLabel();

        hands.Should().BeEquivalentTo(new List<Hand>
        {
            new("66666 1"),
            new("2222A 1"),
            new("TT345 1"),
            new("56788 1"),
            new("45667 1"),
            new("KQ874 1"),
            new("QJ456 1"),
            new("J7465 1"),
            new("T8746 1"),
            new("T4895 1"),
            new("7894T 1"),
            new("5674K 1"),
            new("34568 1"),
            new("34567 1"),
            new("24356 1"),
        });
    }

    [InlineData("32T3K 765", HandResult.OnePair, HandResult.OnePair)]
    [InlineData("T55J5 684", HandResult.OnePair, HandResult.OnePair)]
    [InlineData("KK677 28", HandResult.OnePair, HandResult.OnePair)]
    [InlineData("KTJJT 220", HandResult.OnePair, HandResult.OnePair)]
    [InlineData("QQQJA 483", HandResult.OnePair, HandResult.OnePair)]
    [Theory]
    public void HandResultTestsWithJokers(string line, HandResult result, HandResult resultWithJokers)
    {
        var hand = new Hand(line);
        hand.Result.Should().Be(result);
        hand.ApplyJokers();
        hand.Result.Should().Be(resultWithJokers);
    }
}