namespace AdventOfCode.Year2021.Solvers.Day19;

public interface IInputEntry {}

internal sealed record Separator : IInputEntry;

internal sealed record ScannerHeader(int Index) : IInputEntry;

internal sealed record Beacon(Position Position) : IInputEntry;
