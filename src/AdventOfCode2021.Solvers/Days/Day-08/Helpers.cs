namespace AdventOfCode.Year2021.Solvers.Day8;

public static class Helpers
{
	public static string[] ParseLevel1Input(string line)
	{
		return line.
			Split('|', StringSplitOptions.TrimEntries)[1].
			Split(' ', StringSplitOptions.TrimEntries);
	}

	public static (string[] samples, string[] output) ParseLevel2Input(string line)
	{
		string[] parts = line.Split('|', StringSplitOptions.TrimEntries);
		string[] samples = parts[0].Split(' ', StringSplitOptions.TrimEntries);
		string[] output = parts[1].Split(' ', StringSplitOptions.TrimEntries);
		return (samples, output);
	}
}
