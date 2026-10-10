using DbTerm.Domain;
using Npgsql;

namespace DbTerm.Infrastructure;

public static class ColumnInspector
{
    public static ColumnInfo Inspect(string connectionString, string table, string column)
    {
        string query = InspectColumnQuery.SingleColumn;

        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        using var cmd = new NpgsqlCommand(query, conn);
        cmd.Parameters.AddWithValue("schema", "public");
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

        bool isNullable = columnInfo["is_not_null"] != "t";

        return new ColumnInfo(columnInfo["column_name"], columnInfo["data_type"], isNullable);
    }
}