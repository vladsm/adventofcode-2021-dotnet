using AdventOfCode;
using AdventOfCode.Year2021.Solvers;

Console.WriteLine("Hello, World!");

string sessionToken = "53616c7465645f5f6ea7cf350cf8f73486c178d55b94756da746f42896e323913c65387017809619cb81e5b2d6ae1a34";

var httpHandler = new SocketsHttpHandler
{
	PooledConnectionLifetime = TimeSpan.FromHours(1),
	PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1)
};
var httpClient = new HttpClient(httpHandler);
httpClient.DefaultRequestHeaders.Add("cookie", "session=" + sessionToken);

var runner = new SiteRunner(new Uri("https://adventofcode.com"), httpClient, sessionToken);

var test = await httpClient.GetStringAsync("http://ya.ru");

try
{
	await runner.Test();

	await runner.
		SolveUsing(new Day01Part1Solver()).
		ParsingBy(Convert.ToInt32).
		ObservingResultWith(write).
		Run();
}
catch (Exception exception)
{
	throw;
}

static void write(int result)
{
	Console.WriteLine($"Result: {result}");
}

