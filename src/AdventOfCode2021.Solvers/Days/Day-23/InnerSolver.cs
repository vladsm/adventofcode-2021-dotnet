namespace AdventOfCode.Year2021.Solvers.Day23;

internal sealed class InnerSolver<TRoomMeasure> where TRoomMeasure : IRoomMeasure, new()
{
	private readonly byte[][] _rooms;
	private int _minEnergy;

	public InnerSolver(LocationsEntry[] locationsEntries)
	{
		_rooms = toRooms(locationsEntries);
		
		static byte[][] toRooms(LocationsEntry[] locationsEntries)
		{
			byte[] room1 = locationsEntries.Select(e => e.FirstRoom).ToArray();
			byte[] room2 = locationsEntries.Select(e => e.SecondRoom).ToArray();
			byte[] room3 = locationsEntries.Select(e => e.ThirdRoom).ToArray();
			byte[] room4 = locationsEntries.Select(e => e.FourthRoom).ToArray();
			return new[] { room1, room2, room3, room4 };
		}
	}

	public int CalculateEnergy()
	{
		_minEnergy = int.MaxValue;
		var initial = new BurrowState<TRoomMeasure>(_rooms);
		foreach (var _ in MovesVariations(initial))
		{
		}
		return _minEnergy;
	}


	private IEnumerable<BurrowState<TRoomMeasure>> MovesVariations(BurrowState<TRoomMeasure> currentState)
	{
		var variations = immediateVariations(currentState).ToArray();
		foreach (var variation in variations)
		{
			if (variation.Energy >= _minEnergy) continue;
			
			if (variation.IsFinal())
			{
				_minEnergy = variation.Energy;
				yield return variation;
			}
			else if (!currentState.IsSame(variation))
			{
				foreach (var nextVariation in MovesVariations(variation))
				{
					yield return nextVariation;
				}
			}
		}
		
		static IEnumerable<BurrowState<TRoomMeasure>> immediateVariations(BurrowState<TRoomMeasure> fromState)
		{
			BurrowState<TRoomMeasure> toFinal = MoveToFinal(fromState);
			if (toFinal.IsFinal())
			{
				yield return toFinal;
				yield break;
			}
			if (!fromState.IsSame(toFinal)) yield return toFinal;

			for (int i = 0; i < Const.RoomsQuantity; ++i)
			{
				if (fromState.IsRoomCompleting(i)) continue;
				foreach (BurrowState<TRoomMeasure> state in MovesVariationsToHallway(fromState, i).ToArray())
				{
					if (fromState.IsSame(state)) continue;
					yield return state;
				}
			}
		}
	}
	
	private static BurrowState<TRoomMeasure> MoveToFinal(BurrowState<TRoomMeasure> state)
	{
		BurrowState<TRoomMeasure> originalState = state;
		
		for (int i = 0; i < Const.HallwayLength; ++i)
		{
			byte amphipod = state.GetHallwayEntry(i);
			if (amphipod == Const.Empty) continue;

			int hallwayStepsToRoom = state.NoObstaclesDistanceBetweenHallwayAndRoom(i, amphipod);
			if (hallwayStepsToRoom < 0) continue;

			var targetRoom = state.GetRoom(amphipod);
			int stepsToSettleInRoom = BurrowState<TRoomMeasure>.StepsToSettleInRoom(targetRoom, amphipod);
			if (stepsToSettleInRoom < 0) continue;

			int moveEnergy = BurrowState<TRoomMeasure>.GetMoveEnergy(hallwayStepsToRoom + stepsToSettleInRoom, amphipod);
			state = state.MoveFromHallwayToSettleInRoom(i, amphipod, stepsToSettleInRoom, moveEnergy);
		}
		
		if (state.IsFinal()) return state;

		for (int i = 0; i < Const.RoomsQuantity; ++i)
		{
			if (state.IsRoomCompleting(i)) continue;
			
			(byte amphipod, int stepsToExitRoom) = state.FirstAmphipodInRoom(i);
			if (amphipod == Const.Empty || amphipod == i) continue;

			int distanceBetweenRooms = state.NoObstaclesDistanceBetweenRooms(i, amphipod);
			if (distanceBetweenRooms < 0) continue; 
			
			var targetRoom = state.GetRoom(amphipod);
			int stepsToSettleInRoom = BurrowState<TRoomMeasure>.StepsToSettleInRoom(targetRoom, amphipod);
			if (stepsToSettleInRoom < 0) continue;

			int numberOfSteps = stepsToExitRoom + distanceBetweenRooms + stepsToSettleInRoom;
			int moveEnergy = BurrowState<TRoomMeasure>.GetMoveEnergy(numberOfSteps, amphipod);
			state = state.MoveFromRoomToSettleInRoom(i, stepsToExitRoom, amphipod, stepsToSettleInRoom, moveEnergy);
		}
		
		if (state.IsFinal() || originalState.IsSame(state)) return state;

		return MoveToFinal(state);
	}
	
	private static IEnumerable<BurrowState<TRoomMeasure>> MovesVariationsToHallway(
		BurrowState<TRoomMeasure> state,
		int sourceRoomIndex
		)
	{
		for (int i = 0; i < Const.HallwayLength; ++i)
		{
			byte hallwayEntry = state.GetHallwayEntry(i);
			if (hallwayEntry != Const.Empty) continue;

			int hallwaySteps = state.NoObstaclesDistanceBetweenHallwayAndRoom(i, sourceRoomIndex);
			if (hallwaySteps < 0) continue;
			
			(byte amphipod, int stepsToExitRoom) = state.FirstAmphipodInRoom(sourceRoomIndex);
			if (amphipod == Const.Empty) continue;

			var numberOfSteps = stepsToExitRoom + hallwaySteps;
			int moveEnergy = BurrowState<TRoomMeasure>.GetMoveEnergy(numberOfSteps, amphipod);

			yield return state.MoveFromRoomToHallway(sourceRoomIndex, stepsToExitRoom, amphipod, i, moveEnergy);
		}
	}
}
