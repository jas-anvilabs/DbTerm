namespace DbTerm.Infrastructure.Sqlite;

public static class SqliteInspectColumnQuery
{
    public static readonly string SingleColumn =
        """
        SELECT *
        FROM pragma_table_info(@table)
        WHERE name = @column
        """;

    public static readonly string AllColumns =
        """
        SELECT *
        FROM pragma_table_info(@table)
        """;
}