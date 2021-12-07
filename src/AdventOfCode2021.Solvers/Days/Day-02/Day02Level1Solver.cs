namespace AdventOfCode.Year2021.Solvers.Day2;

public sealed class Day02Level1Solver : IAsyncSolver<(int, int), int>
{
	public async ValueTask<int> Solve(IAsyncEnumerable<(int, int)> entries)
	{
		var (hPos, depth) = await entries.AggregateAsync((0, 0), aggregate);
		return hPos * depth;

		(int, int) aggregate((int, int) currentPosition, (int, int) nextCommand)
		{
			var (currentHPos, currentDepth) = currentPosition;
			var (commandX, commandY) = nextCommand;
			return (currentHPos + commandX, currentDepth + commandY);
		}
	}
}
