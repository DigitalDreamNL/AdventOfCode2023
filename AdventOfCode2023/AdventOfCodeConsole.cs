using Spectre.Console;

namespace AdventOfCode2023;

public static class AdventOfCodeConsole
{
    public static void WriteIntro()
    {
        AnsiConsole.MarkupLine(@"");
        AnsiConsole.MarkupLine(@"              _                 _    ____   __  _____          _     [green] ___   ___ ___  ____  [/]"); 
        AnsiConsole.MarkupLine(@"     /\      | |               | |  / __ \ / _|/ ____|        | |    [green]|__ \ / _ \__ \|___ \ [/]");
        AnsiConsole.MarkupLine(@"    /  \   __| |_   _____ _ __ | |_| |  | | |_| |     ___   __| | ___[green]   ) | | | | ) | __) |[/]"); 
        AnsiConsole.MarkupLine(@"   / /\ \ / _` \ \ / / _ \ '_ \| __| |  | |  _| |    / _ \ / _` |/ _ \[green] / /| | | |/ / |__ < [/]");
        AnsiConsole.MarkupLine(@"  / ____ \ (_| |\ V /  __/ | | | |_| |__| | | | |___| (_) | (_| |  __/[green]/ /_| |_| / /_ ___) |[/]"); 
        AnsiConsole.MarkupLine(@" /_/    \_\__,_| \_/ \___|_| |_|\__|\____/|_|  \_____\___/ \__,_|\___|[green]____|\___/____|____/ [/]");
        AnsiConsole.MarkupLine(@"");
    }

    public static void WritePuzzleHeader(int day)
    {
        AnsiConsole.MarkupLine($"[red]Solution for Day {day}[/]");
    }

    public static void WritePuzzlePartOneAnswer()
    {
        AnsiConsole.Markup($"Part 1: ");
    }

    public static void WritePuzzlePartTwoAnswer()
    {
        AnsiConsole.Markup($"Part 2: ");
    }

    public static void WriteAnswer(string answer)
    {
        WriteLine($"[green]{answer}[/]");
    }

    public static void WriteLine(string line)
    {
        AnsiConsole.MarkupLine(line);
    }
}