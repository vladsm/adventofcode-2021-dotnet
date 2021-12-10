using AdventOfCode.Year2021.Solvers;
using AdventOfCode.Year2021.Solvers.Day10;

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
			Solve<char[], int>(2021, 10, 1).
			AssertingResult(Parsing.ParseToCharactersArray).
			Run(_output);
		
		await Runner.
			Solve<char[], long>(2021, 10, 2).
			AssertingResult(Parsing.ParseToCharactersArray).
			Run(_output);
	}
}
