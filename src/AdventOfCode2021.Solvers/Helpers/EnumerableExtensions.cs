﻿namespace AdventOfCode.Year2021.Solvers;

internal static class EnumerableExtensions
{
	public static async IAsyncEnumerable<TResult> LookBackSelect<TSource, TResult>(
		this IAsyncEnumerable<TSource> source,
		Func<TSource, TSource?, TResult> projection,
		TSource? beforeFirst = default
		) where TSource : struct
	{
		var iterator = source.GetAsyncEnumerator();
		if (!await iterator.MoveNextAsync()) yield break;

		TSource previous = iterator.Current;
		yield return projection(previous, beforeFirst);
		while (await iterator.MoveNextAsync())
		{
			yield return projection(iterator.Current, previous);
			previous = iterator.Current;
		}
	}

	public static IEnumerable<TResult> LookBackSelect<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource?, TResult> projection,
		TSource? beforeFirst = default
		) where TSource : struct
	{
		using IEnumerator<TSource> iterator = source.GetEnumerator();
		if (!iterator.MoveNext()) yield break;

		TSource previous = iterator.Current;
		yield return projection(previous, beforeFirst);
		while (iterator.MoveNext())
		{
			yield return projection(iterator.Current, previous);
			previous = iterator.Current;
		}
	}

	public static IEnumerable<TResult> LookBackSelectInner<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource?, TResult> projection,
		TSource? beforeFirst
		) where TSource : class
	{
		using IEnumerator<TSource> iterator = source.GetEnumerator();
		if (!iterator.MoveNext()) yield break;
		
		TSource previous = iterator.Current;
		yield return projection(previous, beforeFirst);
		while (iterator.MoveNext())
		{
			yield return projection(iterator.Current, previous);
			previous = iterator.Current;
		}
	}
}