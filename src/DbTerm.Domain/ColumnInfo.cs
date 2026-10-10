namespace DbTerm.Domain;

public record ColumnInfo(
    string Name,
    string DataType,
    bool Nullable);
