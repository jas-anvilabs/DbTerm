using Tomlyn;

namespace DbTerm.Cli;

public static class DbTermConfigurationLoader
{
    public static DbTermConfiguration Load()
    {
        var appConfig = DbTermConfigurationPath.ConfigurationPath;

        var toml = File.ReadAllText(appConfig);

        return TomlSerializer.Deserialize<DbTermConfiguration>(toml);
    }
}
