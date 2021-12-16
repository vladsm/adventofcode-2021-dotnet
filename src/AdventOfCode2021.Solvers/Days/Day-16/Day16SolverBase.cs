namespace AdventOfCode.Year2021.Solvers.Day16;

public abstract class Day16SolverBase : SolverWithArrayInput<char[], ulong>
{
	internal static PacketBase ParsePacket(char[][] entries)
	{
		Span<bool> message = entries[0].SelectMany(ToBits).ToArray().AsSpan();
		ParsePacket(message, out var rootPacket);
		return rootPacket;
	}
	
	private static Span<bool> ParsePacket(Span<bool> message, out PacketBase packet)
	{
		message = message.Read(3, out var versionBits);
		message = message.Read(3, out var typeBits);
		int version = versionBits.ToInt();
		int type = typeBits.ToInt();

		return type switch
		{
			4 => ParseLiteralPacket(version, message, out packet),
			_ => ParseOperatorPacket(version, type, message, out packet)
		};
	}

	private static Span<bool> ParseLiteralPacket(
		int version,
		Span<bool> message,
		out PacketBase packet
		)
	{
		bool continueRead = true;
		ulong value = 0;
		while (continueRead)
		{
			message = message.Read(1, out var controlBit);
			continueRead = controlBit[0];
			message = message.Read(4, out var group);

			value += (ulong)group.ToInt();
			if (continueRead) value <<= 4;
		}

		packet = new LiteralPacket(version, value);
		return message;
	}

	private static Span<bool> ParseOperatorPacket(
		int version,
		int type,
		Span<bool> message,
		out PacketBase packet
		)
	{
		message = message.Read(1, out var lengthTypeBit);
		return lengthTypeBit[0] switch
		{
			false => ParseOperatorPacketWithTotalLength(version, type, message, out packet),
			true => ParseOperatorPacketWithNumberOfSubPackets(version, type, message, out packet)
		};
	}

	private static Span<bool> ParseOperatorPacketWithTotalLength(
		int version,
		int type,
		Span<bool> message,
		out PacketBase packet
		)
	{
		message = message.Read(15, out var lengthBits);
		int length = lengthBits.ToInt();

		var subPackets = new List<PacketBase>();
		while (length > 0)
		{
			int lengthBeforeParsing = message.Length;
			message = ParsePacket(message, out var subPacket);
			subPackets.Add(subPacket);
			int lengthAfterParsing = message.Length;
			length -= (lengthBeforeParsing - lengthAfterParsing);
		}
		
		packet = CreateOperatorPacket(version, type, subPackets.ToArray());
		return message;
	}

	private static Span<bool> ParseOperatorPacketWithNumberOfSubPackets(
		int version,
		int type,
		Span<bool> message,
		out PacketBase packet
		)
	{
		message = message.Read(11, out var numberOfSubPacketsBits);
		int numberOfSubPackets = numberOfSubPacketsBits.ToInt();

		var subPackets = new List<PacketBase>();
		while (numberOfSubPackets > 0)
		{
			message = ParsePacket(message, out var subPacket);
			subPackets.Add(subPacket);
			--numberOfSubPackets;
		}
		
		packet = CreateOperatorPacket(version, type, subPackets.ToArray());
		return message;
	}

	private static OperatorPacket CreateOperatorPacket(
		int version,
		int type,
		PacketBase[] subPackets
		)
	{
		return type switch
		{
			0 => new SumPacket(version, subPackets),
			1 => new ProductPacket(version, subPackets),
			2 => new MinimumPacket(version, subPackets),
			3 => new MaximumPacket(version, subPackets),
			5 => new GreaterThanPacket(version, subPackets),
			6 => new LessThanPacket(version, subPackets),
			7 => new EqualToPacket(version, subPackets),
			_ => throw new NotSupportedException($"Packet type {type} is not supported")
		};
	}

	private static IEnumerable<bool> ToBits(char ch)
	{
		return ch switch
		{
			'0' => new[] { false, false, false, false },
			'1' => new[] { false, false, false, true },
			'2' => new[] { false, false, true, false },
			'3' => new[] { false, false, true, true },
			'4' => new[] { false, true, false, false },
			'5' => new[] { false, true, false, true },
			'6' => new[] { false, true, true, false },
			'7' => new[] { false, true, true, true },
			'8' => new[] { true, false, false, false },
			'9' => new[] { true, false, false, true },
			'A' => new[] { true, false, true, false },
			'B' => new[] { true, false, true, true },
			'C' => new[] { true, true, false, false },
			'D' => new[] { true, true, false, true },
			'E' => new[] { true, true, true, false },
			'F' => new[] { true, true, true, true },
			_ => throw new NotSupportedException($"Hexadecimal digit '{ch}' is not supported")
		};
	}
}
