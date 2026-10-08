namespace DbTerm.Cli;

public static class CommandProcessor
{
    public static CommandResult Process(string input)
    {
        return input switch
        {
            "\\q" => new CommandResult("", ShouldExit: true),
            "\\?" => new CommandResult(Help.Text, ShouldExit: false),
            "\\ld" => new CommandResult("List Databases", ShouldExit: false),
            "\\lt" => new CommandResult("List Tables", ShouldExit: false),
            _ => new CommandResult(input, ShouldExit: false)
        };
    }
}
