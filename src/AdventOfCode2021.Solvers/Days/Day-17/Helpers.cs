namespace AdventOfCode.Year2021.Solvers.Day17;

public static class Helpers
{
	public static Target ParseInputLine(string line)
	{
		(int, int)[] ranges = line.Substring(15).Split(", y=").Select(parseRange).Take(2).ToArray();
		return new Target(ranges[0], ranges[1]);

		static (int, int) parseRange(string str)
		{
			int[] bounds = str.Split("..").Select(int.Parse).Take(2).ToArray();
			return bounds[0] > bounds[1] ?
				(bounds[1], bounds[0]) :
				(bounds[0], bounds[1]);
		}
	} 
}
