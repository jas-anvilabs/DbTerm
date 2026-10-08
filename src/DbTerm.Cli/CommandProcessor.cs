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
            _ => new CommandResult(input, ShouldExit: false)
        };
    }
}
