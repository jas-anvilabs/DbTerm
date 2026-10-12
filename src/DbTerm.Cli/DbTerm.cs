using Tomlyn;

namespace DbTerm.Cli;

public static class DbTermApp
{
    public static DbTermConfiguration Init()
    {
        var dbTermConfigPath = DbTermConfigurationPath.ConfigurationPath;

        if (!Path.Exists(dbTermConfigPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dbTermConfigPath));
            DefaultDbTermConfiguration.Write(dbTermConfigPath);
        }

        var toml = File.ReadAllText(dbTermConfigPath);
        var config = TomlSerializer.Deserialize<DbTermConfiguration>(toml);
        var profileStorePath = config.DataSourceProfilePath;

        if (!Path.Exists(profileStorePath))
        {
            Directory.CreateDirectory(profileStorePath);
        }

        return config;
    }
}
