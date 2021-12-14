namespace AdventOfCode.Year2021.Solvers.Day14;

public static class Helpers
{
	public static IInputEntry ParseLine(string line, int index)
	{
		if (index == 0) return new TemplateEntry(line.ToArray());
		if (index == 1) return new SeparatorEntry();

		string[] parts = line.Split("->", StringSplitOptions.TrimEntries);
		return new RuleEntry(parts[0][0], parts[0][1], parts[1][0]);
	}
}
