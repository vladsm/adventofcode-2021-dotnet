namespace AdventOfCode.Year2021.Solvers.Day3;

public sealed class Day03Level1Solver : Day03SolverBase
{
	public override async ValueTask<int> Solve(IAsyncEnumerable<bool[]> entries)
	{
		const int entrySize = 12;

		bool[] gammaRate = (await entries.AggregateAsync(new int[entrySize], aggregate)).
			Select(bitsCount => bitsCount > 0).
			ToArray();
		bool[] epsilonRate = gammaRate.Select(bit => !bit).ToArray();
		return ToNumber(gammaRate) * ToNumber(epsilonRate);

		int[] aggregate(int[] accumulator, bool[] value)
		{
			return value.
				Select(b => b ? 1 : -1).
				Zip(accumulator).
				Select(e => e.First + e.Second).
				ToArray();
		}
	}
}
