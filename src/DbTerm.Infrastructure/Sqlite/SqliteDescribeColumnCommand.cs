using DbTerm.Domain;

namespace DbTerm.Infrastructure.Sqlite;

public static class SqliteDescribeColumnCommand
{
    public static ColumnDescription Describe(string connectionString, string table, string column)
    {
        var columnInfo = SqliteColumnInspector.Inspect(connectionString, table, column);
        return new ColumnDescription(columnInfo);
    }
}