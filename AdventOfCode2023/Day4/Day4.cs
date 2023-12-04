namespace AdventOfCode2023.Day4;

public class Day4 : Puzzle
{
    protected override int Day => 4;
    
    private readonly Dictionary<int, int> _cards = new();
    
    public override async Task Solve()
    {
        AdventOfCodeConsole.WritePuzzleHeader(4);
        await SolvePartOne();
        await SolvePartTwo();
    }

    public override async Task SolvePartOne()
    {
        var totalScore = 0;
        var input = ReadInput();
        await foreach(var cardInput in input)
        {
            var card = new ScratchCard(cardInput);
            totalScore += card.CalculateScore();
        }
        AdventOfCodeConsole.WritePuzzlePartOneAnswer();
        AdventOfCodeConsole.WriteAnswer(totalScore.ToString());
    }

    public override async Task SolvePartTwo()
    {
        var input = ReadInput();
        var cardNumber = 0;
        await foreach (var cardInput in input)
        {
            cardNumber++;
            IncrementNumberOfCards(cardNumber);
            var card = new ScratchCard(cardInput);
            var numberOfCards = _cards[cardNumber];
            var matches = card.CalculateMatches();
            IncrementNextCards(cardNumber, numberOfCards, matches);
        }
        
        AdventOfCodeConsole.WritePuzzlePartTwoAnswer();
        AdventOfCodeConsole.WriteAnswer(_cards.Select(c => c.Value).Sum().ToString());
    }

    private void IncrementNextCards(int cardNumber, int numberOfCards, int matches)
    {
        for (var i = cardNumber + 1; i < cardNumber + 1 + matches; i++)
        {
            IncrementNumberOfCards(i, numberOfCards);
        }
    }

    private void IncrementNumberOfCards(int cardNumber, int? numberOfCardsToAdd = null)
    {
        _cards.TryAdd(cardNumber, 0);
        _cards[cardNumber] = _cards[cardNumber] + (numberOfCardsToAdd ?? 1);
    }
}