namespace AdventOfCode.Year2021.Solvers;

public sealed class Day03Level2Solver : Day03SolverBase
{
	public override async ValueTask<int> Solve(IAsyncEnumerable<bool[]> entriesSource)
	{
		bool[][] entries = await entriesSource.ToArrayAsync();

		int oxygenRating = FindRating(entries, true, 0);
		int co2Rating = FindRating(entries, false, 0);
		return oxygenRating * co2Rating;
	}

	private int FindRating(bool[][] entries, bool detectOxygen, int index)
	{
		return entries.Length == 1 ?
			ToNumber(entries[0]) :
			FindRating(FilterByBitIndex(entries, detectOxygen, index), detectOxygen, index + 1);
	}
	
	private bool[][] FilterByBitIndex(bool[][] entries, bool detectOxygen, int index)
	{
		int bitsSum = entries.Aggregate(0, calculateBitsSum);
		bool bitToMatch = detectOxygen ? bitsSum >= 0 : bitsSum < 0;
		return entries.Where(e => e[index] == bitToMatch).ToArray();
		
		int calculateBitsSum(int sum, bool[] entry)
		{
			bool bit = entry[index];
			return bit ? sum + 1 : sum - 1;
		}
	}
}
