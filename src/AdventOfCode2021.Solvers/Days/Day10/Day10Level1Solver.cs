namespace AdventOfCode.Year2021.Solvers.Day10;

public sealed class Day10Level1Solver : IAsyncSolver<char[], int>
{
	public async ValueTask<int> Solve(IAsyncEnumerable<char[]> entries)
	{
		return await entries.AggregateAsync(0, aggregate);
		
		static int aggregate(int sum, char[] line)
		{
			var matchController = new Stack<char>();
			foreach (char character in line)
			{
				if (character is '(' or '[' or '{' or '<')
				{
					matchController.Push(character);
					continue;
				}

				if (!_closeMatches.TryGetValue(character, out var match)) continue;
				if (matchController.Pop() == match.open) continue;

				return sum + match.score;
			}
			return sum;
		}
	}

	private static readonly Dictionary<char, (char open, int score)> _closeMatches = new()
	{
		{')', ('(', 3)},
		{']', ('[', 57)},
		{'}', ('{', 1197)},
		{'>', ('<', 25137)}
	};
}
