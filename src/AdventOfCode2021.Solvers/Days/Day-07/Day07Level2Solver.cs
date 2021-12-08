namespace AdventOfCode.Year2021.Solvers.Day7;

public sealed class Day07Level2Solver : SolverWithArrayInput<int[], int>
{
	protected override int Solve(int[][] input)
	{
		if (input.Length == 0) throw new ArgumentException("No input", nameof(input));

		return new PositionSolver(input[0]).Solve();
	}


	private sealed class PositionSolver
	{
		private readonly int[] _positions;

		public PositionSolver(int[] positions)
		{
			this._positions = positions;
		}

		public int Solve()
		{
			int minPosition = _positions.Min();
			int maxPosition = _positions.Max();
			int alignedPosition = SearchAlignedPosition(minPosition, maxPosition);
			return TotalFuelTo(alignedPosition);
		}

		private int SearchAlignedPosition(int from, int to)
		{
			if (from == to) return from;

			if (to - from == 1)
			{
				return TotalFuelTo(from) - TotalFuelTo(to) >= 0 ? to : from;
			}

			int middle = (to + from) / 2;

			int middleFuel = TotalFuelTo(middle);
			int preMiddleFuel = TotalFuelTo(middle - 1);
			int postMiddleFuel = TotalFuelTo(middle + 1);

			if (preMiddleFuel < middleFuel) return SearchAlignedPosition(from, middle);
			if (postMiddleFuel < middleFuel) return SearchAlignedPosition(middle, to);
			return middle;
		}

		private int TotalFuelTo(int target)
		{
			return _positions.Sum(consumption);

			int consumption(int position)
			{
				int steps = Math.Abs(target - position);
				return steps * (steps + 1) / 2;
			}
		}
	}
}
