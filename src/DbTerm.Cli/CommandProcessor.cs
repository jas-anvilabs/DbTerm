using DbTerm.Application;

namespace DbTerm.Cli;

public class CommandProcessor(IDatabaseProvider provider)
{
    public CommandResult Process(string input)
    {
        return input switch
        {
            "\\q" => new CommandResult("", ShouldExit: true),
            "\\?" => new CommandResult(Help.Text, ShouldExit: false),
            "\\ld" => new CommandResult(string.Join(Environment.NewLine, provider.ListDatabases()), ShouldExit: false),
            "\\lt" => new CommandResult(string.Join(Environment.NewLine, provider.ListTables()), ShouldExit: false),
            _ => ExecuteSql(input)
        };
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
