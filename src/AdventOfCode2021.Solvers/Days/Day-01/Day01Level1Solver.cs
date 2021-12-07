namespace AdventOfCode.Year2021.Solvers.Day1;

public sealed class Day01Level1Solver : SolverWithArrayInput<int, int>
{
	protected override int Solve(int[] entries)
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
