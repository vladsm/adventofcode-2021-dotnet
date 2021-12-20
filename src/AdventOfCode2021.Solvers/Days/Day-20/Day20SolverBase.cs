namespace AdventOfCode.Year2021.Solvers.Day20;

public abstract class Day20SolverBase : IAsyncSolver<IInputEntry, int>
{
	public abstract int NumberOfEnhancements { get; } 
	
	public async ValueTask<int> Solve(IAsyncEnumerable<IInputEntry> entries)
	{
		var (image, enhanceAlgorithm) = await entries.ToImageAndEnhanceAlgorithm();
		var innerSolver = new InnerSolver(image, enhanceAlgorithm);
		return innerSolver.Solve(NumberOfEnhancements);
	}
}
