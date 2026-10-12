using Tomlyn;

namespace DbTerm.Cli;

public static class DefaultDbTermConfiguration
{
    public static void Write(string path)
    {
        var dataSourceProfileFolderRoot = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments);
        var dataSourceProfileFolder =
            Path.Join(dataSourceProfileFolderRoot, "DbTerm", "Data Source Profiles");

        var defaultConfig = new DbTermConfiguration
        {
            DataSourceProfilePath = dataSourceProfileFolder
        };

        var toml = TomlSerializer.Serialize(defaultConfig);
        File.WriteAllText(path, toml);
    }
}
