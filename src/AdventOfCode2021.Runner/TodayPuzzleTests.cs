using AdventOfCode.Year2021.Solvers;
using AdventOfCode.Year2021.Solvers.Day11;
using AdventOfCode.Year2021.Solvers.Day2;

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
		// var input = new byte[][]
		// {
		// 	Parsing.ParseToDigitsArray("5483143223"),
		// 	Parsing.ParseToDigitsArray("2745854711"),
		// 	Parsing.ParseToDigitsArray("5264556173"),
		// 	Parsing.ParseToDigitsArray("6141336146"),
		// 	Parsing.ParseToDigitsArray("6357385478"),
		// 	Parsing.ParseToDigitsArray("4167524645"),
		// 	Parsing.ParseToDigitsArray("2176841721"),
		// 	Parsing.ParseToDigitsArray("6882881134"),
		// 	Parsing.ParseToDigitsArray("4846848554"),
		// 	Parsing.ParseToDigitsArray("5283751526")
		// };
		//
		// await AdventOfCode.
		// 	SolveUsing(new Day11Level2Solver()).
		// 	WithInput(input).
		// 	ObservingResultWith(r => _output.WriteLine($"Result: {r}")).
		// 	Run();

		await Runner.
			Solve<byte[], int>(2021, 11, 1).
			AssertingResult(Parsing.ParseToDigitsArray).
			Run(_output);

		await Runner.
			Solve<byte[], int>(2021, 11, 2).
			AssertingResult(Parsing.ParseToDigitsArray).
			Run(_output);
	}
}
