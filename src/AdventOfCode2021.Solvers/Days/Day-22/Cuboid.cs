namespace AdventOfCode.Year2021.Solvers.Day22;

public record struct Cuboid(bool On, Range X, Range Y, Range Z);

public record struct Range(int From, int To);
