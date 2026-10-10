using Npgsql;
using DbTerm.Application;

namespace DbTerm.Infrastructure;

public class PostgresDatabaseProvider(string connectionString) : IDatabaseProvider
{
    private readonly string _connectionString = connectionString;

    public IReadOnlyList<string> ListDatabases() =>
        QuerySingleColumn(
            "SELECT datname FROM pg_database WHERE datistemplate = false");

    public IReadOnlyList<string> ListTables() =>
        QuerySingleColumn(
            "SELECT tablename FROM pg_tables WHERE schemaname NOT IN ('pg_catalog', 'information_schema')");

    private IReadOnlyList<string> QuerySingleColumn(string sql)
    {
        var results = new List<string>();

        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(reader.GetString(0));
        }

        return results;
    }

    public QueryResult ExecuteQuery(string sql)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        var columns = new List<string>();
        for (int i = 0; i < reader.FieldCount; i++)
        {
            columns.Add(reader.GetName(i));
        }

        var rows = new List<IReadOnlyList<string>>();
        while (reader.Read())
        {
            var row = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                row.Add(reader.IsDBNull(i) ? "NULL" : reader.GetValue(i)?.ToString() ?? "");
            }

            rows.Add(row);
        }

        return new QueryResult(columns, rows);
    }
}
