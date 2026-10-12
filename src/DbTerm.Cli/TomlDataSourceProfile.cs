using DbTerm.Domain;
using Tomlyn;

namespace DbTerm.Cli;

public class TomlDataSourceProfile
{
    public DataSourceProfile Read(string path)
    {
        var toml = File.ReadAllText(path);
        return TomlSerializer.Deserialize<DataSourceProfile>(toml);
    }

    public void Write(DataSourceProfile dataSourceProfile, string path)
    {
        
        var toml = TomlSerializer.Serialize(dataSourceProfile);
        File.WriteAllText(path, toml);
    }
}
