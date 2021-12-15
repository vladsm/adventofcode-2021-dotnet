namespace AdventOfCode.Year2021.Solvers.Day15;

public sealed class Day15Level1Solver : Day15SolverBase
{
	protected override int DimensionsExtendMultiplier => 1;

	protected override int Cost((int, int) location, byte[][] costs)
	{
		(int row, int col) = location;
		return costs[row][col];
	}
}
