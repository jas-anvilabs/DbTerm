namespace DbTerm.Infrastructure.Sqlite;

public static class SqliteInspectTableQuery
{
    public static readonly string SingleTable =
        """
        SELECT *
        FROM pragma_table_info(@table)
        """;
}