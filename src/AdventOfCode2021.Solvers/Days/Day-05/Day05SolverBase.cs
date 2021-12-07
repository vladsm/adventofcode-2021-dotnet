namespace AdventOfCode.Year2021.Solvers.Day5;

public abstract class Day05SolverBase : IAsyncSolver<FieldLine, int>
{
	public async ValueTask<int> Solve(IAsyncEnumerable<FieldLine> lines)
	{
		return await lines.
			SelectMany(Split).
			GroupBy(p => p, async (_, g) => await g.CountAsync()).
			WhereAwait(async count => await count > 1).
			CountAsync();
	}

	protected abstract IAsyncEnumerable<Position> Split(FieldLine line);
}
