namespace AdventOfCode.Year2021.Solvers.Day20;

public interface IInputEntry {}

internal record EnhanceAlgorithm(bool[] Pixels) : IInputEntry;

internal record Separator : IInputEntry;

internal record ImageRow(bool[] Pixels) : IInputEntry;
