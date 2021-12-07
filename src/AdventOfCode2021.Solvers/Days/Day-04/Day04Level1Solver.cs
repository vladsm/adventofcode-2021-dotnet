using AdventOfCode.Year2021.Solvers.Day4;

namespace AdventOfCode.Year2021.Solvers;

public sealed class Day04Level1Solver : Day04SolverBase
{
	public override async ValueTask<int> Solve(IAsyncEnumerable<IInputEntry> entries)
	{
		var (drawnNumbers, boards) = await ParseEntries(entries);

		foreach (int number in drawnNumbers)
		foreach (Board board in boards)
		{
			bool win = board.AcceptNumberAndCheckWin(number);
			if (win) return number * board.CalculateNotMarkedNumbersSum();
		}
		
		throw new InvalidOperationException("Drawn numbers are completed but there is still no winning board");
	}
}
