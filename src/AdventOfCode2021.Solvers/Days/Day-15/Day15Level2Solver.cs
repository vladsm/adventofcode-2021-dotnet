namespace AdventOfCode.Year2021.Solvers.Day15;

public sealed class Day15Level2Solver : Day15SolverBase
{
	protected override int DimensionsExtendMultiplier => 5;

	protected override int Cost((int, int) location, byte[][] costs)
	{
		(int col, int row) = location;
		int height = costs.Length;
		int width = costs[0].Length;
		int baseCost = costs[row % height][col % width];
		int increment = row / height + col / width;
		int cost = baseCost + increment;
		return (cost - 1) % 9 + 1;
	}
}
