using DbTerm.Cli;
using DbTerm.Domain;
using DbTerm.Infrastructure;

var config = DbTermApp.Init();

var parsedCliArgs = new CliParser(args).Parse();
var dataSourceProfile = new DataSourceProfileLoader(
    config.DataSourceProfilePath).Load(parsedCliArgs.DataSourceProfileName);

if (dataSourceProfile is null)
{
    Console.WriteLine($"Profile '{parsedCliArgs.DataSourceProfileName}' was not found.");
    return;
}

var provider = new PostgresDatabaseProvider(dataSourceProfile);
var processor = new CommandProcessor(provider);

new DbTermTui(processor).Run();