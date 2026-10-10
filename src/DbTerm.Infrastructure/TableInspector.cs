using DbTerm.Domain;
using Npgsql;

namespace DbTerm.Infrastructure;

public static class TableInspector
{
    public static TableInfo Inspect(string connectionString, string table)
    {
        string query = InspectTableQuery.SingleTable;

        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        using var cmd = new NpgsqlCommand(query, conn);
        cmd.Parameters.AddWithValue("schema", "public");
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

        return new TableInfo(tableInfo["table_name"], (long)double.Parse(tableInfo["estimated_row_count"]));
    }
}
