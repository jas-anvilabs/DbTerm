using DbTerm.Application;
using DbTerm.Domain;

namespace DbTerm.Cli;

public class TableDescriptionReportPlainText(TableDescription tableDescription) : ITableDescriptionReport
{
    private readonly TableDescription _tableDescription = tableDescription;

    public string Report()
    { 
        return
            $"""
             Description of table {_tableDescription.Table.Name}

             Columns In Table:

             {BuildColumnInfoTable()}
             """;
    }

    private string BuildColumnInfoTable()
    {
        var columnInfoTable = string.Empty;

        columnInfoTable += "----------------------------------\n";
        columnInfoTable += "| Name | Data Type | Is Nullable |\n";
        columnInfoTable += "----------------------------------\n";

        return _tableDescription.Columns.Aggregate(columnInfoTable, (current, columnInfo) =>
            current + $"| {columnInfo.Name} | {columnInfo.DataType} | {columnInfo.Nullable} |\n");
    }
}
