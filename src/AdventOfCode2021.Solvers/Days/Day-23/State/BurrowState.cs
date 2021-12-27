using System.Text;

namespace AdventOfCode.Year2021.Solvers.Day23;

internal static class Const
{
	public const byte Empty = 0xFF;
	public const int HallwayLength = 7;
	public const int RoomsQuantity = 4;
}

internal readonly struct BurrowState<TRoomMeasure> where TRoomMeasure : IRoomMeasure, new()
{
	private readonly ulong _hallway = 0xFFFFFFFFFFFFFF;
	private readonly uint _room0;
	private readonly uint _room1;
	private readonly uint _room2;
	private readonly uint _room3;
	
	public int Energy { get; }
	
	// ReSharper disable once StaticMemberInGenericType
	public static int RoomSize { get; }

	static BurrowState()
	{
		RoomSize = new TRoomMeasure().Size;
	}

	public BurrowState(byte[][] rooms)
	{
		int roomSize = RoomSize;
		_room0 = toRoom(rooms[0]);
		_room1 = toRoom(rooms[1]);
		_room2 = toRoom(rooms[2]);
		_room3 = toRoom(rooms[3]);
		Energy = 0;

		uint toRoom(byte[] room)
		{
			uint result = RoomSize == 2 ? 0x0000FFFF : 0xFFFFFFFF;
			for (int i = 0; i < roomSize; ++i)
			{
				result = SetPosition(result, room[i], i);
			}
			return result;
		}
	}

	private BurrowState(ulong hallway, uint room0, uint room1, uint room2, uint room3, int energy)
	{
		_hallway = hallway;
		_room0 = room0;
		_room1 = room1;
		_room2 = room2;
		_room3 = room3;
		Energy = energy;
	}


	public bool IsFinal()
	{
		if (RoomSize == 4)
		{
			if (_room0 != 0x00000000) return false;
			if (_room1 != 0x01010101) return false;
			if (_room2 != 0x02020202) return false;
			if (_room3 != 0x03030303) return false;
			return true;
		}
		if (RoomSize == 2)
		{
			if (_room0 != 0x0000) return false;
			if (_room1 != 0x0101) return false;
			if (_room2 != 0x0202) return false;
			if (_room3 != 0x0303) return false;
			return true;
		}
		throw new NotSupportedException($"Room size {RoomSize} is not supported");
	}

	public bool IsSame(BurrowState<TRoomMeasure> anotherState)
	{
		return
			_hallway == anotherState._hallway &&
			_room0 == anotherState._room0 &&
			_room1 == anotherState._room1 &&
			_room2 == anotherState._room2 &&
			_room3 == anotherState._room3;
	}
	
	public static int GetMoveEnergy(int steps, byte amphipod)
	{
		return amphipod switch
		{
			0 => steps,
			1 => steps * 10,
			2 => steps * 100,
			3 => steps * 1000,
			_ => throw new NotSupportedException($"Amphipod '{amphipod}' is not supported")
		};
	}
	
	public byte GetHallwayEntry(int index)
	{
		return index switch
		{
			0 => (byte)(_hallway & 0x00000000000000FF),
			1 => (byte)((_hallway & 0x000000000000FF00) >> 8),
			2 => (byte)((_hallway & 0x0000000000FF0000) >> 16),
			3 => (byte)((_hallway & 0x00000000FF000000) >> 24),
			4 => (byte)((_hallway & 0x000000FF00000000) >> 32),
			5 => (byte)((_hallway & 0x0000FF0000000000) >> 40),
			6 => (byte)((_hallway & 0x00FF000000000000) >> 48),
			_ => throw new IndexOutOfRangeException()
		};
	}

	public bool IsRoomCompleting(int amphipod)
	{
		uint room = GetRoom(amphipod);
		if (RoomSize == 2)
		{
			return amphipod switch
			{
				0 => room is 0x00FF or 0x0000,
				1 => room is 0x01FF or 0x0101,
				2 => room is 0x02FF or 0x0202,
				3 => room is 0x03FF or 0x0303,
				_ => throw new NotSupportedException($"Amphipod '{amphipod}' is not supported")
			};
		}
		if (RoomSize == 4)
		{
			return amphipod switch
			{
				0 => room is 0x00FFFFFF or 0x0000FFFF or 0x000000FF or 0x00000000,
				1 => room is 0x01FFFFFF or 0x0101FFFF or 0x010101FF or 0x01010101,
				2 => room is 0x02FFFFFF or 0x0202FFFF or 0x020202FF or 0x02020202,
				3 => room is 0x03FFFFFF or 0x0303FFFF or 0x030303FF or 0x03030303,
				_ => throw new NotSupportedException($"Amphipod '{amphipod}' is not supported")
			};
		}
		throw new InvalidOperationException($"Room size {RoomSize} is not supported");
	}

	public (byte, int) FirstAmphipodInRoom(int roomIndex)
	{
		uint room = GetRoom(roomIndex);

		uint amphipodMask = room & 0x000000FF;
		if (amphipodMask != 0x000000FF) return ((byte)amphipodMask, 1);
		amphipodMask = room & 0x0000FF00;
		if (amphipodMask != 0x0000FF00) return ((byte)(amphipodMask >> 8), 2);
		if (RoomSize == 2) return (0xFF, -1);
		
		amphipodMask = room & 0x00FF0000;
		if (amphipodMask != 0x00FF0000) return ((byte)(amphipodMask >> 16), 3);
		amphipodMask = room & 0xFF000000;
		if (amphipodMask != 0xFF000000) return ((byte)(amphipodMask >> 24), 4);
		if (RoomSize == 4) return (0xFF, -1);

		throw new NotSupportedException($"Room size {RoomSize} is not supported");
	}

	public uint GetRoom(int index)
	{
		return index switch
		{
			0 => _room0,
			1 => _room1,
			2 => _room2,
			3 => _room3,
			_ => throw new ArgumentOutOfRangeException()
		};
	}

	public int NoObstaclesDistanceBetweenRooms(int room1Index, int room2Index)
	{
		return (room1Index, room2Index) switch
		{
			(0, 1) or (1, 0) => (_hallway & 0x00000000_00FF0000) == 0x00000000_00FF0000 ? 2 : -1,
			(0, 2) or (2, 0) => (_hallway & 0x00000000_FFFF0000) == 0x00000000_FFFF0000 ? 4 : -1,
			(0, 3) or (3, 0) => (_hallway & 0x000000FF_FFFF0000) == 0x000000FF_FFFF0000 ? 6 : -1,
			(1, 2) or (2, 1) => (_hallway & 0x00000000_FF000000) == 0x00000000_FF000000 ? 2 : -1,
			(1, 3) or (3, 1) => (_hallway & 0x000000FF_FF000000) == 0x000000FF_FF000000 ? 4 : -1,
			(2, 3) or (3, 2) => (_hallway & 0x000000FF_00000000) == 0x000000FF_00000000 ? 2 : -1,
			_ => throw new IndexOutOfRangeException()
		};
	}
	
	public int NoObstaclesDistanceBetweenHallwayAndRoom(int hallwayPosition, int roomIndex)
	{
		return (hallwayPosition, roomIndex) switch
		{
			(0, 0) => (_hallway & 0x00000000_0000FF00) == 0x00000000_0000FF00 ? 2 : -1,
			(0, 1) => (_hallway & 0x00000000_00FFFF00) == 0x00000000_00FFFF00 ? 4 : -1,
			(0, 2) => (_hallway & 0x00000000_FFFFFF00) == 0x00000000_FFFFFF00 ? 6 : -1,
			(0, 3) => (_hallway & 0x000000FF_FFFFFF00) == 0x000000FF_FFFFFF00 ? 8 : -1,
			(1, 0) => 1,
			(1, 1) => (_hallway & 0x00000000_00FF0000) == 0x00000000_00FF0000 ? 3 : -1,
			(1, 2) => (_hallway & 0x00000000_FFFF0000) == 0x00000000_FFFF0000 ? 5 : -1,
			(1, 3) => (_hallway & 0x000000FF_FFFF0000) == 0x000000FF_FFFF0000 ? 7 : -1,
			(2, 0) => 1,
			(2, 1) => 1,
			(2, 2) => (_hallway & 0x00000000_FF000000) == 0x00000000_FF000000 ? 3 : -1,
			(2, 3) => (_hallway & 0x000000FF_FF000000) == 0x000000FF_FF000000 ? 5 : -1,
			(3, 0) => (_hallway & 0x00000000_00FF0000) == 0x00000000_00FF0000 ? 3 : -1,
			(3, 1) => 1,
			(3, 2) => 1,
			(3, 3) => (_hallway & 0x000000FF_00000000) == 0x000000FF_00000000 ? 3 : -1,
			(4, 0) => (_hallway & 0x00000000_FFFF0000) == 0x00000000_FFFF0000 ? 5 : -1,
			(4, 1) => (_hallway & 0x00000000_FF000000) == 0x00000000_FF000000 ? 3 : -1,
			(4, 2) => 1,
			(4, 3) => 1,
			(5, 0) => (_hallway & 0x000000FF_FFFF0000) == 0x000000FF_FFFF0000 ? 7 : -1,
			(5, 1) => (_hallway & 0x000000FF_FF000000) == 0x000000FF_FF000000 ? 5 : -1,
			(5, 2) => (_hallway & 0x000000FF_00000000) == 0x000000FF_00000000 ? 3 : -1,
			(5, 3) => 1,
			(6, 0) => (_hallway & 0x0000FFFF_FFFF0000) == 0x0000FFFF_FFFF0000 ? 8 : -1,
			(6, 1) => (_hallway & 0x0000FFFF_FF000000) == 0x0000FFFF_FF000000 ? 6 : -1,
			(6, 2) => (_hallway & 0x0000FFFF_00000000) == 0x0000FFFF_00000000 ? 4 : -1,
			(6, 3) => (_hallway & 0x0000FF00_00000000) == 0x0000FF00_00000000 ? 2 : -1,
			_ => throw new ArgumentOutOfRangeException()
		};
	}

	public BurrowState<TRoomMeasure> MoveFromHallwayToSettleInRoom(
		int hallwayPosition,
		byte amphipod,
		int stepsToSettleInRoom,
		int moveEnergy
		)
	{
		ulong hallway = _hallway | (0x00000000_000000FFUL << hallwayPosition * 8);
		int energy = Energy + moveEnergy;
		uint newRoom(uint room) => SetPosition(room, amphipod, stepsToSettleInRoom - 1);

		return amphipod switch
		{
			0 => new BurrowState<TRoomMeasure>(hallway, newRoom(_room0), _room1, _room2, _room3, energy),
			1 => new BurrowState<TRoomMeasure>(hallway, _room0, newRoom(_room1), _room2, _room3, energy),
			2 => new BurrowState<TRoomMeasure>(hallway, _room0, _room1, newRoom(_room2), _room3, energy),
			3 => new BurrowState<TRoomMeasure>(hallway, _room0, _room1, _room2, newRoom(_room3), energy),
			_ => throw new NotSupportedException($"Amphipod '{amphipod}' is not supported")
		};
	}

	public BurrowState<TRoomMeasure> MoveFromRoomToSettleInRoom(
		int sourceRoomIndex,
		int stepsToExitRoom,
		byte amphipod,
		int stepsToSettleInRoom,
		int moveEnergy
		)
	{
		ulong hallway = _hallway;
		int energy = Energy + moveEnergy;
		uint sourceRoomMask = (uint)(0x000000FF << (stepsToExitRoom - 1) * 8);
		uint targetRoom(uint room) => SetPosition(room, amphipod, stepsToSettleInRoom - 1);

		return (sourceRoomIndex, amphipod) switch
		{
			(0, 1) => newState(_room0 | sourceRoomMask, targetRoom(_room1), _room2, _room3),
			(0, 2) => newState(_room0 | sourceRoomMask, _room1, targetRoom(_room2), _room3),
			(0, 3) => newState(_room0 | sourceRoomMask, _room1, _room2, targetRoom(_room3)),
			(1, 0) => newState(targetRoom(_room0), _room1 | sourceRoomMask, _room2, _room3),
			(1, 2) => newState(_room0, _room1 | sourceRoomMask, targetRoom(_room2), _room3),
			(1, 3) => newState(_room0, _room1 | sourceRoomMask, _room2, targetRoom(_room3)),
			(2, 0) => newState(targetRoom(_room0), _room1, _room2 | sourceRoomMask, _room3),
			(2, 1) => newState(_room0, targetRoom(_room1), _room2 | sourceRoomMask, _room3),
			(2, 3) => newState(_room0, _room1, _room2 | sourceRoomMask, targetRoom(_room3)),
			(3, 0) => newState(targetRoom(_room0), _room1, _room2, _room3 | sourceRoomMask),
			(3, 1) => newState(_room0, targetRoom(_room1), _room2, _room3 | sourceRoomMask),
			(3, 2) => newState(_room0, _room1, targetRoom(_room2), _room3 | sourceRoomMask),
			_ => throw new NotSupportedException(
				$"Source room index '{sourceRoomIndex}' or amphipod '{amphipod}' is not supported"
				)
		};

		BurrowState<TRoomMeasure> newState(uint newRoom0, uint newRoom1, uint newRoom2, uint newRoom3)
		{
			return new BurrowState<TRoomMeasure>(hallway, newRoom0, newRoom1, newRoom2, newRoom3, energy);
		}
	}

	public BurrowState<TRoomMeasure> MoveFromRoomToHallway(
		int sourceRoomIndex,
		int stepsToExitRoom,
		byte amphipod,
		int hallwayPosition,
		int moveEnergy
		)
	{
		ulong hallway = SetPosition(_hallway, amphipod, hallwayPosition);
		int energy = Energy + moveEnergy;
		uint roomMask = (uint)(0x000000FF << (stepsToExitRoom - 1) * 8);

		return sourceRoomIndex switch
		{
			0 => new BurrowState<TRoomMeasure>(hallway, _room0 | roomMask, _room1, _room2, _room3, energy),
			1 => new BurrowState<TRoomMeasure>(hallway, _room0, _room1 | roomMask, _room2, _room3, energy),
			2 => new BurrowState<TRoomMeasure>(hallway, _room0, _room1, _room2 | roomMask, _room3, energy),
			3 => new BurrowState<TRoomMeasure>(hallway, _room0, _room1, _room2, _room3 | roomMask, energy),
			_ => throw new NotSupportedException($"Room index '{sourceRoomIndex}' is not supported")
		};
	}
	
	public static int StepsToSettleInRoom(uint room, byte amphipod)
	{
		int roomSize = RoomSize;

		if (roomSize == 2)
		{
			if (room == 0x0000FFFF) return 2;
			uint secondOnly = SetPosition(0x0000FFFF, amphipod, 1);
			uint secondAndFirst = SetPosition(secondOnly, amphipod, 0);
			if (room == secondOnly) return 1;
			if (room == secondAndFirst) return 0;
			return -1;
		}
		if (roomSize == 4)
		{
			if (room == 0xFFFFFFFF) return 4;
			uint fourthOnly = SetPosition(0xFFFFFFFF, amphipod, 3);
			uint fourthAndThird = SetPosition(fourthOnly, amphipod, 2);
			uint fourthAndThirdAndSecond = SetPosition(fourthAndThird, amphipod, 1);
			uint fourthAndThirdAndSecondAndFirst = SetPosition(fourthAndThirdAndSecond, amphipod, 0);
			if (room == fourthOnly) return 3;
			if (room == fourthAndThird) return 2;
			if (room == fourthAndThirdAndSecond) return 1;
			if (room == fourthAndThirdAndSecondAndFirst) return 0;
			return -1;
		}
		throw new NotSupportedException($"Room size {roomSize} is not supported");
	}

	private static ulong SetPosition(ulong source, byte value, int position)
	{
		int shift = position * 8;
		ulong mask = ~(0xFFUL << shift);
		return (source & mask) | ((ulong)value << shift);
	}
	
	private static uint SetPosition(uint source, byte value, int position)
	{
		int shift = position * 8;
		uint mask = ~(0xFFU << shift);
		return (source & mask) | ((uint)value << shift);
	}


	public string View => ToString();

	public override string ToString()
	{
		char[] hs = ToSymbols(_hallway, 7);
		var sb = new StringBuilder();
		sb.AppendLine($"{hs[0]}{hs[1]}.{hs[2]}.{hs[3]}.{hs[4]}.{hs[5]}{hs[6]}");

		var r0 = ToSymbols(_room0, RoomSize);
		var r1 = ToSymbols(_room1, RoomSize);
		var r2 = ToSymbols(_room2, RoomSize);
		var r3 = ToSymbols(_room3, RoomSize);
		for (int i = 0; i < RoomSize; ++i)
		{
			sb.AppendLine($"##{r0[i]}#{r1[i]}#{r2[i]}#{r3[i]}##");
		}
		sb.AppendLine($"{Energy}");
		return sb.ToString();
	}

	private static char[] ToSymbols(ulong value, int length)
	{
		return toBytes().Take(length).Select(toChar).ToArray();
		
		IEnumerable<byte> toBytes()
		{
			yield return (byte)(value & 0xFFUL);
			yield return (byte)((value >> 8) & 0xFFUL);
			yield return (byte)((value >> 16) & 0xFFUL);
			yield return (byte)((value >> 24) & 0xFFUL);
			yield return (byte)((value >> 32) & 0xFFUL);
			yield return (byte)((value >> 40) & 0xFFUL);
			yield return (byte)((value >> 48) & 0xFFUL);
			yield return (byte)((value >> 56) & 0xFFUL);
		}

		static char toChar(byte amphipod)
		{
			return amphipod switch
			{
				0 => 'A',
				1 => 'B',
				2 => 'C',
				3 => 'D',
				_ => '.'
			};
		} 
	}
}
