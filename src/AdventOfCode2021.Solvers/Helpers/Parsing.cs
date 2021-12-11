namespace AdventOfCode.Year2021.Solvers;

public static class Parsing
{
	public static int[] ParseToIntArray(string line) =>
		line.Split(',').Select(int.Parse).ToArray();

	public static char[] ParseToCharactersArray(string line) =>
		line.ToArray();

	public static byte[] ParseToDigitsArray(string line)
	{
		return line.Select(toDigit).ToArray();

		byte toDigit(char character)
		{
			return character switch
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
				_ => throw new NotSupportedException($"Character '{character}' is not supported")
			};
		}
	}
		
	
}
