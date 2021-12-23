namespace AdventOfCode.Year2021.Solvers.Day22;

internal sealed class InnerSolver
{
	private readonly Cuboid[] _cuboids;

	public InnerSolver(Cuboid[] cuboids)
	{
		_cuboids = cuboids;
	}
	
	public ulong CalculateOnCubes()
	{
		var xRanges = GetRanges(c => c.X);
		var yRanges = GetRanges(c => c.Y);
		var zRanges = GetRanges(c => c.Z);

		ulong result = 0;
		foreach (var xRange in xRanges)
		{
			int[] xs = xRange.Cuboids;
			foreach (var yRange in yRanges)
			{
				var ys = CommonIndexes(xs, yRange.Cuboids);
				foreach (var zRange in zRanges)
				{
					int lastCuboidIndex = FirstCommonIndex(ys, zRange.Cuboids);
					if (lastCuboidIndex < 0) continue;
					if (!_cuboids[lastCuboidIndex].On) continue;

					result +=
						(ulong)(xRange.To - xRange.From) *
						(ulong)(yRange.To - yRange.From) *
						(ulong)(zRange.To - zRange.From);
				}
			}
		}
		return result;
	}


	private record struct Bound(int Position, bool Close);

	private record struct MarkedRange(int From, int To, int[] Cuboids);


	private MarkedRange[] GetRanges(Func<Cuboid, Range> getDimension)
	{
		return ToRanges(BoundsBy(getDimension), getDimension);
	}
	
	private Bound[] BoundsBy(Func<Cuboid, Range> getDimension)
	{
		return _cuboids.
			SelectMany(toBounds).
			Distinct().
			OrderBy(bound => bound.Position).
			ThenBy(bound => bound.Close).
			ToArray();
		
		IEnumerable<Bound> toBounds(Cuboid cuboid)
		{
			Range range = getDimension(cuboid);
			yield return new Bound(range.From, false);
			yield return new Bound(range.To, true);
		}
	}

	private MarkedRange[] ToRanges(Bound[] bounds, Func<Cuboid, Range> getDimension)
	{
		return bounds.
			Select(extend).
			LookForwardSelect(toRange).
			SelectMany(_ => _).
			Select(mark).
			ToArray();

		Bound extend(Bound bound)
		{
			return bound.Close ? new Bound(bound.Position + 1, bound.Close) : bound;
		}
		
		IEnumerable<Range> toRange(Bound bound, Bound? nexBound)
		{
			if (nexBound is null) yield break;
			yield return new Range(bound.Position, nexBound.Value.Position);
		}

		MarkedRange mark(Range range)
		{
			var cuboids = _cuboids.
				Select((cuboid, index) => (cuboid, index)).
				Where(e => overlaps(e.cuboid, range)).
				Select(c => c.index).
				ToHashSet();
			return new MarkedRange(range.From, range.To, cuboids.OrderByDescending(i => i).ToArray());
		}
		
		bool overlaps(Cuboid cuboid, Range range)
		{
			Range cuboidRange = getDimension(cuboid);
			return cuboidRange.From <= range.From && range.To - 1 <= cuboidRange.To;
		}
	}
	
	private static int[] CommonIndexes(int[] xs, int[] ys)
	{
		int xsLength = xs.Length;
		if (xsLength == 0) return Array.Empty<int>(); 
		int ysLength = ys.Length;
		if (ysLength == 0) return Array.Empty<int>();
		
		int i = 0;
		int j = 0;
		var result = new List<int>(xs.Length);
		while (true)
		{
			int x = xs[i];
			int y = ys[j];
			if (x == y)
			{
				result.Add(x);
				i++;
				j++;
			}
			else if (x > y)
			{
				i++;
			}
			else
			{
				j++;
			}

			if (i >= xsLength || j >= ysLength) return result.ToArray();
		}
	}
	
	private static int FirstCommonIndex(int[] ys, int[] zs)
	{
		int ysLength = ys.Length;
		if (ysLength == 0) return -1;
		int zsLength = zs.Length;
		if (zsLength == 0) return -1; 
		
		int i = 0;
		int j = 0;
		while (true)
		{
			int y = ys[i];
			int z = zs[j];
			if (y == z) return y;
			
			if (y > z)
			{
				i++;
			}
			else
			{
				j++;
			}
			if (i >= ysLength || j >= zsLength) return -1;
		}
	}
}
