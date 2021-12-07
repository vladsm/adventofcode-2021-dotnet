namespace AdventOfCode.Year2021.Solvers.Day4;

internal sealed class Board
{
	private readonly Dictionary<int,Position> _numbersPositions;
	private readonly Cell[,] _cells;

	public Board(int[][] lines)
	{
		int boardHeight = lines.Length;
		if (boardHeight == 0) throw new ArgumentException("No board lines passed", nameof(lines));
		int boardWidth = lines[0].Length;
		
		_numbersPositions = new Dictionary<int, Position>(boardWidth);
		_cells = new Cell[boardHeight, boardWidth];

		for (byte rowIndex = 0; rowIndex < boardHeight; ++rowIndex)
		{
			int[] row = lines[rowIndex];
			for (byte columnIndex = 0; columnIndex < row.Length; ++columnIndex)
			{
				int number = row[columnIndex];
				_numbersPositions[number] = new Position(rowIndex, columnIndex);
				_cells[rowIndex, columnIndex] = new Cell(number, false);
			}
		}
	}

	public bool AcceptNumberAndCheckWin(int number)
	{
		if (!_numbersPositions.TryGetValue(number, out Position position)) return false;

		(byte row, byte column) = position;
		_cells[position.Row, position.Column] = new Cell(number, true);

		int boardWidth = _cells.GetLength(1);
		bool isWin = true;
		for (int i = 0; i < boardWidth; ++i)
		{
			if (_cells[row, i].Marked) continue;
			isWin = false;
			break;
		}
		if (isWin) return true;
		
		isWin = true;
		int boardHeight = _cells.GetLength(0);
		for (int i = 0; i < boardHeight; ++i)
		{
			if (_cells[i, column].Marked) continue;
			isWin = false;
			break;
		}
		return isWin;
	}

	public int CalculateNotMarkedNumbersSum()
	{
		int result = 0;
		foreach (Cell cell in _cells)
		{
			if (cell.Marked) continue;
			result += cell.Number;
		}
		return result;
	}

	private readonly record struct Position(byte Row, byte Column);
	
	private readonly record struct Cell(int Number, bool Marked);
}
