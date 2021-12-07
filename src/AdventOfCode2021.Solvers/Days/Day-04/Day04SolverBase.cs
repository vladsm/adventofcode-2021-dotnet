using AdventOfCode.Year2021.Solvers.Day4;

namespace AdventOfCode.Year2021.Solvers;

public abstract class Day04SolverBase : IAsyncSolver<IInputEntry, int>
{
	public abstract ValueTask<int> Solve(IAsyncEnumerable<IInputEntry> entries);

	internal static async ValueTask<(int[], Board[])> ParseEntries(IAsyncEnumerable<IInputEntry> entries)
	{
		int[]? drawnNumbers = null;
		var boards = new List<Board>();

		List<int[]>? currentBoardLines = null; 
		await foreach (IInputEntry entry in entries)
		{
			switch (entry)
			{
				case DrawnNumbers drawnNumbersEntry:
					drawnNumbers = drawnNumbersEntry.Numbers;
					break;
				case BoardLine boardLineEntry:
					currentBoardLines ??= new List<int[]>();
					currentBoardLines.Add(boardLineEntry.Numbers);
					break;
				case Separator:
					if (currentBoardLines is not null)
					{
						boards.Add(new Board(currentBoardLines.ToArray()));
					}
					currentBoardLines = new List<int[]>();
					break;
			}
		}

		if (drawnNumbers is null) throw new InvalidOperationException("No drawn numbers in the input");
		if (!boards.Any()) throw new InvalidOperationException("No boards in the input");

		return (drawnNumbers, boards.ToArray());
	}
}
