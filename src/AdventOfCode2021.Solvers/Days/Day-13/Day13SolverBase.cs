namespace AdventOfCode.Year2021.Solvers.Day13;

public abstract class Day13SolverBase<TResult> : SolverWithArrayInput<IInputEntry, TResult>
{
	protected override TResult Solve(IInputEntry[] entries)
	{
		HashSet<Dot> dots = entries.OfType<DotEntry>().Select(e => new Dot(e.X, e.Y)).ToHashSet();
		FoldEntryBase[] folds = entries.OfType<FoldEntryBase>().ToArray();
		return Solve(dots, folds);
	}

	protected abstract TResult Solve(HashSet<Dot> dots, FoldEntryBase[] folds);

	protected static HashSet<Dot> Fold(HashSet<Dot> dots, FoldEntryBase fold)
	{
		return fold switch
		{
			HorizontalFoldEntry horizontalFold => HorizontalFold(dots, horizontalFold),
			VerticalFoldEntry verticalFold => VerticalFold(dots, verticalFold),
			_ => dots
		};
	}

	protected static HashSet<Dot> HorizontalFold(HashSet<Dot> dots,  HorizontalFoldEntry fold)
	{
		(_, int height) = GetDimensions(dots);
		if (fold.Position < 0 || fold.Position > height) return dots;

		int topHeight = fold.Position;
		var topDots = dots.Where(dot => dot.Y < topHeight).ToArray();
		var bottomDots = dots.Where(dot => dot.Y > topHeight).ToArray();
		
		bottomDots = doFold(bottomDots).ToArray();
		return topDots.Union(bottomDots).ToHashSet();
		
		IEnumerable<Dot> doFold(IEnumerable<Dot> someDots)
		{
			return someDots.Select(dot => new Dot(dot.X, topHeight * 2 - dot.Y));
		}
	}
	
	protected static HashSet<Dot> VerticalFold(HashSet<Dot> dots,  VerticalFoldEntry fold)
	{
		(int width, int _) = GetDimensions(dots);
		if (fold.Position < 0 || fold.Position > width) return dots;
		
		int leftWidth = fold.Position;

		var leftDots = dots.Where(dot => dot.X < leftWidth).ToArray();
		var rightDots = dots.Where(dot => dot.X > leftWidth).ToArray();
		
		rightDots = doFold(rightDots).ToArray();
		return leftDots.Union(rightDots).ToHashSet();
		
		IEnumerable<Dot> doFold(IEnumerable<Dot> someDots)
		{
			return someDots.Select(dot => new Dot(leftWidth * 2 - dot.X, dot.Y));
		}
	}

	protected static (int width, int height) GetDimensions(IEnumerable<Dot> dots)
	{
		return dots.Aggregate((0, 0), aggregate);

		(int, int) aggregate((int, int) dimensions, Dot dot)
		{
			var (width, height) = dimensions;
			if (dot.X >= width) width = dot.X + 1;
			if (dot.Y >= height) height = dot.Y + 1;
			return (width, height);
		}
	}
}
