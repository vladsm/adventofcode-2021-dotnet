namespace AdventOfCode.Year2021.Solvers.Day11;

public sealed class Day11Level1Solver : Day11SolverBase
{
	protected override int Solve(byte[][] input)
	{
		byte[,] cells = CreateField(input);
		return Enumerable.Repeat(false, 100).Select(_ => SimulateStep(cells)).Sum();
	}
}
