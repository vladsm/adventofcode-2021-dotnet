namespace AdventOfCode.Year2021.Solvers.Day5;

public sealed class Day05Level1Solver : Day05SolverBase
{
	protected override IAsyncEnumerable<Position> Split(FieldLine line)
	{
		var ((fromX, fromY), (toX, toY)) = line;
		var xSign = Math.Sign(toX - fromX);
		var ySign = Math.Sign(toY - fromY);

		if (fromX == toX)
		{
			return AsyncEnumerable.
				Range(0, ySign * (toY - fromY) + 1).
				Select(i => new Position(fromX, fromY + ySign * i));
		}
		if (fromY == toY)
		{
			return AsyncEnumerable.
				Range(0, xSign * (toX - fromX) + 1).
				Select(i => new Position(fromX + xSign * i, fromY));
		}
		return AsyncEnumerable.Empty<Position>();
	}
}
