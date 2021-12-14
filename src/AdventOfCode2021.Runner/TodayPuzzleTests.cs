using AdventOfCode.Year2021.Solvers.Day14;

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
			Solve<IInputEntry, long>(2021, 14, 1).
			AssertingResult(Helpers.ParseLine).
			Run(_output);
		
		await Runner.
			Solve<IInputEntry, long>(2021, 14, 2).
			AssertingResult(Helpers.ParseLine).
			Run(_output);
	}
}
