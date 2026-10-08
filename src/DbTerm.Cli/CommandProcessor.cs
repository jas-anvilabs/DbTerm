using DbTerm.Application;

namespace DbTerm.Cli;

public class CommandProcessor
{
    private readonly IDatabaseProvider _provider;

    public CommandProcessor(IDatabaseProvider provider)
    {
        _provider = provider;
    }

    public CommandResult Process(string input)
    {
        return input switch
        {
            "\\q" => new CommandResult("", ShouldExit: true),
            "\\?" => new CommandResult(Help.Text, ShouldExit: false),
            "\\ld" => new CommandResult(string.Join(Environment.NewLine, _provider.ListDatabases()), ShouldExit: false),
            "\\lt" => new CommandResult("List Tables", ShouldExit: false),
            _ => new CommandResult(input, ShouldExit: false)
        };
    }
}
