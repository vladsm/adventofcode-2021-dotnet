namespace AdventOfCode.Year2021.Solvers.Day2;

public sealed class Day02Level2Solver : IAsyncSolver<(int, int), int>
{
	public async ValueTask<int> Solve(IAsyncEnumerable<(int, int)> entries)
	{
		var (hPos, depth, _) = await entries.AggregateAsync((0, 0, 0), aggregate);
		return hPos * depth;

		(int, int, int) aggregate((int, int, int) currentState, (int, int) nextCommand)
		{
			var (currentHPos, currentDepth, currentAim) = currentState;
			var (commandX, commandY) = nextCommand;
			return (currentHPos + commandX, currentDepth + currentAim * commandX, currentAim + commandY);
		}
	}
}
