using DbTerm.Domain;
using Microsoft.Data.Sqlite;

namespace DbTerm.Infrastructure.Sqlite;

public static class SqliteColumnInspector
{
    public static ColumnInfo Inspect(string connectionString, string table, string column)
    {
        string query = SqliteInspectColumnQuery.SingleColumn;

        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        using var cmd = new SqliteCommand(query, conn);
        cmd.Parameters.AddWithValue("table", table);
        cmd.Parameters.AddWithValue("column", column);
        using var reader = cmd.ExecuteReader();

        var columnInfo = new Dictionary<string, string>();
        if (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columnInfo.Add(reader.GetName(i), reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString() ?? "");
            }
        }

        bool isNullable = columnInfo["notnull"] != "1";

        return new ColumnInfo(columnInfo["name"], columnInfo["type"], isNullable);
    }
}
