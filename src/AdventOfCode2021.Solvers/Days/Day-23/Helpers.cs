namespace AdventOfCode.Year2021.Solvers.Day23;

public static class Helpers
{
	public static IInputEntry ParseInputLine(string line, int index)
	{
		return index switch
		{
			{} when index == 2 || index == 3 => parseLocations(line),
			_ => new SkipEntry()
		};
		
		static LocationsEntry parseLocations(string line)
		{
			return new LocationsEntry(
				charToByte(line[3]),
				charToByte(line[5]),
				charToByte(line[7]),
				charToByte(line[9])
				);
			
			byte charToByte(char ch)
			{
				return ch switch
				{
					'A' => 0,
					'B' => 1,
					'C' => 2,
					'D' => 3,
					_ => throw new NotSupportedException($"Amphipod type '{ch}' is not supported")
				};
			}
		}
	}
}
