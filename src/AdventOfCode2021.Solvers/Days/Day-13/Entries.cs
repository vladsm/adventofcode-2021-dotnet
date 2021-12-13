namespace AdventOfCode.Year2021.Solvers.Day13;

public interface IInputEntry {}

public sealed record DotEntry(
	int X,
	int Y
	) : IInputEntry;

public sealed record Separator : IInputEntry;

public abstract record FoldEntryBase(
	int Position
	) : IInputEntry;

public sealed record HorizontalFoldEntry(
	int Position
	) : FoldEntryBase(Position);

public sealed record VerticalFoldEntry(
	int Position
	) : FoldEntryBase(Position); 
