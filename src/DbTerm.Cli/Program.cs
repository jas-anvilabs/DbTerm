using DbTerm.Cli;
using DbTerm.Infrastructure;

var connString = Environment.GetEnvironmentVariable("DBTERM_CONNSTRING")
                 ?? throw new InvalidOperationException(
                     "Set DBTERM_CONNSTRING to your PostgreSQL connection string.");
var provider = new PostgresDatabaseProvider(connString);
var processor = new CommandProcessor(provider);

while (true)
{
    Console.Write("DbTerm >> ");
    var input = Console.ReadLine();
    if (input is null)
    {
        break;
    }

    var result = processor.Process(input);
    if (result.Output.Length > 0)
    {
        Console.WriteLine(result.Output);
    }

    if (result.ShouldExit)
    {
        break;
    }
}
