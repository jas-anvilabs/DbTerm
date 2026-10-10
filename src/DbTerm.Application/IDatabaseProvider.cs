namespace DbTerm.Application;

public interface IDatabaseProvider
{
    IReadOnlyList<string> ListDatabases();
    IReadOnlyList<string> ListTables();
    QueryResult ExecuteQuery(string sql);
}
