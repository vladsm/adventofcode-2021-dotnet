namespace AdventOfCode.Year2021.Solvers.Day21;

public sealed class Day21Level2Solver : SolverWithArrayInput<byte, ulong>
{
	private const int ScoreToWin = 21;
	
	protected override ulong Solve(byte[] entries)
	{
		var states = new Dictionary<State, ulong> { {new State(entries[0], 0, entries[1], 0), 1} };
		State[] notFinishedStates = NotFinishedStates(states);

		while (notFinishedStates.Length > 0)
		{
			MakeTurn(
				s => s.FirstPosition, s => s.FirstScore,
				(s, newPosition, newScore) => new State(newPosition, newScore, s.SecondPosition, s.SecondScore),
				notFinishedStates,
				states
				);
			notFinishedStates = NotFinishedStates(states);
			
			MakeTurn(
				s => s.SecondPosition,
				s => s.SecondScore,
				(s, newPosition, newScore) => new State(s.FirstPosition, s.FirstScore, newPosition, newScore),
				notFinishedStates,
				states
				);
			notFinishedStates = NotFinishedStates(states);
		}

		(ulong firstWins, ulong secondWins) = states.Aggregate(
			(firstWins: 0UL, secondWins: 0UL),
			(acc, s) =>
			(
				acc.firstWins + (s.Key.FirstScore >= ScoreToWin ? s.Value : 0),
				acc.secondWins + (s.Key.SecondScore >= ScoreToWin ? s.Value : 0)
			)
			);
		return firstWins > secondWins ? firstWins : secondWins; 
	}

	private delegate State CreateNewStateDelegate(State state, byte newPosition, byte newScore);

	private static void MakeTurn(
		Func<State, byte> getPosition,
		Func<State, byte> getScore,
		CreateNewStateDelegate createNewState,
		State[] notFinishedStates,
		Dictionary<State, ulong> states
		)
	{
		var halfTurnVariations = DieCompositeVariations().ToArray();
		var newStates = new Dictionary<State, ulong>();
		foreach (State state in notFinishedStates)
		{
			ulong stateQuantity = states[state];
			foreach ((int dieValue, ulong numberOfVariations) in halfTurnVariations)
			{
				byte position = getPosition(state);
				byte newPosition = (byte)((position + dieValue - 1) % 10 + 1);
				var newState = createNewState(state, newPosition, (byte)(getScore(state) + newPosition));
				Add(newState, stateQuantity * numberOfVariations, newStates);
			}
		}

		Remove(notFinishedStates, states);
		Add(newStates, states);
	}
	
	private State[] NotFinishedStates(Dictionary<State, ulong> states)
	{
		return states.
			Keys.
			Where(s => s.FirstScore < ScoreToWin && s.SecondScore < ScoreToWin).
			ToArray();
	}

	private static void Add(Dictionary<State, ulong> statesToAdd, Dictionary<State, ulong> states)
	{
		foreach (var kvp in statesToAdd)
		{
			Add(kvp.Key, kvp.Value, states);
		}
	}
	
	private static void Add(State state, ulong quantityToAdd, Dictionary<State, ulong> states)
	{
		if (states.TryGetValue(state, out ulong quantity))
		{
			states[state] = quantity + quantityToAdd;
		}
		else
		{
			states.Add(state, quantityToAdd);
		}
	}

	private static void Remove(IEnumerable<State> statesToRemove, Dictionary<State, ulong> states)
	{
		foreach (State state in statesToRemove)
		{
			states.Remove(state);
		}
	}
	
	private static IEnumerable<(byte value, ulong count)> DieCompositeVariations()
	{
		yield return (3, 1);
		yield return (4, 3);
		yield return (5, 6);
		yield return (6, 7);
		yield return (7, 6);
		yield return (8, 3);
		yield return (9, 1);
	}


	private record struct State(
		byte FirstPosition,
		byte FirstScore,
		byte SecondPosition,
		byte SecondScore
		);
}
