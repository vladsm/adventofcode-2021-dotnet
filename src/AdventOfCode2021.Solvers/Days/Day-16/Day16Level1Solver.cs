namespace AdventOfCode.Year2021.Solvers.Day16;

public sealed class Day16Level1Solver : Day16SolverBase
{
	protected override ulong Solve(char[][] entries)
	{
		var rootPacket = ParsePacket(entries);
		return SumVersions(rootPacket);
	}

	private static ulong SumVersions(PacketBase packet)
	{
		return packet switch
		{
			LiteralPacket => (ulong)packet.Version,
			OperatorPacket operatorPacket =>
				(ulong)packet.Version + operatorPacket.SubPackets.Aggregate(0UL, (acc, sp) => acc + SumVersions(sp)),
			_ => throw new NotSupportedException("Unknown packet type")
		};
	}
}
