namespace AdventOfCode.Year2021.Solvers.Day11;

public abstract class Day11SolverBase : SolverWithArrayInput<byte[], int>
{
	protected static int SimulateStep(byte[,] cells)
	{
		ForEach(cells, (i, j) => cells[i, j] = (byte)(cells[i, j] + 1));

		int totalFlashes = 0;
		int flashes;
		do
		{
			flashes = Sum(cells, processFlash);
			totalFlashes += flashes;
		} while (flashes > 0);
		return totalFlashes;
		
		int processFlash(int x, int y)
		{
			if (cells[x, y] < 10) return 0;
			
			cells[x, y] = 0;
			ForAdjacent(cells, x, y, increase);
			return 1;

			void increase(int i, int j)
			{
				byte value = cells[i, j];
				if (value == 0) return;
				cells[i, j] = (byte)(value + 1);
			}
		}
	}

	private static void ForAdjacent(byte[,] cells, int x, int y, Action<int, int> action)
	{
		int height = cells.GetLength(0);
		int width = cells.GetLength(1);

		if (x > 0)
		{
			action(x - 1, y);
			if (y > 0) action(x - 1, y - 1);
			if (y < width - 1) action(x - 1, y + 1);
		}
		if (x < height - 1)
		{
			action(x + 1, y);
			if (y > 0) action(x + 1, y - 1);
			if (y < width - 1) action(x + 1, y + 1);
		}
		if (y > 0) action(x, y - 1);
		if (y < width - 1) action(x, y + 1);
	}

	private static int Sum(byte[,] cells, Func<int, int, int> func)
	{
		int sum = 0;
		ForEach(cells, (i, j) => sum += func(i, j));
		return sum;
	}

	private static void ForEach(byte[,] cells, Action<int, int> action)
	{
		ForEach(cells.GetLength(0), cells.GetLength(1), action);
	}

	private static void ForEach(int width, int height, Action<int, int> action)
	{
		for (int i = 0; i < height; ++i)
		for (int j = 0; j < width; ++j)
		{
			action(i, j);
		}
	}

	protected static byte[,] CreateField(byte[][] input)
	{
		int height = input.Length;
		if (height == 0) throw new ArgumentException("Input is empty");
		int width = input[0].Length;

		var cells = new byte[width, height];
		ForEach(width, height, (i, j) => cells[i, j] = input[i][j]);
		
		return cells;
	}
}
