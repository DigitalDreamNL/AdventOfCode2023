namespace AdventOfCode2023.Day5;

public class Day5 : Puzzle
{
    public List<long> Seeds = new();
    public List<long> RangedSeeds = new();
    public List<Tuple<long, long>> RangedSeedRanges = new ();
    public List<Map> Mappings = new ();

    protected override int Day => 5;
    
    public async Task Initialize()
    {
        var input = ReadInput();

        var currentMapType = new MapType();

        await foreach (var line in input)
        {
            var lineType = DetermineLineType(line);
            switch (lineType)
            {
                case LineType.Map:
                    currentMapType = ParseMapType(line);
                    break;
                case LineType.Seeds:
                    Seeds = ParseSeeds(line);
                    RangedSeeds = ParseRangedSeeds(line);
                    RangedSeedRanges = ParseRangedSeedRanges(line);
                    break;
                case LineType.Values:
                    Mappings.Add(ParseMap(line, currentMapType));
                    break;
                case LineType.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        Console.WriteLine("X");
    }
    
    public override async Task Solve()
    {
        AdventOfCodeConsole.WritePuzzleHeader(Day);
        await Initialize();
        await SolvePartOne();
        await SolvePartTwo();
    }

    public override Task SolvePartOne()
    {
        var locations = Seeds.Select(seed => MapSeedToLocation(seed, Mappings)).ToList();
        var minLocation = locations.MinBy(l => l);
        AdventOfCodeConsole.WritePuzzlePartOneAnswer();
        AdventOfCodeConsole.WriteAnswer(minLocation.ToString());
        return Task.CompletedTask;
    }

    public override Task SolvePartTwo()
    {
        var minLocation = long.MaxValue;
        for (var i = 0; i < RangedSeeds.Count; i++)
        {
            var location = MapSeedToLocation(RangedSeeds[i], Mappings);
            Console.WriteLine($"[{Math.Round((double)(i+1)/RangedSeeds.Count*100)}%] Mapped seed {i+1} of {RangedSeeds.Count} ({RangedSeeds[i]}) to location {location}");
            minLocation = Math.Min(minLocation, location);
        }
        AdventOfCodeConsole.WritePuzzlePartTwoAnswer();
        AdventOfCodeConsole.WriteAnswer(minLocation.ToString());
        return Task.CompletedTask;
    }
    
    public LineType DetermineLineType(string line)
    {
        if (line.Length == 0) return LineType.Empty;
        if (line.Contains("map")) return LineType.Map;
        if (line.Contains("seeds")) return LineType.Seeds;
        return LineType.Values;
    }

    public List<long> ParseSeeds(string line)
    {
        var strippedLine = line.Replace("seeds: ", "");
        var seedStrings = strippedLine.Split(' ');
        var seeds = seedStrings.Select(long.Parse);
        return seeds.ToList();
    }

    public List<long> ParseRangedSeeds(string line)
    {
        var seeds = new List<long>();
        var strippedLine = line.Replace("seeds: ", "");
        var seedStrings = strippedLine.Split(' ');
        for (var i = 0; i < seedStrings.Length / 2; i++)
        {
            var seedStart = long.Parse(seedStrings[i*2]);
            var length = long.Parse(seedStrings[i*2+1]);
            for (var x = seedStart; x < seedStart + length; x++)
            {
                seeds.Add(x);
            }
        }

        return seeds;
    }

    public List<Tuple<long,long>> ParseRangedSeedRanges(string line)
    {
        var seeds = new List<Tuple<long, long>>();
        var strippedLine = line.Replace("seeds: ", "");
        var seedStrings = strippedLine.Split(' ');
        for (var i = 0; i < seedStrings.Length / 2; i++)
        {
            var seedStart = long.Parse(seedStrings[i*2]);
            var length = long.Parse(seedStrings[i*2+1]);
            var seed = new Tuple<long, long>(seedStart, seedStart + length - 1);
            seeds.Add(seed);
        }

        return seeds;
    }

    public MapType ParseMapType(string line)
    {
        var strippedLine = line.Replace(" map:", "");
        var types = strippedLine.Split("-to-");
        return new MapType
        {
            Source = types[0],
            Destination = types[1]
        };
    }

    public Map ParseMap(string line, MapType mapType)
    {
        var values = line.Split(' ');
        var destinationStart = long.Parse(values[0]);
        var sourceStart = long.Parse(values[1]);
        var length = long.Parse(values[2]);
        return new Map
        {
            Destination = mapType.Destination,
            Source = mapType.Source,
            SourceStart = sourceStart,
            SourceEnd = sourceStart + length - 1,
            DestinationStart = destinationStart,
            DestinationEnd = destinationStart + length - 1
        };
    }

    public long MapValues(long needle, List<Map> mappings, string source, string destination)
    {
        var relevantMapping = mappings.SingleOrDefault(m => m.Source == source &&
                                                            m.Destination == destination &&
                                                            m.SourceStart <= needle &&
                                                            m.SourceEnd >= needle);
        return relevantMapping == null
            ? needle
            : needle - relevantMapping.SourceStart + relevantMapping.DestinationStart;
    }

    public long ReverseMapValues(long needle, List<Map> mappings, string source, string destination)
    {
        var relevantMapping = mappings.SingleOrDefault(m => m.Source == source &&
                                                            m.Destination == destination &&
                                                            m.DestinationStart <= needle &&
                                                            m.DestinationEnd >= needle);
        return relevantMapping == null
            ? needle
            : needle + relevantMapping.SourceStart - relevantMapping.DestinationStart;
    }

    public long MapSeedToLocation(long seed, List<Map> mappings)
    {
        var soil = MapValues(seed, mappings, "seed", "soil");
        var fertilizer = MapValues(soil, mappings, "soil", "fertilizer");
        var water = MapValues(fertilizer, mappings, "fertilizer", "water");
        var light = MapValues(water, mappings, "water", "light");
        var temperature = MapValues(light, mappings, "light", "temperature");
        var humidity = MapValues(temperature, mappings, "temperature", "humidity");
        var location = MapValues(humidity, mappings, "humidity", "location");
        return location;
    }

    public long MapLocationToSeed(long location, List<Map> mappings)
    {
        var humidity = ReverseMapValues(location, mappings, "humidity", "location");
        var temperature = ReverseMapValues(humidity, mappings, "temperature", "humidity");
        var light = ReverseMapValues(temperature, mappings, "light", "temperature");
        var water = ReverseMapValues(light, mappings, "water", "light");
        var fertilizer = ReverseMapValues(water, mappings, "fertilizer", "water");
        var soil = ReverseMapValues(fertilizer, mappings, "soil", "fertilizer");
        var seed = ReverseMapValues(soil, mappings, "seed", "soil");
        return seed;
    }

    public long SolveReverse()
    {
        var count = 0;
        Parallel.ForEach(Enumerable.Range(0, 177942185), (location, state) =>
        {
            count++;
            var seed = MapLocationToSeed(location, Mappings);
            if (count % 100000 == 0)
            {
                Console.WriteLine($"Mapped {count} locations...");
            }
            if (count % 1000000 == 0)
            {
                Console.WriteLine($"Mapped location {location} to seed {seed}");
            }

            if (RangedSeedRanges.SingleOrDefault(r => r.Item1 <= seed && r.Item2 >= seed) != null)
            {
                Console.WriteLine($"Seed exists! Return location: {location}");
                state.Break();
            }
        });
        for (long location = 0; location <= 177942185; location++)
        {
            var seed = MapLocationToSeed(location, Mappings);
            if (location % 10000 == 0)
            {
                Console.WriteLine($"Mapped location {location} to seed {seed}");
            }

            if (RangedSeedRanges.SingleOrDefault(r => r.Item1 <= seed && r.Item2 >= seed) != null)
            {
                Console.WriteLine($"Seed exists! Return location: {location}");
                return location;
            }
        }

        throw new Exception();
    }
}

public enum LineType
{
    Empty,
    Seeds,
    Map,
    Values,
}

public class MapType
{
    public string Source { get; init; } = string.Empty;
    public string Destination { get; init; } = string.Empty;
}

public class Map
{
    public string Source { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public long SourceStart { get; set; }
    public long SourceEnd { get; set; }
    public long DestinationStart { get; set; }
    public long DestinationEnd { get; set; }
}