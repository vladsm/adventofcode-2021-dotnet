namespace AdventOfCode.Year2021.Solvers.Day16;

public sealed class Day16Level2Solver : Day16SolverBase
{
	protected override ulong Solve(char[][] entries)
	{
		return ParsePacket(entries).CalculateValue();
	}
}
