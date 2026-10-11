using DbTerm.Domain;

namespace DbTerm.Infrastructure;

public static class DescribeColumnCommand
{
    public static ColumnDescription Describe(string connectionString, string table, string column)
    {
        var columnInfo = ColumnInspector.Inspect(connectionString, table, column);
        return new ColumnDescription(columnInfo);
    }
}
