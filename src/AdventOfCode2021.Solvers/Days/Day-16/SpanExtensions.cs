namespace AdventOfCode.Year2021.Solvers.Day16;

internal static class SpanExtensions
{
	public static Span<T> Read<T>(this Span<T> source, int length, out Span<T> readValue)
	{
		readValue = source[..length];
		return source[length..];
	}

	public static int ToInt(this Span<bool> bits)
	{
		int length = bits.Length;
		int result = 0;
		for (int i = 0; i < length; ++i)
		{
			if (!bits[length - 1 - i]) continue;
			result += (1 << i);
		}
		return result;
	}
}
