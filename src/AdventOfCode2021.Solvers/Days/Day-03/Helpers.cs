namespace AdventOfCode.Year2021.Solvers.Day3;

public static class Helpers
{
	public static bool[] ParseInputLine(string line)
	{
		return line.Select(ch => ch == '1').ToArray();
	}
}
