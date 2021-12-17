namespace AdventOfCode.Year2021.Solvers.Day17;

public sealed class Day17Level1Solver : SolverWithArrayInput<Target, int>
{
	protected override int Solve(Target[] entries)
	{
		Target target = entries[0];
		return Enumerable.
			Range(1, 2*Math.Max(Math.Abs(target.YRange.from), Math.Abs(target.YRange.to))).
			Select(steps => CalculateForStepsToTarget(steps, target)).
			Max();
	}

	private int CalculateForStepsToTarget(int steps, Target target)
	{
		int velocityYFrom = (int)((double)target.YRange.from / steps + ((double)steps - 1) / 2);
		int velocityYTo = (int)((double)target.YRange.to / steps + ((double)steps - 1) / 2);
		if (velocityYFrom > velocityYTo)
		{
			(velocityYFrom, velocityYTo) = (velocityYTo, velocityYFrom);
		}
		if (velocityYFrom <= 0) return 0;
		
		return Enumerable.
			Range(velocityYFrom, velocityYTo - velocityYFrom + 1).
			Select(v => CalculateForStepsToTargetAndVelocityY(steps, v, target)).
			Max();
	}

	private int CalculateForStepsToTargetAndVelocityY(int steps, int velocityY, Target target)
	{
		int targetY = steps * velocityY - steps * (steps - 1) / 2;
		if (target.YRange.from > targetY || targetY > target.YRange.to) return -1;
		if (!CheckX(steps, target)) return -1;
		if (velocityY < 0) return 0;
		return (velocityY * velocityY + velocityY) / 2;
	}

	private bool CheckX(int steps, Target target)
	{
		int velocityXFrom = (int)((double)target.XRange.from / steps + ((double)steps - 1) / 2);
		int velocityXTo = (int)((double)target.XRange.to / steps + ((double)steps - 1) / 2);
		if (velocityXFrom > velocityXTo) velocityXTo = velocityXFrom;
		return velocityXTo > 0;
	}
}
