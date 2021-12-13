namespace AdventOfCode.Year2021.Solvers.Day13;

public sealed class Day13Level2Solver : Day13SolverBase<Dot[]>
{
	protected override Dot[] Solve(HashSet<Dot> dots, FoldEntryBase[] folds)
	{
		return folds.
			Aggregate(dots, Fold).
			ToArray();
	}

	public static IEnumerable<string> ToOutputLines(Dot[] dots)
	{
		Dictionary<int, HashSet<int>> lines = dots.
			GroupBy(d => d.Y).
			ToDictionary(g => g.Key, g => g.Select(d => d.X).ToHashSet());
		int minY = lines.Keys.Min();
		int maxY = lines.Keys.Max();
		int minX = lines.Values.SelectMany(x => x).Min();
		int maxX = lines.Values.SelectMany(x => x).Max();
		
		return Enumerable.Range(minY, maxY - minY + 1).Select(toOutputLine);

		string toOutputLine(int y)
		{
			if (!lines.TryGetValue(y, out HashSet<int>? lineDots)) return string.Empty;
			var characters = Enumerable.
				Range(minX, maxX - minX + 1).
				Select(x => lineDots.Contains(x) ? 'x' : ' ').
				ToArray();
			return new string(characters);
		}
	}
}
