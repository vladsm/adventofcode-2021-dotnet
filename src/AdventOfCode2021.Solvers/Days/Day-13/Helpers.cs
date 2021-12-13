namespace AdventOfCode.Year2021.Solvers.Day13;

public static class Helpers
{
	public static IInputEntry ParseLine(string line)
	{
		if (line.Length == 0) return new Separator();
		if (char.IsDigit(line[0])) return ParseDot(line);
		if (line.StartsWith("fold along ")) return ParseFold(line);
		if (line.Trim().Length == 0) return new Separator();

		throw new NotSupportedException($"Line '{line}' format is not supported");
	}


	private static DotEntry ParseDot(string line)
	{
		string[] parts = line.Split(',');
		return new DotEntry(int.Parse(parts[0]), int.Parse(parts[1]));
	}
	
	private static FoldEntryBase ParseFold(string line)
	{
		string[] parts = line.Substring(11).Split('=');
		int position = int.Parse(parts[1]);
		return parts[0] switch
		{
			"x" => new VerticalFoldEntry(position),
			"y" => new HorizontalFoldEntry(position),
			_ => throw new NotSupportedException($"Line '{line}' format is not supported")
		};
	}
}
