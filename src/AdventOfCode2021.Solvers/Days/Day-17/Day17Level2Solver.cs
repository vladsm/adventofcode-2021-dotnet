namespace AdventOfCode.Year2021.Solvers.Day17;

public sealed class Day17Level2Solver : SolverWithArrayInput<Target, int>
{
	protected override int Solve(Target[] entries)
	{
		Target target = entries[0];
		var velocities = new HashSet<(int, int)>();
		var stepsToTargetCandidates = Enumerable.
			Range(1, 2 * Math.Max(Math.Abs(target.YRange.from), Math.Abs(target.YRange.to)));
		foreach (int steps in stepsToTargetCandidates)
		{
			FindForStepsToTarget(steps, target, velocities);
		}
		return velocities.Count;
	}

	private void FindForStepsToTarget(int steps, Target target, HashSet<(int, int)> velocities)
	{
		int velocityYFrom = (int)((double)target.YRange.from / steps + ((double)steps - 1) / 2);
		int velocityYTo = (int)((double)target.YRange.to / steps + ((double)steps - 1) / 2);
		if (velocityYFrom > velocityYTo)
		{
			(velocityYFrom, velocityYTo) = (velocityYTo, velocityYFrom);
		}

		var velocityYCandidates = Enumerable.Range(velocityYFrom, velocityYTo - velocityYFrom + 1);
		foreach (int velocityY in velocityYCandidates)
		{
			int targetY = steps * velocityY - steps * (steps - 1) / 2;
			if (target.YRange.from > targetY || targetY > target.YRange.to) continue;

			FindForStepsToTargetAndVelocityY(steps, velocityY, target, velocities);
		}
	}

	private void FindForStepsToTargetAndVelocityY(
		int steps,
		int velocityY,
		Target target,
		HashSet<(int, int)> velocities
		)
	{
		int velocityXFrom = (int)((double)target.XRange.from / steps + ((double)steps - 1) / 2);
		int velocityXTo = (int)((double)target.XRange.to / steps + ((double)steps - 1) / 2);
		if (velocityXFrom > velocityXTo) velocityXTo = velocityXFrom;
		if (velocityXTo < 0) return;

		var velocityXCandidates = Enumerable.Range(0, velocityXTo + 1);
		foreach (var velocityX in velocityXCandidates)
		{
			int targetX = steps >= velocityX ?
				velocityX * (velocityX + 1) / 2 :
				velocityX * steps - steps * (steps - 1) / 2;
			if (target.XRange.from > targetX || targetX > target.XRange.to) continue;
			velocities.Add((velocityX, velocityY));
		}
	}
}
