namespace AdventOfCode.Year2021.Solvers.Day9;

public sealed class Day09Level1Solver : SolverWithArrayInput<byte[], int>
{
	const byte Highest = 9;

	protected override int Solve(byte[][] inputEntries)
	{
		if (inputEntries is null) throw new ArgumentNullException(nameof(inputEntries));
		if (!inputEntries.Any()) throw new ArgumentException("Input is empty", nameof(inputEntries));
		
		int entryLength = inputEntries.First().Length;

		var emptyEntry = Enumerable.Repeat(Highest, entryLength + 2).ToArray();
		var beforeBegin = Enumerable.Repeat(emptyEntry, 1);
		var afterEnd = Enumerable.Repeat(emptyEntry, 1);
		var backEntries = beforeBegin.Concat(inputEntries.Select(Extend)).Concat(afterEnd).ToArray();

		var entries = backEntries.Skip(1).ToArray();
		var forwardEntries = entries.Skip(1);

		return entries.
			Zip(backEntries).
			Zip(forwardEntries).
			Select(e => (current: e.First.First, previous: e.First.Second, next: e.Second)).
			Sum(CalculateRisk);
	}


	private static byte[] Extend(byte[] source)
	{
		return new[] { Highest }.Concat(source).Concat(new[] { Highest }).ToArray();
	}
	
	private static int CalculateRisk((byte[], byte[], byte[]) entry)
	{
		var (current, previous, next) = entry;
		int risk = 0;
		for (int i = 1; i < current.Length - 1; ++i)
		{
			byte location = current[i];
			bool isLow =
				location < current[i - 1] &&
				location < current[i + 1] &&
				location < previous[i] &&
				location < next[i];
			risk += (isLow ? location + 1 : 0);
		}
		return risk;
	}
}
