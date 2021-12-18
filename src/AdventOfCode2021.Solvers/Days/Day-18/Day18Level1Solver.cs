namespace AdventOfCode.Year2021.Solvers.Day18;

public sealed class Day18Level1Solver : Day18SolverBase
{
	public override async ValueTask<ulong> Solve(IAsyncEnumerable<SnailFishNumber> numbers)
	{
		var sum = await numbers.AggregateAsync(new SnailFishNumber(), Add);
		return CalculateMagnitude(sum);
	}
}
