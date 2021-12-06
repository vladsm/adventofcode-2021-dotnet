using AdventOfCode.Year2021.Solvers;
using AdventOfCode.Year2021.Solvers.Day4;

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
			Puzzle(2021, 4, 1).
			SolveUsing<IInputEntry, int, Day04Level1Solver>().
			ParsingInputWith(Helpers.ParseInputLine).
			ObservingInputWith((line, e) => _output.WriteLine($"{e}\n{line}\n")).
			Run();

		// await Runner.
		// 	Solve<IInputEntry, int>(2021, 4, 1).
		// 	AssertingResult(Helpers.ParseInputLine).
		// 	Run(_output);
	}
}
