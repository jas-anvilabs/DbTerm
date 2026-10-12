using DbTerm.Application;
using DbTerm.Cli;
using DbTerm.Domain;
using DbTerm.Infrastructure.PostgreSQL;
using DbTerm.Infrastructure.Sqlite;

var config = DbTermApp.Init();

var parsedCliArgs = new CliParser(args).Parse();
var dataSourceProfile = new DataSourceProfileLoader(
    config.DataSourceProfilePath).Load(parsedCliArgs.DataSourceProfileName);

if (dataSourceProfile is null)
{
    Console.WriteLine($"Profile '{parsedCliArgs.DataSourceProfileName}' was not found.");
    return;
}

IDatabaseProvider? provider = null;
switch (dataSourceProfile.Provider)
{
    case DatabaseProvider.PostgreSQL:
        provider = new PostgresDatabaseProvider(dataSourceProfile);
        break;
    case DatabaseProvider.Sqlite:
        provider = new SqliteDatabaseProvider(dataSourceProfile);
        break;
}

if (provider is null)
{
    Console.WriteLine($"Provider '{dataSourceProfile.Provider}' is not supported.");
    return;
}

var processor = new CommandProcessor(provider);

new DbTermTui(processor).Run();
