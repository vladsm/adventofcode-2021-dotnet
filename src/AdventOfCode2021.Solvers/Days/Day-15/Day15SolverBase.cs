namespace AdventOfCode.Year2021.Solvers.Day15;

public abstract class Day15SolverBase : SolverWithArrayInput<byte[], int>
{
	protected override int Solve(byte[][] costs)
	{
		return new InnerSolver(costs, Cost, DimensionsExtendMultiplier).Solve();
	}

	protected abstract int DimensionsExtendMultiplier { get; }
	protected abstract int Cost((int row, int col) location, byte[][] costs);


	private class InnerSolver
	{
		private readonly int _dimensionsExtendMultiplier;
		private readonly Func<(int row, int col), int> _getCost;
		private readonly int _height;
		private readonly int _width;

		public InnerSolver(
			byte[][] costs,
			Func<(int row, int col), byte[][], int> getCost,
			int dimensionsExtendMultiplier
			)
		{
			_height = costs.Length;
			_width = costs[0].Length;
			_getCost = l => getCost(l, costs);
			_dimensionsExtendMultiplier = dimensionsExtendMultiplier;
		}

		public int Solve()
		{
			var startLocation = (0, 0);
			var extension = new HashSet<(int row, int col)> { startLocation };
			var reached = new Dictionary<(int, int), int> { { startLocation, 0 } };

			while (extension.Any())
			{
				extension = Extend(extension, reached);
			}

			var endLocation = (_height * _dimensionsExtendMultiplier - 1, _width * _dimensionsExtendMultiplier - 1);
			return reached[endLocation];
		}

		private HashSet<(int, int)> Extend(
			HashSet<(int, int)> prevExtension,
			Dictionary<(int, int), int> reached
			)
		{
			var extension = new HashSet<(int, int)>();
			foreach (var location in prevExtension)
			{
				int locationTotalCost = reached[location];
				ForAdjacent(location, a => processAdjacent(a, locationTotalCost));
			}
			return extension;

			void processAdjacent((int row, int col) adjacent, int locationTotalCost)
			{
				int newAdjacentTotalCost = _getCost(adjacent) + locationTotalCost;
				if (reached.TryGetValue(adjacent, out int adjacentTotalCost))
				{
					if (newAdjacentTotalCost >= adjacentTotalCost) return;
					reached[adjacent] = newAdjacentTotalCost;
				}
				else
				{
					reached.Add(adjacent, newAdjacentTotalCost);
				}
				extension.Add(adjacent);
			}
		}
		
		private void ForAdjacent((int, int) location, Action<(int, int)> action)
		{
			int height = _height * _dimensionsExtendMultiplier;
			int width = _width * _dimensionsExtendMultiplier;

			(int row, int col) = location;
			if (row > 0) action((row - 1, col));
			if (row < height - 1) action((row + 1, col));
			if (col > 0) action((row, col - 1));
			if (col < width - 1) action((row, col + 1));
		}
	}
}
