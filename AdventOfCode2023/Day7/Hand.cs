namespace AdventOfCode2023.Day7;

public class Hand
{
    public int Bet { get; }
    public List<int> Cards { get; private set; }
    public HandResult Result { get; private set; }

    public Hand(string line)
    {
        var parts = line.Split(' ');
        Bet = int.Parse(parts[1]);
        Cards = parts[0].ToCharArray().Select(c => c.ToCardValue()).ToList();
        Result = CalculateResult();
    }

    private HandResult CalculateResult()
    {
        var groups = Cards.GroupBy(
            c => c,
            c => c,
            (cardValue, cards) => cards.Count()).ToList().OrderByDescending(c => c).ToList();

        return groups.ToHandResult();
    }

    public void ApplyJokers()
    {
        ReplaceJacksWithJokers();
        Result = ApplyJokersToResult();
    }

    private void ReplaceJacksWithJokers()
    {
        Cards = Cards.Select(c => c == 11 ? 0 : c).ToList();
    }

    private HandResult ApplyJokersToResult()
    {
        var numberOfJokers = Cards.Count(c => c == 0);

        return Result switch
        {
            HandResult.FiveOfAKind => HandResult.FiveOfAKind,
            HandResult.FourOfAKind => numberOfJokers switch
            {
                4 => HandResult.FiveOfAKind,
                1 => HandResult.FiveOfAKind,
                _ => HandResult.FourOfAKind
            },
            HandResult.FullHouse => numberOfJokers switch
            {
                3 => HandResult.FiveOfAKind,
                2 => HandResult.FiveOfAKind,
                _ => HandResult.FullHouse
            },
            HandResult.ThreeOfAKind => numberOfJokers switch
            {
                3 => HandResult.FourOfAKind,
                1 => HandResult.FourOfAKind,
                _ => HandResult.ThreeOfAKind
            },
            HandResult.TwoPair => numberOfJokers switch
            {
                2 => HandResult.FourOfAKind,
                1 => HandResult.FullHouse,
                _ => HandResult.TwoPair
            },
            HandResult.OnePair => numberOfJokers switch
            {
                2 => HandResult.ThreeOfAKind,
                1 => HandResult.ThreeOfAKind,
                _ => HandResult.OnePair
            },
            HandResult.HighCard => numberOfJokers switch
            {
                1 => HandResult.OnePair,
                _ => HandResult.HighCard
            },
            _ => throw new Exception("Unexpected result")
        };
    }

    public int Compare(Hand hand)
    {
        if (Result > hand.Result) return 1;
        if (Result < hand.Result) return -1;

        for (var i = 0; i < Cards.Count; i++)
        {
            if (Cards[i] > hand.Cards[i]) return 1;
            if (Cards[i] < hand.Cards[i]) return -1;
        }

        return 0;
    }
}

public static class ExtensionMethods
{
    public static int ToCardValue(this char face)
    {
        return face switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 11,
            'T' => 10,
            _ => int.Parse(face.ToString())
        };
    }

    private static readonly List<int> FiveOfAKind = new() { 5 };
    private static readonly List<int> FourOfAKind = new() { 4, 1 };
    private static readonly List<int> FullHouse = new() { 3, 2 };
    private static readonly List<int> ThreeOfAKind = new() { 3, 1, 1 };
    private static readonly List<int> TwoPair = new() { 2, 2, 1 };
    private static readonly List<int> OnePair = new() { 2, 1, 1, 1 };
    public static HandResult ToHandResult(this List<int> groups)
    {
        if (groups.SequenceEqual(FiveOfAKind))
            return HandResult.FiveOfAKind;

        if (groups.SequenceEqual(FourOfAKind))
            return HandResult.FourOfAKind;

        if (groups.SequenceEqual(FullHouse))
            return HandResult.FullHouse;

        if (groups.SequenceEqual(ThreeOfAKind))
            return HandResult.ThreeOfAKind;

        if (groups.SequenceEqual(TwoPair))
            return HandResult.TwoPair;

        if (groups.SequenceEqual(OnePair))
            return HandResult.OnePair;

        return HandResult.HighCard;
    }

    public static void SortByResultAndLabel(this List<Hand> cards)
    {
        cards.Sort((a, b) => a.Compare(b));
    }
}

public enum HandResult
{
    HighCard = 0,
    OnePair = 1,
    TwoPair = 2,
    ThreeOfAKind = 3,
    FullHouse = 4,
    FourOfAKind = 5,
    FiveOfAKind = 6
}
