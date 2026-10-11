using DbTerm.Domain;

namespace DbTerm.Application;

public interface IDatabaseProvider
{
    IReadOnlyList<string> ListDatabases();
    IReadOnlyList<string> ListTables();
    QueryResult ExecuteQuery(string sql);
    TableDescription DescribeTable(string table);
    ColumnDescription DescribeColumn(string table, string column);
}
