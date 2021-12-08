namespace AdventOfCode.Year2021.Solvers.Day7;

public sealed class Day07Level1Solver : SolverWithArrayInput<int[], int>
{
	protected override int Solve(int[][] input)
	{
		if (input.Length == 0) throw new ArgumentException("No input", nameof(input));

		int[] positions = input[0].OrderBy(position => position).ToArray();
		int halfLength = positions.Length / 2;

		var alignedPosition = positions.Length % 2 == 0 ?
			(positions[halfLength] + positions[halfLength - 1]) / 2 :
			positions[halfLength];

		return positions.Sum(position => Math.Abs(position - alignedPosition));
	}
}