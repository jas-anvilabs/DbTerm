namespace DbTerm.Domain;

public record TableDescription(
    TableInfo Table,
    IReadOnlyList<ColumnInfo> Columns);
