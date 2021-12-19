namespace AdventOfCode.Year2021.Solvers.Day19;

internal sealed class InnerSolver
{
	private readonly IDictionary<int, Position[]> _scanners;

	public InnerSolver(IDictionary<int, Position[]> scanners)
	{
		_scanners = scanners;
	}

	public int CalculateUniquePositions()
	{
		Dictionary<int, List<OverlapInfo>> overlaps = GetOverlaps();
		
		var notCalculated = new HashSet<int>(_scanners.Keys);
		var uniquePositions = new HashSet<Position>();
		addScannerPositionAndOverlaps(0);
		return uniquePositions.Count;

		void addScannerPositionAndOverlaps(int scannerIndex, Func<Position, Position>? convert = null)
		{
			if (!notCalculated.Contains(scannerIndex)) return;
			
			notCalculated.Remove(scannerIndex);

			foreach (Position position in _scanners[scannerIndex])
			{
				uniquePositions.Add(convert?.Invoke(position) ?? position);
			}

			if (!overlaps.TryGetValue(scannerIndex, out var scannerOverlaps)) return;

			foreach (OverlapInfo overlapInfo in scannerOverlaps)
			{
				Position overlapConvert(Position position)
				{
					var rotation = _scannerRotations[overlapInfo.RotationToFirstScannerIndex];
					position = rotation(position);
					Position delta = overlapInfo.DeltaToFirstScanner;
					position = new Position(position.X + delta.X, position.Y + delta.Y, position.Z + delta.Z);
					return convert?.Invoke(position) ?? position;
				}

				addScannerPositionAndOverlaps(overlapInfo.SecondScannerIndex, overlapConvert);
			}
		}
	}

	public int CalculateMaxDistanceBetweenScanners()
	{
		Dictionary<int, List<OverlapInfo>> overlaps = GetOverlaps();
		var scannersPositions = new Dictionary<int, Position>();
		addScannerPosition(0);

		int maxDistance = 0;
		foreach (Position first in scannersPositions.Values)
		foreach (Position second in scannersPositions.Values)
		{
			int distance =
				Math.Abs(first.X - second.X) +
				Math.Abs(first.Y - second.Y) +
				Math.Abs(first.Z - second.Z);
			if (distance > maxDistance) maxDistance = distance;
		}

		return maxDistance;

		void addScannerPosition(int scannerIndex, Func<Position, Position>? convert = null)
		{
			if (scannersPositions.ContainsKey(scannerIndex)) return;

			var scannerPosition = convert?.Invoke(Position.Zero) ?? Position.Zero;
			scannersPositions.Add(scannerIndex, scannerPosition);
			
			if (!overlaps.TryGetValue(scannerIndex, out var scannerOverlaps)) return;

			foreach (OverlapInfo overlapInfo in scannerOverlaps)
			{
				Position overlapConvert(Position position)
				{
					var rotation = _scannerRotations[overlapInfo.RotationToFirstScannerIndex];
					position = rotation(position);
					Position delta = overlapInfo.DeltaToFirstScanner;
					position = new Position(position.X + delta.X, position.Y + delta.Y, position.Z + delta.Z);
					return convert?.Invoke(position) ?? position;
				}

				addScannerPosition(overlapInfo.SecondScannerIndex, overlapConvert);
			}
		}
	}


	private static readonly Dictionary<int, Func<Position, Position>> _scannerRotations = new()
	{
		{0, p => new Position(p.X, p.Y, p.Z)},
		{1, p => new Position(p.X, p.Z, -p.Y)},
		{2, p => new Position(p.X, -p.Y, -p.Z)},
		{3, p => new Position(p.X, -p.Z, p.Y)},

		{4, p => new Position(-p.X, -p.Y, p.Z)},
		{5, p => new Position(-p.X, p.Z, p.Y)},
		{6, p => new Position(-p.X, p.Y, -p.Z)},
		{7, p => new Position(-p.X, -p.Z, -p.Y)},

		{8, p => new Position(p.Y, -p.X, p.Z)},
		{9, p => new Position(p.Y, p.Z, p.X)},
		{10, p => new Position(p.Y, p.X, -p.Z)},
		{11, p => new Position(p.Y, -p.Z, -p.X)},

		{12, p => new Position(-p.Y, p.X, p.Z)},
		{13, p => new Position(-p.Y, p.Z, -p.X)},
		{14, p => new Position(-p.Y, -p.Z, p.X)},
		{15, p => new Position(-p.Y, -p.X, -p.Z)},

		{16, p => new Position(p.Z, p.Y, -p.X)},
		{17, p => new Position(p.Z, -p.X, -p.Y)},
		{18, p => new Position(p.Z, -p.Y, p.X)},
		{19, p => new Position(p.Z, p.X, p.Y)},

		{20, p => new Position(-p.Z, p.Y, p.X)},
		{21, p => new Position(-p.Z, p.X, -p.Y)},
		{22, p => new Position(-p.Z, -p.Y, -p.X)},
		{23, p => new Position(-p.Z, -p.X, p.Y)},
	};

	private Dictionary<int, List<OverlapInfo>> GetOverlaps()
	{
		var overlaps = new Dictionary<int, List<OverlapInfo>>();
		for (int i = 0; i < _scanners.Count; ++i)
		{
			Position[] firstPositions = _scanners[i];
			for (int j = 0; j < _scanners.Count; ++j)
			{
				if (i == j) continue;
			
				Position[] secondPositions = _scanners[j];
				foreach (var rotationKvp in _scannerRotations)
				{
					Position[] rotatedSecondPositions = RotatePositions(secondPositions, rotationKvp.Value);
					if (StronglyOverlapped(firstPositions, rotatedSecondPositions, out Position delta))
					{
						var overlapInfo = new OverlapInfo(j, rotationKvp.Key, delta);
						if (overlaps.TryGetValue(i, out var firstOverlaps))
						{
							firstOverlaps.Add(overlapInfo);
						}
						else
						{
							overlaps.Add(i, new List<OverlapInfo>(new[] {overlapInfo}));
						}
						break;
					}
				}
			}
		}
		return overlaps;
	}
	
	private Position[] RotatePositions(Position[] positions, Func<Position, Position> rotation)
	{
		var result = new Position[positions.Length];
		for (int i = 0; i < positions.Length; ++i)
		{
			result[i] = rotation(positions[i]);
		}
		return result;
	}

	private bool StronglyOverlapped(
		Position[] firstPositions,
		Position[] secondPositions,
		out Position toFirstDelta
		)
	{
		foreach (Position firstSample in firstPositions)
		foreach (Position secondSample in secondPositions)
		{
			int deltaX = firstSample.X - secondSample.X;
			int deltaY = firstSample.Y - secondSample.Y;
			int deltaZ = firstSample.Z - secondSample.Z;

			int overlapCounter = 0;
			foreach (var first in firstPositions)
			foreach (var second in secondPositions)
			{
				if (
					first.X == second.X + deltaX &&
					first.Y == second.Y + deltaY &&
					first.Z == second.Z + deltaZ
				)
				{
					if (++overlapCounter >= 12)
					{
						toFirstDelta = new Position(deltaX, deltaY, deltaZ);
						return true;
					}
				}
			}
		}

		toFirstDelta = Position.Zero;
		return false;
	}

	private sealed record OverlapInfo(
		int SecondScannerIndex,
		int RotationToFirstScannerIndex,
		Position DeltaToFirstScanner
		);
}
