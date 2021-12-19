namespace AdventOfCode.Year2021.Solvers.Day19;

public static class Helpers
{
	public static IInputEntry ParseInputLine(string line)
	{
		int lineLength = line.Length;
		
		if (lineLength == 0 || line.Trim().Length == 0) return new Separator();
		if (line.StartsWith("--- scanner "))
		{
			int index = int.Parse(line.Substring(12, lineLength - 12 - 4));
			return new ScannerHeader(index);
		}

		int[] coordinates = line.Split(',').Select(int.Parse).ToArray();
		return new Beacon(new Position(coordinates[0], coordinates[1], coordinates[2]));
	}

	internal static async Task<Dictionary<int, Position[]>> ToScanners(
		this IAsyncEnumerable<IInputEntry> entries
		)
	{
		return await toScanners().ToDictionaryAsync(s => s.index, s => s.positions);
		
		async IAsyncEnumerable<(int index, Position[] positions)> toScanners()
		{
			int scannerIndex = -1;
			var beacons = new List<Position>();
			await foreach (var entry in entries.Where(e => e is not Separator))
			{
				if (entry is Beacon beacon)
				{
					beacons.Add(beacon.Position);
				}
				else if (entry is ScannerHeader scanner)
				{
					if (scannerIndex >= 0)
					{
						yield return (scannerIndex, beacons.ToArray());
					}
					scannerIndex = scanner.Index;
					beacons.Clear();
				}
			}

			if (scannerIndex >= 0)
			{
				yield return (scannerIndex, beacons.ToArray());
			}
		}
	}
}
