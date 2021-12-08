using AdventOfCode.Year2021.Solvers.Day6;

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
		 	Solve<byte[], long>(2021, 6, 1).
			AssertingResult(Helpers.ParseInitialTimers).
			Run(_output);
		
		 await Runner.
		 	Solve<byte[], long>(2021, 6, 2).
			AssertingResult(Helpers.ParseInitialTimers).
			Run(_output);
	}
}
