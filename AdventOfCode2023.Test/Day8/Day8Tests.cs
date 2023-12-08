using AdventOfCode2023.Day8;
using FluentAssertions;

namespace AdventOfCode2023.Test.Day8;

public class Day8Tests
{
    private readonly List<string> _lines = new List<string>
    {
        "AAA = (BBB, BBB)",
        "BBB = (AAA, ZZZ)",
        "ZZZ = (ZZZ, ZZZ)"
    };
    
    private readonly string _instructions = "LLR";

    [Fact]
    public void ParseMapTests()
    {
        var nodes = AdventOfCode2023.Day8.Day8.ParseMap(_lines);

        var nodeA = new Node("AAA", "BBB", "BBB");
        var nodeB = new Node("BBB", "AAA", "ZZZ");
        var nodeZ = new Node("ZZZ", "ZZZ", "ZZZ");
        nodeA.LeftNode = nodeB;
        nodeA.RightNode = nodeB;
        nodeB.LeftNode = nodeA;
        nodeB.RightNode = nodeZ;
        var expectedNodes = new List<Node>
        {
            nodeA, nodeB, nodeZ
        };

        nodes.Should().BeEquivalentTo(expectedNodes, options =>
            options.Excluding(n => n.LeftNode).Excluding(n => n.RightNode));
    }

    [Fact]
    public void ParseInstructionsTests()
    {
        var instructions = AdventOfCode2023.Day8.Day8.ParseInstructions(_instructions);
        instructions.Should().BeEquivalentTo(new char[] { 'L', 'L', 'R' });
    }

    [Fact]
    public void CalculateStepsTests()
    {
        var nodes = AdventOfCode2023.Day8.Day8.ParseMap(_lines);
        var instructions = AdventOfCode2023.Day8.Day8.ParseInstructions(_instructions);
        var steps = AdventOfCode2023.Day8.Day8.CalculateSteps(nodes, instructions);

        steps.Should().Be(6);
    }
}