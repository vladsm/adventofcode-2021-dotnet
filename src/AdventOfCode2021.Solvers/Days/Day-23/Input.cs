namespace AdventOfCode.Year2021.Solvers.Day23;

public interface IInputEntry {}

internal sealed record SkipEntry : IInputEntry;

internal sealed record LocationsEntry(
	byte FirstRoom,
	byte SecondRoom,
	byte ThirdRoom,
	byte FourthRoom
	) : IInputEntry;
