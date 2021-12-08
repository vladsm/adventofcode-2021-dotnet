namespace AdventOfCode.Year2021.Solvers.Day6;

public static class Helpers
{
	public static byte[] ParseInitialTimers(string line)
	{
		return line.Split(',').Select(byte.Parse).ToArray();
	}
}
