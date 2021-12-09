namespace AdventOfCode.Year2021.Solvers.Day8;

public sealed class Day08Level1Solver : IAsyncSolver<string[], int>
{
	public async ValueTask<int> Solve(IAsyncEnumerable<string[]> entries)
	{
		return await entries.
			SelectMany(e => e.ToAsyncEnumerable()).
			Where(accepted).
			CountAsync();

		bool accepted(string segment)
		{
			return segment.Length switch
			{
				2 or 3 or 4 or 7 => true,
				_ => false
			};
		}
	}
}
