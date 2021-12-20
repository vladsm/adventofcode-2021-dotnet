namespace AdventOfCode.Year2021.Solvers.Day20;

internal sealed class InnerSolver
{
	private readonly HashSet<string> _enhanceAlgorithm;
	
	private readonly HashSet<(int col, int row)> _image;
	private readonly int _height;
	private readonly int _width;

	public InnerSolver(bool[][] image, bool[] enhanceAlgorithm)
	{
		_enhanceAlgorithm = enhanceAlgorithm.
			Select((pixel, index) => (pixel, index)).
			Where(e => e.pixel).
			Select(e => Convert.ToString(e.index, 2).PadLeft(9, '0')).
			ToHashSet();
		_height = image.Length;
		_width = image[0].Length;
		_image = image.
			SelectMany((row, rowIndex) => row.Select((pixel, colIndex) => (position: (colIndex, rowIndex), pixel))).
			Where(e => e.pixel).
			Select(e => e.position).
			ToHashSet();
	}

	public int Solve(int enhancementTimes)
	{
		var resultPixels = Enumerable.
			Repeat(false, enhancementTimes).
			Aggregate((image: _image, _width, _height, false), aggregate).
			image;
		return resultPixels.Count;

		(HashSet<(int, int)>, int, int, bool) aggregate((HashSet<(int, int)> pixels, int width, int height, bool defaultPixel) acc, bool _)
		{
			var newPixels = EnhanceImage(acc.width, acc.height, acc.defaultPixel, acc.pixels);
			return (newPixels, acc.width + 2, acc.height + 2, !acc.defaultPixel);
		}
	}
	
	public HashSet<(int, int)> EnhanceImage(int width, int height, bool defaultPixel, HashSet<(int, int)> imagePixels)
	{
		int extendedWidth = width + 2;
		int extendedHeight = height + 2;

		var result = new HashSet<(int, int)>();
		for (int i = 0; i < extendedWidth; ++i)
		for (int j = 0; j < extendedHeight; ++j)
		{
			string key = GetEnhancementKey(i, j, extendedWidth, extendedHeight, defaultPixel, imagePixels);
			if (_enhanceAlgorithm.Contains(key))
			{
				result.Add((i, j));
			}
		}
		return result;
	}

	private string GetEnhancementKey(
		int col,
		int row,
		int extendedWidth,
		int extendedHeight,
		bool defaultPixel,
		HashSet<(int, int)> pixels
		)
	{
		var indexes = new[]
		{
			(col - 1, row - 1),
			(col, row - 1),
			(col + 1, row - 1),
			(col - 1, row),
			(col, row),
			(col + 1, row),
			(col - 1, row + 1),
			(col, row + 1),
			(col + 1, row + 1)
		};
		return new string(indexes.Select(getPixel).ToArray());

		char getPixel((int col, int row) position)
		{
			if (
				position.col <= 0 ||
				position.col >= extendedWidth - 1 ||
				position.row <= 0 ||
				position.row >= extendedHeight - 1
				)
			{
				return defaultPixel ? '1' : '0';
			}

			return pixels.Contains((position.col - 1, position.row - 1)) ? '1' : '0';
		}
	} 
}
