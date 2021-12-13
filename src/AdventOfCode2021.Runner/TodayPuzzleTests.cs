using AdventOfCode.Year2021.Solvers.Day13;

using Xunit;
using Xunit.Abstractions;

using Helpers = AdventOfCode.Year2021.Solvers.Day13.Helpers;

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
			Solve<IInputEntry, int>(2021, 13, 1).
			AssertingResult(Helpers.ParseLine).
			Run(_output);
		
		await Runner.
			Solve<IInputEntry, Dot[]>(2021, 13, 2).
			ParsingInputWith(Helpers.ParseLine).
			ObservingResultWith(write).
			Run(_output);

		void write(Dot[] dots)
		{
			foreach (string line in Day13Level2Solver.ToOutputLines(dots))
			{
				_output.WriteLine(line);
			}
		}
	}
}
