using DbTerm.Application;
using DbTerm.Domain;
using Microsoft.Data.Sqlite;

namespace DbTerm.Infrastructure.Sqlite;

public class SqliteDatabaseProvider : IDatabaseProvider
{
    private readonly string _connectionString;
    private readonly string _dataSource;

    public SqliteDatabaseProvider(DataSourceProfile dataSourceProfile)
    {
        _connectionString = ConnectionString(dataSourceProfile);
        _dataSource = dataSourceProfile.Database;
    }

    public string ConnectionString(DataSourceProfile dataSourceProfile)
    {
        var connectionString =
            $"Data Source={dataSourceProfile.Database}";

        return connectionString;
    }

    public IReadOnlyList<string> ListDatabases()
    {
        IReadOnlyList<string> database = new List<string> { $"{_dataSource}" };
        return database;
    }

    public IReadOnlyList<string> ListTables() =>
        QuerySingleColumn(
            "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%'");

    private IReadOnlyList<string> QuerySingleColumn(string sql)
    {
        var results = new List<string>();

        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = new SqliteCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(reader.GetString(0));
        }

        return results;
    }

    public QueryResult ExecuteQuery(string sql)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = new SqliteCommand(sql, conn);
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

    public TableDescription DescribeTable(string table)
    {
        return SqliteDescribeTableCommand.Describe(_connectionString, table);
    }

    public ColumnDescription DescribeColumn(string table, string column)
    {
        return SqliteDescribeColumnCommand.Describe(_connectionString, table, column);
    }
}
