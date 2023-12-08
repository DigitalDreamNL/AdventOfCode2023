using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day8;

public class Day8 : Puzzle
{
    protected override int Day => 8;

    private readonly char[] _instructions = ParseInstructions("LRLRLRRLRRLRLRRRLRRLRLRRLLLRRRLRRRLLRRLRRRLRRRLRRLRRLRRRLRRLRLRLRRRLRRLRRLLRRRLLRLRRRLRRRLRRLRRRLRRLLRLLRRRLRRLRLRRRLRLRLRRRLLRLRRLLRRRLRRRLRLRRLLRRLRLRRLRRRLRLLRRRLRRRLLRLLRLLRRRLRLRLRLRLRRLRRLRLRRRLLLRLLRRRLRRLLRLRLRRRLLRLRRRLLRRLRRLRLRLRLRRLRRLRRRLRRRLRRLRRLRRRLRLRRRLRLRRRR");
    private readonly List<Node> _nodes;

    public Day8()
    {
        var lines = File.ReadLines("Input/Day8.txt").ToList();
        _nodes = ParseMap(lines);
    }

    public override async Task Solve()
    {
        AdventOfCodeConsole.WritePuzzleHeader(Day);
        await SolvePartOne();
        await SolvePartTwo();
    }

    public override Task SolvePartOne()
    {
        var answer = CalculateSteps(_nodes, _instructions);
        AdventOfCodeConsole.WritePuzzlePartOneAnswer();
        AdventOfCodeConsole.WriteAnswer(answer);

        return Task.CompletedTask;
    }

    public override Task SolvePartTwo()
    {
        var answer = CalculateGhostSteps(_nodes, _instructions);

        AdventOfCodeConsole.WritePuzzlePartTwoAnswer();
        AdventOfCodeConsole.WriteAnswer(answer);

        return Task.CompletedTask;
    }
    
    public static int CalculateSteps(List<Node> nodes, char[] instructions)
    {
        var steps = 0;
        var instructionIndex = 0;
        var currentNode = nodes.Single(n => n.Name == "AAA");
        while (currentNode.Name != "ZZZ")
        {
            var instruction = instructions[instructionIndex % instructions.Length];
            var nextNode = instruction == 'L' ? currentNode.LeftNode : currentNode.RightNode;
            currentNode = nextNode;
            steps++;
            instructionIndex++;
        }

        return steps;
    }

    public static int CalculateGhostSteps(List<Node> nodes, char[] instructions)
    {
        var currentNode = nodes.Where(n => n.IsStartNode).ElementAt(0);
        var steps = 0;
        var instructionIndex = 0;
        while (!currentNode.IsEndNode)
        {
            var instruction = instructions[instructionIndex % instructions.Length];
            var nextNode = instruction == 'L' ? currentNode.LeftNode : currentNode.RightNode;
            currentNode = nextNode;
            steps++;
            instructionIndex++;
        }

        return steps;
    }
    
    public static List<Node> ParseMap(List<string> lines)
    {
        var pattern = @"(\w{3})";
        var nodes = new List<Node>();
        foreach (var line in lines)
        {
            var matches = Regex.Matches(line, pattern, RegexOptions.IgnoreCase);
            var node = new Node(matches[0].Value, matches[1].Value, matches[2].Value);
            nodes.Add(node);
        }

        foreach (var node in nodes)
        {
            node.LinkNodes(nodes);
        }
        return nodes;
    }

    public static char[] ParseInstructions(string line)
    {
        return line.ToCharArray();
    }
}

public class Node
{
    public Node(string name, string left, string right)
    {
        Name = name;
        Left = left;
        Right = right;
        IsStartNode = Name.Substring(2, 1) == "A";
        IsEndNode = Name.Substring(2, 1) == "Z";
    }

    public string Name { get; }
    private string Left { get; }
    private string Right { get; }
    public Node? LeftNode { get; set; }
    public Node? RightNode { get; set; }
    public bool IsStartNode { get; }
    public bool IsEndNode { get; }

    public void LinkNodes(List<Node> nodes)
    {
        LeftNode = Name != Left ? nodes.Single(n => n.Name == Left) : null;
        RightNode = Name != Right ? nodes.Single(n => n.Name == Right) : null;
    }
}