namespace AdventOfCode.Year2021.Solvers.Day16;

internal abstract record PacketBase(int Version)
{
	public abstract ulong CalculateValue();
}

internal sealed record LiteralPacket(int Version, ulong Value) :
	PacketBase(Version)
{
	public override ulong CalculateValue() => Value;
}

internal abstract record OperatorPacket(int Version, PacketBase[] SubPackets) :
	PacketBase(Version);

internal sealed record SumPacket(int Version, PacketBase[] SubPackets) :
	OperatorPacket(Version, SubPackets)
{
	public override ulong CalculateValue() =>
		SubPackets.Aggregate(0UL, (acc, sp) => acc + sp.CalculateValue());
}

internal sealed record ProductPacket(int Version, PacketBase[] SubPackets) :
	OperatorPacket(Version, SubPackets)
{
	public override ulong CalculateValue() =>
		SubPackets.Aggregate(1UL, (acc, sp) => acc * sp.CalculateValue());
}

internal sealed record MinimumPacket(int Version, PacketBase[] SubPackets) :
	OperatorPacket(Version, SubPackets)
{
	public override ulong CalculateValue() =>
		SubPackets.Min(sp => sp.CalculateValue());
}

internal sealed record MaximumPacket(int Version, PacketBase[] SubPackets) :
	OperatorPacket(Version, SubPackets)
{
	public override ulong CalculateValue() =>
		SubPackets.Max(sp => sp.CalculateValue());
}

internal sealed record GreaterThanPacket(int Version, PacketBase[] SubPackets) :
	OperatorPacket(Version, SubPackets)
{
	public override ulong CalculateValue() =>
		SubPackets[0].CalculateValue() > SubPackets[1].CalculateValue() ? 1UL : 0UL;
}

internal sealed record LessThanPacket(int Version, PacketBase[] SubPackets) :
	OperatorPacket(Version, SubPackets)
{
	public override ulong CalculateValue() =>
		SubPackets[0].CalculateValue() < SubPackets[1].CalculateValue() ? 1UL : 0UL;
}

internal sealed record EqualToPacket(int Version, PacketBase[] SubPackets) :
	OperatorPacket(Version, SubPackets)
{
	public override ulong CalculateValue() =>
		SubPackets[0].CalculateValue() == SubPackets[1].CalculateValue() ? 1UL : 0UL;
}
