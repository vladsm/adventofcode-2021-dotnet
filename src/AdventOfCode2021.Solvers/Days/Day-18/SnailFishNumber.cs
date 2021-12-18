namespace AdventOfCode.Year2021.Solvers.Day18;

public interface INumberComponent {}

public sealed class SnailFishNumber : List<INumberComponent>
{
	public SnailFishNumber()
	{
	}

	public SnailFishNumber(IEnumerable<INumberComponent> components) : base(components)
	{
	}
}


internal record struct OpenBracket : INumberComponent;

internal record struct CloseBracket : INumberComponent;

internal record struct Regular(int Number): INumberComponent;
