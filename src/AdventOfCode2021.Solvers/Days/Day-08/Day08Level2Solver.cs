namespace AdventOfCode.Year2021.Solvers.Day8;

public sealed class Day08Level2Solver : IAsyncSolver<(string[], string[]), int>
{
	public async ValueTask<int> Solve(IAsyncEnumerable<(string[], string[])> entries)
	{
		return await entries.SumAsync(SolveEntry);
	}


	private int SolveEntry((string[], string[]) entry)
	{
		(string[] rawSamples, string[] output) = entry;

		HashSet<char>[] samples = rawSamples.Select(s => s.ToHashSet()).ToArray();

		var oneDigitSample = samples.First(s => s.Count == 2);
		var fourDigitSample = samples.First(s => s.Count == 4);
		var sevenDigitSample = samples.First(s => s.Count == 3);
		var eightDigitSample = samples.First(s => s.Count == 7);

		char aSegment = sevenDigitSample.Except(oneDigitSample).First();

		var fourDigitWithSegmentA = fourDigitSample.Concat(new[] { aSegment }).ToHashSet();
		char gSegment = samples.Select(s => s.Except(fourDigitWithSegmentA)).First(s => s.Count() == 1).First();
		
		var fourDigitWithSegmentsAg = fourDigitWithSegmentA.Concat(new[] { gSegment }).ToHashSet();
		char eSegment = eightDigitSample.Except(fourDigitWithSegmentsAg).First();

		var twoDigitSample = samples.Single(s => s.Count == 5 && s.Contains(eSegment));
		char bSegment = eightDigitSample.Except(twoDigitSample).Except(oneDigitSample).Single();
		char dSegment = eightDigitSample.Except(new[] { aSegment, bSegment, eSegment, gSegment }).Except(oneDigitSample).Single();
		char cSegment = twoDigitSample.Except(new[] { aSegment, dSegment, eSegment, gSegment }).Single();
		char fSegment = oneDigitSample.Except(new[] { cSegment }).Single();

		var decodingMap = new Dictionary<char, char>
		{
			{aSegment, 'a'},
			{bSegment, 'b'},
			{cSegment, 'c'},
			{dSegment, 'd'},
			{eSegment, 'e'},
			{fSegment, 'f'},
			{gSegment, 'g'}
		};

		return int.Parse(
			new string(output.Select(decode).Select(ToDigit).ToArray())
			);

		char[] decode(string encoded)
		{
		  return encoded.Select(s => decodingMap[s]).ToArray();
		}
	}

	private readonly HashSet<char> _oneSegments = new() { 'c', 'f' };
	private readonly HashSet<char> _twoSegments = new() { 'a', 'c', 'd', 'e', 'g' };
	private readonly HashSet<char> _threeSegments = new() { 'a', 'c', 'd', 'f', 'g' };
	private readonly HashSet<char> _fourSegments = new() { 'b', 'c', 'd', 'f' };
	private readonly HashSet<char> _fiveSegments = new() { 'a', 'b', 'd', 'f', 'g' };
	private readonly HashSet<char> _sixSegments = new() { 'a', 'b', 'd', 'e', 'f', 'g' };
	private readonly HashSet<char> _sevenSegments = new() { 'a', 'c', 'f' };
	private readonly HashSet<char> _eightSegments = new() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
	private readonly HashSet<char> _nineSegments = new() { 'a', 'b', 'c', 'd', 'f', 'g' };
	private readonly HashSet<char> _zeroSegments = new() { 'a', 'b', 'c', 'e', 'f', 'g' };

	private char ToDigit(char[] segments)
	{
		if (_oneSegments.SetEquals(segments)) return '1';
		if (_twoSegments.SetEquals(segments)) return '2';
		if (_threeSegments.SetEquals(segments)) return '3';
		if (_fourSegments.SetEquals(segments)) return '4';
		if (_fiveSegments.SetEquals(segments)) return '5';
		if (_sixSegments.SetEquals(segments)) return '6';
		if (_sevenSegments.SetEquals(segments)) return '7';
		if (_eightSegments.SetEquals(segments)) return '8';
		if (_nineSegments.SetEquals(segments)) return '9';
		if (_zeroSegments.SetEquals(segments)) return '0';
		throw new InvalidOperationException("Can not determine number");
	}
}
