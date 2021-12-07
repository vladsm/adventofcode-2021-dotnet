namespace AdventOfCode.Year2021.Solvers.Day1;

public sealed class Day01Level2Solver : IAsyncSolver<int, int>
{
	public async ValueTask<int> Solve(IAsyncEnumerable<int> entries)
	{
		const int windowSize = 3;

		var (_, _, increasesQuantity) = await entries.AggregateAsync((new Queue<int>(), 0, 0), accumulate);
		return increasesQuantity;

		(Queue<int>, int, int increasesQuantity) accumulate(
			(Queue<int> window, int windowSum, int increasesQuantity) accumulator,
			int current
			)
		{
			int? removed = accumulator.window.Count >= windowSize ?
				accumulator.window.Dequeue() :
				null;

			int newWindowSum = accumulator.windowSum + current - (removed ?? 0);
			accumulator.window.Enqueue(current);

			int newIncreasesQuantity =
				removed.HasValue && accumulator.windowSum < newWindowSum ?
				accumulator.increasesQuantity + 1 :
				accumulator.increasesQuantity;

			return (accumulator.window, newWindowSum, newIncreasesQuantity);
		}
	}
}
