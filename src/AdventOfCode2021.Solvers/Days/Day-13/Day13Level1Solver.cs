namespace AdventOfCode.Year2021.Solvers.Day13;

public sealed class Day13Level1Solver : Day13SolverBase<int>
{
	protected override int Solve(HashSet<Dot> dots, FoldEntryBase[] folds)
	{
		return Fold(dots, folds.First()).Count;
	}
}
