namespace AdventOfCode.Year2021.Solvers.Day4;

public sealed class Day04Level2Solver : Day04SolverBase
{
	public override async ValueTask<int> Solve(IAsyncEnumerable<IInputEntry> entries)
	{
		var (drawnNumbers, boards) = await ParseEntries(entries);

		(Board, int)? lastWinner = null;
		var alreadyWonBoards = new HashSet<Board>(boards.Length);

		foreach (int number in drawnNumbers)
		foreach (Board board in boards)
		{
			if (alreadyWonBoards.Contains(board)) continue;
			
			bool win = board.AcceptNumberAndCheckWin(number);
			if (!win) continue;

			lastWinner = (board, number);
			alreadyWonBoards.Add(board);
		}
		
		if (lastWinner is null)
		{
			throw new InvalidOperationException("Drawn numbers are completed but there is still no winning board");
		}

		var (winnerBoard, winnerNumber) = lastWinner.Value;
		return winnerNumber * winnerBoard.CalculateNotMarkedNumbersSum();
	}
}
