using DbTerm.Application;

namespace DbTerm.Cli.Tests;

public class FakeDatabaseProvider : IDatabaseProvider
{
    public IReadOnlyList<string> ListDatabases() => ["alpha", "beta"];
}
