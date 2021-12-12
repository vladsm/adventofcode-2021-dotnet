using AdventOfCode.Year2021.Solvers.Day12;

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
			Solve<(string, string), int>(2021, 12, 1).
			AssertingResult(Helpers.ParseLine).
			Run(_output);
		
		await Runner.
			Solve<(string, string), int>(2021, 12, 2).
			AssertingResult(Helpers.ParseLine).
			Run(_output);
	}
}
