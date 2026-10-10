using DbTerm.Domain;
using Npgsql;

namespace DbTerm.Infrastructure;

public static class DescribeTableCommand
{
    public static TableDescription Describe(string connectionString, string table)
    {
        var tableInfo = TableInspector.Inspect(connectionString, table);
        var columnInfo = InspectColumns(connectionString, table);
        return new TableDescription(tableInfo, columnInfo);
    }

    private static IReadOnlyList<ColumnInfo> InspectColumns(string connectionString, string table)
    {
        var query = InspectColumnQuery.AllColumns;

        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        using var cmd = new NpgsqlCommand(query, conn);
        cmd.Parameters.AddWithValue("schema", "public");
        cmd.Parameters.AddWithValue("table", table);
        using var reader = cmd.ExecuteReader();

        var columnInfo = new List<ColumnInfo>();
        while (reader.Read())
        {
            var name = reader.GetString(reader.GetOrdinal("column_name"));
            var dataType = reader.GetString(reader.GetOrdinal("data_type"));
            var isNullable = reader.GetValue(reader.GetOrdinal("is_not_null")).ToString() != "t";

            columnInfo.Add(new ColumnInfo(name, dataType, isNullable));
        }

        return columnInfo;
    }
}
