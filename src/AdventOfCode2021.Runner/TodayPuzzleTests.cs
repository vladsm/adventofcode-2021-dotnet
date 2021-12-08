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
		//await AdventOfCode.
		//	SolveUsing(new Day07Level2Solver()).
		//	WithInput(new[] { new[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 } }).
		//	ObservingResultWith(result => _output.WriteLine($"Result: {result}")).
		//	Run();

		await Runner.
			Solve<int[], int>(2021, 7, 1).
			AssertingResult(Parsing.ParseToIntArray).
			Run(_output);

		await Runner.
			Solve<int[], int>(2021, 7, 2).
			AssertingResult(Parsing.ParseToIntArray).
			Run(_output);
	}
}
