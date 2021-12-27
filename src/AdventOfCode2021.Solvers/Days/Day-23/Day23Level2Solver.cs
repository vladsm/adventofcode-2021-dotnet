namespace AdventOfCode.Year2021.Solvers.Day23;

public sealed class Day23Level2Solver : SolverWithArrayInput<IInputEntry, int>
{
	protected override int Solve(IInputEntry[] entries)
	{
		var inputLocationsEntries = entries.OfType<LocationsEntry>().ToArray();
		var locationsEntries = new[]
		{
			inputLocationsEntries[0],
			new LocationsEntry(3, 2, 1, 0),
			new LocationsEntry(3, 1, 0, 2),
			inputLocationsEntries[1]
		};
		return new InnerSolver<LargeRoom>(locationsEntries).CalculateEnergy();
	}
}
