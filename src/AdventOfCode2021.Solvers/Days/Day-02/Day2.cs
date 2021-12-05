namespace AdventOfCode.Year2021.Solvers;

public static class Day2
{
	public static (int, int) ParseInputLine(string line)
	{
		string[] parts = line.Split(' ');
		int value = Convert.ToInt32(parts[1]);
		return parts[0] switch
		{
			"forward" => (value, 0),
			"up" => (0, -value),
			"down" => (0, value),
			_ => throw new InvalidOperationException($"Can not parse line '{line}'")
		};
	}
}
