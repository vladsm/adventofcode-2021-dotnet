namespace AdventOfCode.Year2021.Solvers.Day20;

public static class Helpers
{
	public static IInputEntry ParseInputLine(string line, int index)
	{
		if (index == 0) return new EnhanceAlgorithm(parsePixelsRow(line));
		if (index == 1) return new Separator();
		return new ImageRow(parsePixelsRow(line));

		static bool[] parsePixelsRow(string rowLine)
		{
			return rowLine.Select(character => character == '#').ToArray();
		}
	}

	internal static async Task<(bool[][], bool[])> ToImageAndEnhanceAlgorithm(this IAsyncEnumerable<IInputEntry> entries)
	{
		var groups = await entries.GroupBy(toKey).OrderBy(g => g.Key).Where(g => g.Key < 2).ToArrayAsync();
		bool[] enhanceAlgorithm = await groups[0].OfType<EnhanceAlgorithm>().Select(a => a.Pixels).FirstAsync();
		bool[][] image = await groups[1].OfType<ImageRow>().Select(row => row.Pixels).ToArrayAsync();
		return (image, enhanceAlgorithm);

		int toKey(IInputEntry entry)
		{
			return entry switch
			{
				EnhanceAlgorithm => 0,
				ImageRow => 1,
				_ => 2
			};
		}
	}
}
