namespace AdventOfCode.Year2021.Solvers;

public static class Parsing
{
	public static int[] ParseToIntArray(string line)
	{
		return line.Split(',').Select(int.Parse).ToArray();
	}
}
