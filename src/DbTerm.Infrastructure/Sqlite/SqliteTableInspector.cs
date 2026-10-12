using DbTerm.Domain;
using Microsoft.Data.Sqlite;

namespace DbTerm.Infrastructure.Sqlite;

public static class SqliteTableInspector
{
    public static TableInfo Inspect(string connectionString, string table)
    {
        string query = $"SELECT COUNT(*) AS 'rowcount' FROM {table}";

        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        using var cmd = new SqliteCommand(query, conn);
        cmd.Parameters.AddWithValue("table", table);
        using var reader = cmd.ExecuteReader();

        var tableInfo = new Dictionary<string, string>();
        if (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                tableInfo.Add(reader.GetName(i), reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString() ?? "");
            }
        }

        return new TableInfo(table, long.Parse(tableInfo["rowcount"]));
    }
}