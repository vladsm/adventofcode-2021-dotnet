namespace AdventOfCode.Year2021.Solvers.Day18;

public abstract class Day18SolverBase : IAsyncSolver<SnailFishNumber, ulong>
{
	public abstract ValueTask<ulong> Solve(IAsyncEnumerable<SnailFishNumber> entries);

	protected static SnailFishNumber Add(SnailFishNumber first, SnailFishNumber second)
	{
		if (second.Count == 0) return first;
		if (first.Count == 0) return second;
		
		var sum = new List<INumberComponent>(first.Count + second.Count + 2)
		{
			new OpenBracket()
		};
		sum.AddRange(first);
		sum.AddRange(second);
		sum.Add(new CloseBracket());
		Reduce(sum);
		return new SnailFishNumber(sum);
	}

	protected static ulong CalculateMagnitude(List<INumberComponent> components)
	{
		while (true)
		{
			if (components.Count == 1 && components[0] is Regular regular)
			{
				return (ulong)regular.Number;
			}

			for (int i = 0; i < components.Count; ++i)
			{
				if (i + 3 >= components.Count) break;
				if (components[i] is not OpenBracket) continue;
				if (components[i + 1] is not Regular left) continue;
				if (components[i + 2] is not Regular right) continue;
				if (components[i + 3] is not CloseBracket) continue;
			
				components.RemoveRange(i, 4);
				components.Insert(i, new Regular(3 * left.Number + 2 * right.Number));
			}
		}
	}


	private static void Reduce(List<INumberComponent> components)
	{
		while (true)
		{
			if (TryExplode(components)) continue;
			if (TrySplit(components)) continue;
			break;
		}
	}

	private static bool TryExplode(List<INumberComponent> components)
	{
		int indexToExplodeAt = FindPairToExplode(components);
		if (indexToExplodeAt < 0) return false;

		INumberComponent left = components[indexToExplodeAt + 1];
		if (left is not Regular leftRegular) throw new InvalidOperationException("Number is incorrect");
		INumberComponent right = components[indexToExplodeAt + 2];
		if (right is not Regular rightRegular) throw new InvalidOperationException("Number is incorrect");

		for (int i = indexToExplodeAt - 1; i >= 0; --i)
		{
			if (components[i] is not Regular regular) continue;
			components.RemoveAt(i);
			components.Insert(i, new Regular(regular.Number + leftRegular.Number));
			break;
		}
		for (int i = indexToExplodeAt + 3; i < components.Count; ++i)
		{
			if (components[i] is not Regular regular) continue;
			components.RemoveAt(i);
			components.Insert(i, new Regular(regular.Number + rightRegular.Number));
			break;
		}
		
		components.RemoveRange(indexToExplodeAt, 4);
		components.Insert(indexToExplodeAt, new Regular(0));
		
		return true;
	}

	private static int FindPairToExplode(List<INumberComponent> components)
	{
		int openBracketsCounter = 0;
		for (int i = 0; i < components.Count; ++i)
		{
			INumberComponent component = components[i];
			switch (component)
			{
				case OpenBracket:
					++openBracketsCounter;
					if (openBracketsCounter == 5) return i;
					break;
				case CloseBracket:
					--openBracketsCounter;
					break;
			}
		}
		return -1;
	}

	private static bool TrySplit(List<INumberComponent> components)
	{
		for (int i = 0; i < components.Count; ++i)
		{
			var component = components[i];
			if (component is not Regular regular || regular.Number < 10) continue;

			int oddIncrement = regular.Number % 2 == 0 ? 0 : 1;
			int newNumber = regular.Number / 2;
			components.RemoveAt(i);
			var replacement = new INumberComponent[]
			{
				new OpenBracket(),
				new Regular(newNumber),
				new Regular(newNumber + oddIncrement),
				new CloseBracket()
			};
			components.InsertRange(i, replacement);
			return true;
		}
		return false;
	}
}
