namespace AdventOfCode.Year2021.Solvers;

public abstract class Day03SolverBase : IAsyncSolver<bool[], int>
{
	public abstract ValueTask<int> Solve(IAsyncEnumerable<bool[]> entries);
	
	protected static int ToNumber(bool[] bits)
	{
		return bits.
			Reverse().
			Select((bit, index) => bit ? (1 << index) : 0).
			Sum();
	}
}
