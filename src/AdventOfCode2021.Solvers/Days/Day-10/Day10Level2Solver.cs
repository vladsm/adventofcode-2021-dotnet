namespace AdventOfCode.Year2021.Solvers.Day10;

public sealed class Day10Level2Solver : IAsyncSolver<char[], long>
{
	public async ValueTask<long> Solve(IAsyncEnumerable<char[]> entries)
	{
		long[] scores = await entries.
			Select(toCompleteScore).
			Where(score => score > 0).
			OrderBy(score => score).
			ToArrayAsync();
		return scores[scores.Length / 2];
		
		static long toCompleteScore(char[] line)
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

				return 0;
			}

			long score = 0;
			while (matchController.Any())
			{
				if (!_completePoints.TryGetValue(matchController.Pop(), out int points)) continue;
				score = score * 5 + points;
			}

			return score;
		}
	}

	private static readonly Dictionary<char, (char open, int score)> _closeMatches = new()
	{
		{')', ('(', 3)},
		{']', ('[', 57)},
		{'}', ('{', 1197)},
		{'>', ('<', 25137)}
	};
	
	private static readonly Dictionary<char, int> _completePoints = new()
	{
		{'(', 1},
		{'[', 2},
		{'{', 3},
		{'<', 4}
	};
}
