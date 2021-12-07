namespace AdventOfCode.Year2021.Solvers.Day5;

public static class Helpers
{
	public static FieldLine ParseFieldLine(string line)
	{
		Position[] positions = line.
			Split("->", StringSplitOptions.TrimEntries).
			Select(parsePosition).
			ToArray();
		if (positions.Length < 2)
		{
			throw new ArgumentException($"Can not parse field line from '{line}'", nameof(line));
		}
		return new FieldLine(positions[0], positions[1]);

		static Position parsePosition(string text)
		{
			var coordinates = text.Split(',').Select(int.Parse).ToArray();
			if (coordinates.Length < 2)
			{
				throw new ArgumentException($"Can not parse position from '{text}'", nameof(text));
			}
			return new Position(coordinates[0], coordinates[1]);
		}
	}
}
