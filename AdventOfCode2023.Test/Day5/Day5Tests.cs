using AdventOfCode2023.Day5;
using FluentAssertions;

namespace AdventOfCode2023.Test.Day5;

public class Day5Tests
{
    [Fact]
    public void DetermineLineTypeTests()
    {
        var puzzle = new AdventOfCode2023.Day5.Day5();

        puzzle.DetermineLineType("").Should().Be(LineType.Empty);
        puzzle.DetermineLineType("seeds: 79 14 55 13").Should().Be(LineType.Seeds);
        puzzle.DetermineLineType("seed-to-soil map:").Should().Be(LineType.Map);
        puzzle.DetermineLineType("50 98 2").Should().Be(LineType.Values);
    }

    [Fact]
    public void ParseSeedsTests()
    {
        var puzzle = new AdventOfCode2023.Day5.Day5();

        var seeds = puzzle.ParseSeeds("seeds: 79 14 55 13");
        seeds.Should().BeEquivalentTo(new List<int> { 79, 14, 55, 13 });
    }

    [Fact]
    public void ParsedRangedSeedsTests()
    {
        var puzzle = new AdventOfCode2023.Day5.Day5();

        var seeds = puzzle.ParseRangedSeeds("seeds: 79 14 55 13");
        seeds.Should().BeEquivalentTo(new List<long>
        {
            79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67
        });
    }
    
    [Fact]
    public void ParseMapTypeTests()
    {
        var puzzle = new AdventOfCode2023.Day5.Day5();

        var mapType = puzzle.ParseMapType("seed-to-soil map:");
        mapType.Should().BeEquivalentTo(new MapType
        {
            Source = "seed",
            Destination = "soil"
        });
    }

    [Fact]
    public void ParseMapTests()
    {
        var puzzle = new AdventOfCode2023.Day5.Day5();

        var map = puzzle.ParseMap("50 98 2", new MapType
        {
            Source = "source",
            Destination = "target"
        });

        map.Should().BeEquivalentTo(new Map
        {
            Destination = "target",
            Source ="source",
            SourceStart = 98,
            SourceEnd = 99,
            DestinationStart = 50,
            DestinationEnd = 51
        });
    }

    [Fact]
    public void MapValuesTests()
    {
        var puzzle = new AdventOfCode2023.Day5.Day5();
        
        var mappings = new List<Map>
        {
            new Map
            {
                Destination = "soil",
                Source = "seed",
                DestinationStart = 70,
                SourceStart = 50,
                SourceEnd = 51
            },
            new Map
            {
                Destination = "soil",
                Source = "seed",
                DestinationStart = 300,
                SourceStart = 53,
                SourceEnd = 60,
            },
            new Map
            {
                Destination = "soil",
                Source = "fertilizer",
                DestinationStart = 90,
                SourceStart = 50,
                SourceEnd = 51,
            },
            new Map
            {
                Destination = "seed",
                Source = "fertilizer",
                DestinationStart = 80,
                SourceStart = 50,
                SourceEnd = 51,
            },
        };

        puzzle.MapValues(50, mappings, "seed", "soil").Should().Be(70);
        puzzle.MapValues(51, mappings, "seed", "soil").Should().Be(71);
        puzzle.MapValues(52, mappings, "seed", "soil").Should().Be(52);
        puzzle.MapValues(53, mappings, "seed", "soil").Should().Be(300);
    }

    [Fact]
    public async Task Main()
    {
        var puzzle = new AdventOfCode2023.Day5.Day5();

        await puzzle.Initialize();

        puzzle.Seeds.Should().BeEquivalentTo(new List<int> { 79, 14, 55, 13 });

        puzzle.Mappings.Count().Should().Be(18);
        puzzle.Mappings.Should().ContainEquivalentOf(new Map
        {
            Source = "temperature",
            Destination = "humidity",
            SourceStart = 69,
            SourceEnd = 69,
            DestinationStart = 0,
            DestinationEnd = 0
        });
    }

    [Fact]
    public async Task SolvePartOneTests()
    {
        var puzzle = new AdventOfCode2023.Day5.Day5();
        await puzzle.Initialize();
        await puzzle.SolvePartOne();
    }

    [Fact]
    public async Task SolvePartTwoTests()
    {
        var puzzle = new AdventOfCode2023.Day5.Day5();
        await puzzle.Initialize();
        await puzzle.SolvePartTwo();
    }

    [Fact]
    public async Task SolveReverse()
    {
        var puzzle = new AdventOfCode2023.Day5.Day5();
        await puzzle.Initialize();
        var result = puzzle.SolveReverse();
        result.Should().Be(35);
    }
}