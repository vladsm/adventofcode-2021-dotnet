namespace AdventOfCode.Year2021.Solvers.Day9;

public static class Helpers
{
	public static byte[] ParseLine(string line)
	{
		return line.Select(ch => byte.Parse(ch.ToString())).ToArray();
	}
}
