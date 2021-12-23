namespace AdventOfCode.Year2021.Solvers.Day22;

public sealed class Day22Level1Solver : SolverWithArrayInput<Cuboid, ulong>
{
	private const int Min = -50;
	private const int Max = 50;
	
	protected override ulong Solve(Cuboid[] cuboids)
	{
		cuboids = cuboids.
			Where(Filter).
			Select(ReplaceWithSmaller).
			ToArray();

		var innerSolver = new InnerSolver(cuboids);
		return innerSolver.CalculateOnCubes();
	}


	private static bool Filter(Cuboid cuboid)
	{
		var (_, (xFrom, xTo), (yFrom, yTo), (zFrom, zTo)) = cuboid;
		return
			xFrom <= Max && yFrom <= Max && zFrom <= Max &&
			xTo >= Min && yTo >= Min && zTo >= Min;
	}
	
	private static Cuboid ReplaceWithSmaller(Cuboid cuboid)
	{
		var (on, (xFrom, xTo), (yFrom, yTo), (zFrom, zTo)) = cuboid;
		if (xFrom < Min) xFrom = Min;
		if (yFrom < Min) yFrom = Min;
		if (zFrom < Min) zFrom = Min;
		if (xTo > Max) xTo = Max;
		if (yTo > Max) yTo = Max;
		if (zTo > Max) zTo = Max;

		return new Cuboid(on, new Range(xFrom, xTo), new Range(yFrom, yTo), new Range(zFrom, zTo));
	}
}
