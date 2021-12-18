namespace AdventOfCode.Year2021.Solvers.Day18;

public static class Helpers
{
	public static SnailFishNumber ParseInputLine(string line)
	{
		var components = line.SelectMany(toComponent);
		return new SnailFishNumber(components);

		static IEnumerable<INumberComponent> toComponent(char character)
		{
			return character switch
			{
				'[' => new INumberComponent[] {new OpenBracket()},
				']' => new INumberComponent[] {new CloseBracket()},
				var digit when char.IsDigit(digit) => new INumberComponent[] {new Regular(toNumber(digit))},
				_ => Array.Empty<INumberComponent>()
			};
		}

		static int toNumber(char digit)
		{
			return digit switch
			{
				'0' => 0,
				'1' => 1,
				'2' => 2,
				'3' => 3,
				'4' => 4,
				'5' => 5,
				'6' => 6,
				'7' => 7,
				'8' => 8,
				'9' => 9,
				_ => throw new NotSupportedException($"Digit '{digit}' is not supported")
			};
		} 
	}
}
