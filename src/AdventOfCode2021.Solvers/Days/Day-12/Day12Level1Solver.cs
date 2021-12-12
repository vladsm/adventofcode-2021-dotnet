namespace AdventOfCode.Year2021.Solvers.Day12;

using Transitions = Dictionary<string, List<string>>;

public sealed class Day12Level1Solver : Day12SolverBase
{
	protected override int Solve((string, string)[] entries)
	{
		Transitions allTransitions = entries.
			Concat(entries.Select(e => (e.Item2, e.Item1))).
			GroupBy(e => e.Item1).
			ToDictionary(g => g.Key, g => g.Select(e => e.Item2).ToList());

		return GetPathsFrom("start", null, allTransitions).Count(p => p.First() == "end");
	}
}
