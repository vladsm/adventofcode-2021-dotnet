namespace AdventOfCode.Year2021.Solvers;

public sealed class Day01Level1Solver : SolverWithEnumerableInput<int, int>
{
	protected override int Solve(IEnumerable<int> entries)
	{
		return entries.
			LookBackSelect(detectIncreased).
			Count(increased => increased);

		static bool detectIncreased(int current, int? previous)
		{
			return current > previous;
		}
	}
}
