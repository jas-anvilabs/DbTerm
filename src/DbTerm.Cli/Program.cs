using DbTerm.Cli;
using DbTerm.Infrastructure;

var connString = Environment.GetEnvironmentVariable("DBTERM_CONNSTRING")
                 ?? throw new InvalidOperationException(
                     "Set DBTERM_CONNSTRING to your PostgreSQL connection string.");
var provider = new PostgresDatabaseProvider(connString);
var processor = new CommandProcessor(provider);

new DbTermTui(processor).Run();
