namespace AdventOfCode.Year2021.Solvers.Day11;

public sealed class Day11Level2Solver : Day11SolverBase
{
	protected override int Solve(byte[][] input)
	{
		byte[,] cells = CreateField(input);
		int numberOfCells = cells.GetLength(0) * cells.GetLength(1);
		return Enumerable.
			Repeat(false, int.MaxValue).
			Select((_, step) => (step: step + 1, flashes: SimulateStep(cells))).
			First(e => e.flashes == numberOfCells).
			step;
	}
}
