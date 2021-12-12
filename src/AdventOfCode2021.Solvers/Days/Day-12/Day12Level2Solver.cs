namespace AdventOfCode.Year2021.Solvers.Day12;

using Transitions = Dictionary<string, List<string>>;

public sealed class Day12Level2Solver : Day12SolverBase
{
	protected override int Solve((string, string)[] entries)
	{
		Transitions allTransitions = entries.
			Concat(entries.Select(e => (e.Item2, e.Item1))).
			GroupBy(e => e.Item1).
			ToDictionary(g => g.Key, g => g.Select(e => e.Item2).ToList());

		return allTransitions.
			Select(t => t.Key).
			Where(node => node != StartNode && node != EndNode && char.IsLower(node[0])).
			SelectMany(twiceAllowed => GetPathsFrom(StartNode, twiceAllowed, allTransitions)).
			Where(p => p.First() == EndNode).
			Select(ToString).
			Distinct().
			Count();
	}

	private string ToString(List<string> path) => string.Join("->", path);
}
