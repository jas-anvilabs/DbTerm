using DbTerm.Domain;
using Microsoft.Data.Sqlite;

namespace DbTerm.Infrastructure.Sqlite;

public static class SqliteDescribeTableCommand
{
    public static TableDescription Describe(string connectionString, string table)
    {
        var tableInfo = SqliteTableInspector.Inspect(connectionString, table);
        var columnInfo = InspectColumns(connectionString, table);
        return new TableDescription(tableInfo, columnInfo);
    }

    private static IReadOnlyList<ColumnInfo> InspectColumns(string connectionString, string table)
    {
        var query = SqliteInspectColumnQuery.AllColumns;

        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        using var cmd = new SqliteCommand(query, conn);
        cmd.Parameters.AddWithValue("table", table);
        using var reader = cmd.ExecuteReader();

        var columnInfo = new List<ColumnInfo>();
        while (reader.Read())
        {
            var name = reader.GetString(reader.GetOrdinal("name"));
            var dataType = reader.GetString(reader.GetOrdinal("type"));
            var isNullable = reader.GetValue(reader.GetOrdinal("notnull")).ToString() != "t";

            columnInfo.Add(new ColumnInfo(name, dataType, isNullable));
        }

        return columnInfo;
    }
}