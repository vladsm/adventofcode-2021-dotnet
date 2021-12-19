namespace AdventOfCode.Year2021.Solvers.Day19;

internal record struct Position(int X, int Y, int Z)
{
	public static Position Zero { get; } = new();
}
