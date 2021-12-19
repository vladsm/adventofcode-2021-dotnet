namespace AdventOfCode.Year2021.Solvers.Day19;

public sealed class Day19Level2Solver : IAsyncSolver<IInputEntry, int>
{
	public async ValueTask<int> Solve(IAsyncEnumerable<IInputEntry> entries)
	{
		var innerSolver = new InnerSolver(await entries.ToScanners());
		return innerSolver.CalculateMaxDistanceBetweenScanners();
	}
}
