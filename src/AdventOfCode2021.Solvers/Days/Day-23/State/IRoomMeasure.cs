namespace AdventOfCode.Year2021.Solvers.Day23;

interface IRoomMeasure
{
	int Size { get; }
}

internal class SmallRoom : IRoomMeasure
{
	public int Size => 2;
}

internal class LargeRoom : IRoomMeasure
{
	public int Size => 4;
}
