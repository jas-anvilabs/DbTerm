using DbTerm.Application;

namespace DbTerm.Cli.Tests;

public class FakeDatabaseProvider : IDatabaseProvider
{
    public IReadOnlyList<string> ListDatabases() => ["db_alpha", "db_beta"];
    public IReadOnlyList<string> ListTables() => ["tbl_alpha", "tbl_beta"];

    public QueryResult ExecuteQuery(string sql) =>
        new QueryResult(
            Columns: ["id", "name"],
            Rows: [["1", "alpha"], ["2", "beta"]]
        );
}
