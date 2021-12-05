namespace AdventOfCode.Year2021.Solvers;

public static class Day3
{
	public static bool[] ParseInputLine(string line)
	{
		return line.Select(ch => ch == '1').ToArray();
	}
}
