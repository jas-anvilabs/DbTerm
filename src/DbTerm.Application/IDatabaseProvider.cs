namespace DbTerm.Application;

public interface IDatabaseProvider
{
    IReadOnlyList<string> ListDatabases();
}
