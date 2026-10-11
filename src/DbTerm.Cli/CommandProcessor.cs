using DbTerm.Application;

namespace DbTerm.Cli;

public class CommandProcessor(IDatabaseProvider provider)
{
    public CommandResult Process(string input)
    {
        var (command, parameter1, parameter2) = ParseInput(input);
        return command switch
        {
            "\\q" => new CommandResult("", ShouldExit: true),
            "\\?" => new CommandResult(Help.Text, ShouldExit: false),
            "\\ld" => new CommandResult(string.Join(Environment.NewLine, provider.ListDatabases()), ShouldExit: false),
            "\\lt" => new CommandResult(string.Join(Environment.NewLine, provider.ListTables()), ShouldExit: false),
            "\\dt" => new CommandResult(new TableDescriptionReportPlainText(provider.DescribeTable(parameter1)).Report(), ShouldExit: false),
            "\\dc" => new CommandResult(new ColumnDescriptionReportPlainText(provider.DescribeColumn(parameter1, parameter2)).Report(), ShouldExit: false),
            _ => ExecuteSql(input)
        };
    }

    private (string Command, string Parameter1, string Parameter2) ParseInput(string input)
    {
        var parsedInput = input.Split(' ', 3);
        switch (parsedInput.Length)
        {
            case 1:
                return (Command: parsedInput[0], Parameter1: string.Empty, Parameter2: string.Empty);
            case 2:
                return (Command: parsedInput[0], Parameter1: parsedInput[1], Parameter2: string.Empty);
            case 3:
                return (Command: parsedInput[0], Parameter1: parsedInput[1], Parameter2: parsedInput[2]);
            default:
                return (Command: string.Empty, Parameter1: string.Empty, Parameter2: string.Empty);
        }
    }

    private CommandResult ExecuteSql(string sql)
    {
        try
        {
            var result = provider.ExecuteQuery(sql);
            return new CommandResult(FormatQueryResult(result), ShouldExit: false);
        }
        catch (Exception ex)
        {
            return new CommandResult($"Error: {ex.Message}", ShouldExit: false);
        }
    }

    private static string FormatQueryResult(QueryResult result)
    {
        if (result.Rows.Count == 0)
        {
            return "(0 rows)";
        }

        var lines = new List<string>();
        lines.Add(string.Join(" | ", result.Columns));
        lines.Add(new string('-', lines[0].Length));

        foreach (var row in result.Rows)
        {
            lines.Add(string.Join(" | ", row));
        }

        lines.Add($"({result.Rows.Count} rows)");
        return string.Join(Environment.NewLine, lines);
    }
}
