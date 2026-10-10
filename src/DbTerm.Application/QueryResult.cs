namespace DbTerm.Application;

public record QueryResult(
    IReadOnlyList<string> Columns,
    IReadOnlyList<IReadOnlyList<string>> Rows
    );
