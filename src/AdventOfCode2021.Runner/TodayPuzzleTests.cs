using AdventOfCode.Year2021.Solvers;

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
			Solve<byte[], int>(2021, 15, 1).
			AssertingResult(Parsing.ParseToDigitsArray).
			Run(_output);
		
		await Runner.
			Solve<byte[], int>(2021, 15, 2).
			AssertingResult(Parsing.ParseToDigitsArray).
			Run(_output);
	}
}
