using Npgsql;
using DbTerm.Application;

namespace DbTerm.Infrastructure;

public class PostgresDatabaseProvider(string connectionString) : IDatabaseProvider
{
    private readonly string _connectionString = connectionString;

    public IReadOnlyList<string> ListDatabases()
    {
        var databases = new List<string>();

        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        using var cmd = new NpgsqlCommand(
            "SELECT datname FROM pg_database WHERE datistemplate = false", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            databases.Add(reader.GetString(0));
        }

        return databases;
    }
}
