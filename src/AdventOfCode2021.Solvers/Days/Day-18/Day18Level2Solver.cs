namespace AdventOfCode.Year2021.Solvers.Day18;

public sealed class Day18Level2Solver : Day18SolverBase
{
	public override async ValueTask<ulong> Solve(IAsyncEnumerable<SnailFishNumber> numbersEnumerable)
	{
		SnailFishNumber[] numbers = await numbersEnumerable.ToArrayAsync();

		ulong max = ulong.MinValue;
		for (int i = 0; i < numbers.Length; ++i)
		for (int j = 0; j < numbers.Length; ++j)
		{
			if (i == j) continue;

			ulong test = CalculateMagnitude(Add(numbers[i], numbers[j]));
			if (test > max) max = test;
			test = CalculateMagnitude(Add(numbers[j], numbers[i]));
			if (test > max) max = test;
		}

		return max;
	}
}
