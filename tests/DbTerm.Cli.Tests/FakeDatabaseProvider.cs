using DbTerm.Application;
using DbTerm.Domain;

namespace DbTerm.Cli.Tests;

public class FakeDatabaseProvider : IDatabaseProvider
{
    public IReadOnlyList<string> ListDatabases() => ["db_alpha", "db_beta"];
    public IReadOnlyList<string> ListTables() => ["tbl_alpha", "tbl_beta"];

    public TableDescription DescribeTable(string table) =>
        new TableDescription(
            new TableInfo("fake_table", 1),
            [new ColumnInfo("id", "integer", false), new ColumnInfo("name", "varchar", true)]
            );

    public ColumnDescription DescribeColumn(string table, string column) =>
        new ColumnDescription(
            new ColumnInfo("id", "integer", false)
            );

    public QueryResult ExecuteQuery(string sql) =>
        new QueryResult(
            Columns: ["id", "name"],
            Rows: [["1", "alpha"], ["2", "beta"]]
        );
}
