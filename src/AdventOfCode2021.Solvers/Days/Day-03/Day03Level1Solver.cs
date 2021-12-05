namespace AdventOfCode.Year2021.Solvers;

public sealed class Day03Level1Solver : IAsyncSolver<bool[], int>
{
	public async ValueTask<int> Solve(IAsyncEnumerable<bool[]> entries)
	{
		const int entrySize = 12;

		bool[] gammaRate = (await entries.AggregateAsync(new int[entrySize], aggregate)).
			Select(bitsCount => bitsCount > 0).
			ToArray();
		bool[] epsilonRate = gammaRate.Select(bit => !bit).ToArray();
		return toNumber(gammaRate) * toNumber(epsilonRate);

		int[] aggregate(int[] accumulator, bool[] value)
		{
			return value.
				Select(b => b ? 1 : -1).
				Zip(accumulator).
				Select(e => e.First + e.Second).
				ToArray();
		}

		int toNumber(bool[] bits)
		{
			return bits.
				Reverse().
				Select((bit, index) => bit ? (1 << index) : 0).
				Sum();
		}
	}
}
