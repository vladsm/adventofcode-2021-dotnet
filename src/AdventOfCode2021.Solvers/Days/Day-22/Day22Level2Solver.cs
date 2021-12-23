namespace AdventOfCode.Year2021.Solvers.Day22;

public sealed class Day22Level2Solver : SolverWithArrayInput<Cuboid, ulong>
{
	protected override ulong Solve(Cuboid[] cuboids)
	{
		var innerSolver = new InnerSolver(cuboids);
		return innerSolver.CalculateOnCubes();
	}
}
