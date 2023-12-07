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
        await HandleScratchCards(input);

        AdventOfCodeConsole.WritePuzzlePartTwoAnswer();
        var totalNumberOfScratchCards = _cards.Select(c => c.Value).Sum().ToString();
        AdventOfCodeConsole.WriteAnswer(totalNumberOfScratchCards);
    }

    private async Task HandleScratchCards(IAsyncEnumerable<string> input)
    {
        var cardNumber = 0;
        await foreach (var cardInput in input)
        {
            cardNumber++;
            HandleScratchCard(cardNumber, cardInput);
        }
    }

    private void HandleScratchCard(int cardNumber, string cardInput)
    {
        IncrementNumberOfCards(cardNumber); // Add one original card
        var numberOfCards = _cards[cardNumber];
        var matches = new ScratchCard(cardInput).CalculateMatches();
        IncrementNextCards(cardNumber, numberOfCards, matches);
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
        _cards[cardNumber] += (numberOfCardsToAdd ?? 1);
    }
}