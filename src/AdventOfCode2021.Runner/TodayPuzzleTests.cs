using AdventOfCode.Year2021.Solvers.Day9;

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
		// var input = new[]
		// {
		// 	Helpers.ParseLine("2199943210"),
		// 	Helpers.ParseLine("3987894921"),
		// 	Helpers.ParseLine("9856789892"),
		// 	Helpers.ParseLine("8767896789"),
		// 	Helpers.ParseLine("9899965678")
		// };
		//
		// await AdventOfCode.
		// 	SolveUsing(new Day09Level2Solver()).
		// 	WithInput(input).
		// 	ObservingResultWith(r => _output.WriteLine($"Result: {r}")).
		// 	Run();

		await Runner.
			Solve<byte[], int>(2021, 9, 1).
			AssertingResult(Helpers.ParseLine).
			Run(_output);

		await Runner.
			Solve<byte[], int>(2021, 9, 2).
			AssertingResult(Helpers.ParseLine).
			Run(_output);
	}
}
