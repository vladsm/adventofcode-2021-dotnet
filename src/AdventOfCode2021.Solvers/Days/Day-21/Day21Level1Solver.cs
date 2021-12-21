namespace AdventOfCode.Year2021.Solvers.Day21;

public sealed class Day21Level1Solver : SolverWithArrayInput<byte, int>
{
	protected override int Solve(byte[] entries)
	{
		const int scoreToWin = 1000;
		
		int firstPosition = entries[0];
		int secondPosition = entries[1];

		var (firstScore, secondScore, turnIndex) = DieCompositeValues().
			Select((value, index) => (value, index)).
			AccumulatingSelect((firstScore: 0, secondScore: 0, turnIndex: 0), makeTurn).
			SkipWhile(s => s.firstScore < scoreToWin && s.secondScore < scoreToWin).
			First();
		return firstScore >= scoreToWin ?
			secondScore * (turnIndex + 1) * 3 :
			firstScore * (turnIndex + 1) * 3;

		(int, int, int) makeTurn(
			(int firstScore, int secondScore, int turnIndex) state,
			(int diceCompositeValue, int index) turn
			)
		{
			if (turn.index % 2 == 0)
			{
				firstPosition = (firstPosition + turn.diceCompositeValue - 1) % 10 + 1;
				return (state.firstScore + firstPosition, state.secondScore, turn.index);
			}
			secondPosition = (secondPosition + turn.diceCompositeValue - 1) % 10 + 1;
			return (state.firstScore, state.secondScore + secondPosition, turn.index);
		}
	}

	private static IEnumerable<int> DieCompositeValues()
	{
		int currentValue = 1;
		while (true)
		{
			switch (currentValue)
			{
				case 98:
					yield return 98 + 99 + 100;
					currentValue = 1;
					break;
				case 99:
					yield return 99 + 100 + 1;
					currentValue = 2;
					break;
				case 100:
					yield return 100 + 1 + 2;
					currentValue = 3;
					break;
				default:
					yield return currentValue * 3 + 3;
					currentValue += 3;
					break;
			}
		}
		// ReSharper disable once IteratorNeverReturns
	}
}
