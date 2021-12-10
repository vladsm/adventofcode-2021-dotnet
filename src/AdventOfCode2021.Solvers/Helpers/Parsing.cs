namespace AdventOfCode.Year2021.Solvers;

public static class Parsing
{
	public static int[] ParseToIntArray(string line) =>
		line.Split(',').Select(int.Parse).ToArray();

	public static char[] ParseToCharactersArray(string line) =>
		line.ToArray();
}
