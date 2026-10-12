using DbTerm.Domain;
using Tomlyn;

namespace DbTerm.Cli;

public class DataSourceProfileLoader
{
    private List<string> _profileStore = new();

    public DataSourceProfileLoader(string dataSourceProfileFolder)
    {
        var profiles = Directory.GetFiles(dataSourceProfileFolder);
        foreach (var profile in profiles)
        {
            _profileStore.Add(profile);
        }
    }
    public DataSourceProfile? Load(string profileName)
    {
        var found =
            _profileStore.Find(p =>
                Path.GetFileNameWithoutExtension(p).Equals(
                    profileName, StringComparison.OrdinalIgnoreCase));

        if (found is not null)
        {
            if (Path.Exists(found))
            {
                var toml = File.ReadAllText(found);
                return TomlSerializer.Deserialize<DataSourceProfile>(toml);
            }
        }

        return null;
    }
}
