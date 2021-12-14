namespace AdventOfCode.Year2021.Solvers.Day14;

public interface IInputEntry {}

public sealed record SeparatorEntry : IInputEntry;

public sealed record TemplateEntry(char[] Template) : IInputEntry;

public sealed record RuleEntry(char Left, char Right, char ToInsert) : IInputEntry; 
