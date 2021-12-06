namespace AdventOfCode.Year2021.Solvers.Day4;

public static class Helpers
{
	public static IInputEntry ParseInputLine(string line, int index)
	{
		if (index == 0)
		{
			return new DrawnNumbers(parseNumbers(line, ','));
		}
		if (string.IsNullOrWhiteSpace(line))
		{
			return new Separator();
		}
		return new BoardLine(parseNumbers(line, ' '));

		static int[] parseNumbers(string lineToParse, char separator)
		{
			return lineToParse.
				Split(separator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).
				Select(str => Convert.ToInt32(str)).
				ToArray();
		}
	} 
}
