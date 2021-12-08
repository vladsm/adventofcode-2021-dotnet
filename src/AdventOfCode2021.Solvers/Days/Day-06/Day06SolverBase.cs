namespace AdventOfCode.Year2021.Solvers.Day6;

public abstract class Day06SolverBase : SolverWithArrayInput<byte[], long>
{
	protected abstract int NumberOfDaysToCalculateAfter { get; }
	
	protected override long Solve(byte[][] input)
	{
		const int cycleLength = 7;
		const int daysToMature = 2;
		
		if (input.Length == 0) throw new ArgumentException("No ages in the input", nameof(input));

		long[] numberToSpawnAtDay = input[0].
			GroupBy(timer => timer % cycleLength, (day, g) => (day, quantity: (long)g.Count())).
			Concat(Enumerable.Range(0, cycleLength).Select(d => (day: d, quantity: 0L))).
			GroupBy(e => e.day, (day, g) => (day, quantity: g.Sum(e => e.quantity))).
			OrderBy(e => e.day).
			Select(e => e.quantity).
			ToArray();
		long[] numberToMatureAtDay = Enumerable.Repeat(0L, daysToMature).ToArray();

		for (int currentDay = 0; currentDay < NumberOfDaysToCalculateAfter; ++currentDay)
		{
			int cycleDay = currentDay % cycleLength;
			long spawnNumber = numberToSpawnAtDay[cycleDay];
			
			int matureCycleDay = currentDay % daysToMature;
			long maturedNumber = numberToMatureAtDay[matureCycleDay];
			numberToMatureAtDay[matureCycleDay] = 0;
			
			numberToSpawnAtDay[cycleDay] += maturedNumber;
			numberToMatureAtDay[matureCycleDay] = spawnNumber;
		}

		return numberToMatureAtDay.Sum() + numberToSpawnAtDay.Sum();
	}
}
