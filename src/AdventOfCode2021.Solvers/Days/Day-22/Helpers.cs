namespace AdventOfCode.Year2021.Solvers.Day22;

public static class Helpers
{
	public static Cuboid ParseInputLine(string line)
	{
		return line.StartsWith("on") ?
			parseRanges(3, true) :
			parseRanges(4, false);

		Cuboid parseRanges(int startIndex, bool on)
		{
			int[] ranges = line.
				Substring(startIndex).
				Split(new[] { "x=", "..", ",y=", ",z=" }, StringSplitOptions.RemoveEmptyEntries).
				Select(int.Parse).
				ToArray();
			return new Cuboid(
				on,
				new Range(ranges[0], ranges[1]),
				new Range(ranges[2], ranges[3]),
				new Range(ranges[4], ranges[5])
				);
		}
	}
}
