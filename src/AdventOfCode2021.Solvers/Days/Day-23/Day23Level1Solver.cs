namespace AdventOfCode.Year2021.Solvers.Day23;

public sealed class Day23Level1Solver : SolverWithArrayInput<IInputEntry, int>
{
	protected override int Solve(IInputEntry[] entries)
	{
		var locationsEntries = entries.OfType<LocationsEntry>().ToArray();
		return new InnerSolver<SmallRoom>(locationsEntries).CalculateEnergy();
	}
}
