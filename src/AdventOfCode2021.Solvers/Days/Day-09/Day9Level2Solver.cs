namespace AdventOfCode.Year2021.Solvers.Day9;

public sealed class Day09Level2Solver : SolverWithArrayInput<byte[], int>
{
	const byte Highest = 9;

	protected override int Solve(byte[][] inputEntries)
	{
		const int numberOfLargestBasins = 3;
		
		int entryLength = inputEntries.First().Length;

		var emptyEntry = Enumerable.Repeat(Highest, entryLength + 2).ToArray();
		var beforeBegin = Enumerable.Repeat(emptyEntry, 1);
		var afterEnd = Enumerable.Repeat(emptyEntry, 1);
		
		var backEntries = beforeBegin.
			Concat(inputEntries.Select(Extend)).
			Concat(afterEnd).
			Select(e => e.Select(h => new Location(h)).ToArray()).
			ToArray();
		var entries = backEntries.Skip(1);

		var palette = new Palette();
		Location[][] colorizedEntries = entries.
			Zip(backEntries).
			Select(e => (current: e.First, previous: e.Second)).
			Select(e => Colorize(e, palette)).
			ToArray();
		palette.GroupSameColors();

		return colorizedEntries.
			SelectMany(row => row).
			Where(l => l.Color.HasValue).
			GroupBy(l => palette.GetModelColor(l.Color ?? -1)).
			Select(g => g.Count()).
			OrderByDescending(area => area).
			Take(numberOfLargestBasins).
			Aggregate(1, (production, area) => production * area);
	}
	
	
	private Location[] Colorize(
		(Location[] current, Location[] previous) entry,
		Palette palette
		)
	{
		var (current, previous) = entry;
		for (int i = 1; i < current.Length - 1; ++i)
		{
			Location location = current[i];
			if (location.Height == Highest) continue;

			Location left = current[i - 1];
			Location top = previous[i];

			if (left.Color != top.Color)
			{
				palette.SameColor(left.Color, top.Color);
			}
			location.Color = left.Color ?? top.Color ?? palette.NextColor();
		}
		return current;
	}

	private static byte[] Extend(byte[] source)
	{
		return new[] { Highest }.Concat(source).Concat(new[] { Highest }).ToArray();
	}


	private sealed class Location
	{
		public int Height { get; }
		public int? Color { get; set; }

		public Location(int height)
		{
			Height = height;
		}
	}


	private sealed class Palette
	{
		private readonly List<(int, int)> _sameColors = new();
		private Dictionary<int, int> _normalizedColors = new();

		private int _color;

		public int NextColor() => ++_color;

		public void SameColor(int? color1, int? color2)
		{
			if (color1 is null || color2 is null) return;
			_sameColors.Add((color1.Value, color2.Value));
		}

		public void GroupSameColors()
		{
			var sameColorsGroups = _sameColors.Aggregate(new HashSet<HashSet<int>>(), aggregate);
			_normalizedColors = sameColorsGroups.
				SelectMany(
					g =>
					{
						int modelColor = g.Min();
						return g.Select(color => (color, modelColor));
					}
					).
				ToDictionary(e => e.color, e => e.modelColor);

			HashSet<HashSet<int>> aggregate(HashSet<HashSet<int>> groups, (int, int) colorsPair)
			{
				(int color1, int color2) = colorsPair;
				var color1Group = groups.FirstOrDefault(g => g.Contains(color1));
				var color2Group = groups.FirstOrDefault(g => g.Contains(color2));
				
				
				if (color1Group is null && color2Group is null)
				{
					groups.Add(new HashSet<int> {color1, color2});
				}
				else if (color1Group == color2Group)
				{
					// do nothing
				}
				else if (color1Group is not null && color2Group is null)
				{
					color1Group.Add(color2);
				}
				else if (color1Group is null && color2Group is not null)
				{
					color2Group.Add(color1);
				}
				else if (color1Group is not null && color2Group is not null)
				{
					color1Group.UnionWith(color2Group);
					groups.Remove(color2Group);
				}
				return groups;
			}
		}

		public int GetModelColor(int color)
		{
			return _normalizedColors.TryGetValue(color, out int normalizedColor) ?
				normalizedColor :
				color;
		}
	}
}
