namespace AdventOfCode.Year2021.Solvers.Day4;

public interface IInputEntry {}

internal sealed record DrawnNumbers(int[] Numbers) : IInputEntry;

internal sealed record Separator : IInputEntry;

internal sealed record BoardLine(int[] Numbers) : IInputEntry;
