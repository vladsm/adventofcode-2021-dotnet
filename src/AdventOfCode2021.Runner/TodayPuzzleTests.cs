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
			Solve<char[], ulong>(2021, 16, 1).
			AssertingResult(Parsing.ParseToCharactersArray).
			Run(_output);
		
		await Runner.
			Solve<char[], ulong>(2021, 16, 2).
			AssertingResult(Parsing.ParseToCharactersArray).
			Run(_output);
	}
}
