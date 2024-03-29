﻿using AdventOfCode.Year2021.Solvers.Day23;

using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Year2021.Runner;

public class TodayPuzzleTests : PuzzleTestsBase
{
	private readonly ITestOutputHelper _output;

	public TodayPuzzleTests(ITestOutputHelper output)
	{
		_output = output;
	}

	[Fact(DisplayName = "Result should be accepted by site")]
	public async Task Result_should_be_accepted_by_site()
	{
		await Runner.
			Solve<IInputEntry, int>(2021, 23, 1).
			AssertingResult(Helpers.ParseInputLine).
			Run(_output);

		await Runner.
			Solve<IInputEntry, int>(2021, 23, 2).
			AssertingResult(Helpers.ParseInputLine).
			Run(_output);
	}
}
