namespace AdventOfCode.Year2021.Solvers.Day17;

public sealed record Target(
	(int from, int to) XRange,
	(int from, int to) YRange
	);
