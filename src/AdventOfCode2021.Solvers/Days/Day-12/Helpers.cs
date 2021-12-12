namespace AdventOfCode.Year2021.Solvers.Day12;

public static class Helpers
{
	public static (string, string) ParseLine(string line)
	{
		string[] parts = line.Split('-');
		return (parts[0], parts[1]);
	}
}
