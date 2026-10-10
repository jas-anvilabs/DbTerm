using DbTerm.Application;

namespace DbTerm.Cli;

public class CommandProcessor(IDatabaseProvider provider)
{
    public CommandResult Process(string input)
    {
        var (command, parameter) = ParseInput(input);
        return command switch
        {
            "\\q" => new CommandResult("", ShouldExit: true),
            "\\?" => new CommandResult(Help.Text, ShouldExit: false),
            "\\ld" => new CommandResult(string.Join(Environment.NewLine, provider.ListDatabases()), ShouldExit: false),
            "\\lt" => new CommandResult(string.Join(Environment.NewLine, provider.ListTables()), ShouldExit: false),
            "\\dt" => new CommandResult(new TableDescriptionReportPlainText(provider.DescribeTable(parameter)).Report(), ShouldExit: false),
            _ => ExecuteSql(input)
        };
    }

    private (string Command, string Parameter) ParseInput(string input)
    {
        var parsedInput = input.Split(' ', 2);
        if (parsedInput.Length == 2)
        {
            return (Command: parsedInput[0], Parameter: parsedInput[1]);
        }
        else
        {
            return (Command: parsedInput[0], Parameter: string.Empty);
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
