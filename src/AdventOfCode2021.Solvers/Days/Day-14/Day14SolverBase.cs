namespace AdventOfCode.Year2021.Solvers.Day14;

using Template = Dictionary<(char left, char right), long>;
using Rules = Dictionary<(char, char), char>;

public abstract class Day14SolverBase : SolverWithArrayInput<IInputEntry, long>
{
	protected override long Solve(IInputEntry[] entries)
	{
		Rules rules = entries.
			OfType<RuleEntry>().
			ToDictionary(rule => (rule.Left, rule.Right), rule => rule.ToInsert);
		
		char[] templateElements = entries.
			OfType<TemplateEntry>().
			Select(e => e.Template).
			Single();
		char last = templateElements.Last();
		long[] elementsGroups = Enumerable.
			Range(0, NumberOfSteps).
			Aggregate(CreateTemplate(templateElements), applyRules).
			Append(new KeyValuePair<(char left, char), long>((last, '-'), 1)).
			GroupBy(e => e.Key.left, (_, g) => g.Sum(e => e.Value)).
			ToArray();
		return elementsGroups.Max() - elementsGroups.Min();
		
		Template applyRules(Template acc, int _)
		{
			return Apply(acc, rules);
		}
	}
	
	protected abstract int NumberOfSteps { get; }

	private static Template CreateTemplate(char[] templateElements)
	{
		return templateElements.
			LookBackSelect(toPair).
			SelectMany(elements => elements).
			GroupBy(p => p).
			ToDictionary(g => g.Key, g => (long)g.Count());

		(char, char)[] toPair(char right, char? left)
		{
			return left.HasValue ?
				new[] { (left.Value, right) } :
				Array.Empty<(char, char)>();
		}
	}

	private static Template Apply(Template template, Rules rules)
	{
		return template.
			Select(extend).
			SelectMany(_ => _).
			GroupBy(e => e.pair).
			ToDictionary(g => g.Key, g => g.Sum(e => e.value));

		((char, char) pair, long value)[] extend(KeyValuePair<(char, char), long> kvp)
		{
			(char left, char right) = kvp.Key;
			return rules.TryGetValue(kvp.Key, out char toInsert) ?
				new[] { ((left, toInsert), kvp.Value), ((toInsert, right), kvp.Value) } :
				new[] {(kvp.Key, kvp.Value)};
		}
	}
}
