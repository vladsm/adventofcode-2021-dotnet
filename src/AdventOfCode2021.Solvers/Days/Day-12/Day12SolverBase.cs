namespace AdventOfCode.Year2021.Solvers.Day12;

using Transitions = Dictionary<string, List<string>>;
using Paths = List<List<string>>;

public abstract class Day12SolverBase : SolverWithArrayInput<(string, string), int>
{
	protected const string StartNode = "start";
	protected const string EndNode = "end";
	
	protected Paths GetPathsFrom(string from, string? twiceAllowed, Transitions availableTransitions)
	{
		if (!availableTransitions.TryGetValue(from, out var transitions) || !transitions.Any() || from == EndNode)
		{
			return new Paths { new() {from} };
		}
		
		Transitions newAvailableTransitions;
		if (char.IsLower(from[0]))
		{
			if (twiceAllowed == from)
			{
				twiceAllowed = null;
				newAvailableTransitions = availableTransitions;
			}
			else
			{
				newAvailableTransitions = CloneWithout(availableTransitions, from);
			}
		}
		else
		{
			newAvailableTransitions = availableTransitions;
		}
		return transitions.SelectMany(getPaths).ToList();
		
		Paths getPaths(string next)
		{
			var paths = GetPathsFrom(next, twiceAllowed, newAvailableTransitions);
			foreach (var path in paths)
			{
				path.Add(from);
			}
			return paths;
		}
	}

	private static Transitions CloneWithout(Transitions transitions, string node)
	{
		return transitions.
			Where(kvp => kvp.Key != node).
			ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Where(n => n != node).ToList());
	}
}
