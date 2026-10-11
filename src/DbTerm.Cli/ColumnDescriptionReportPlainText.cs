using DbTerm.Application;
using DbTerm.Domain;

namespace DbTerm.Cli;

public class ColumnDescriptionReportPlainText(ColumnDescription columnDescription) : IColumnDescriptionReport
{
    private readonly ColumnDescription _columnDescription = columnDescription;

    public string Report()
    {
        return
            $"""
             Column Info:

             {BuildColumnInfoTable()}
             """;
    }

    private string BuildColumnInfoTable()
    {
        var columnInfoTable = string.Empty;

        columnInfoTable += "----------------------------------\n";
        columnInfoTable += "| Name | Data Type | Is Nullable |\n";
        columnInfoTable += "----------------------------------\n";

        return columnInfoTable +=
            $"| {columnDescription.Column.Name} | {columnDescription.Column.DataType} | {columnDescription.Column.Nullable} |\n";
    }
}