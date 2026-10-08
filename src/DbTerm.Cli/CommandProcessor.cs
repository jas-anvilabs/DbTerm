using DbTerm.Domain;

namespace DbTerm.Cli;

public class CommandProcessor
{
    public CommandResult Process(string input)
    {
        if (input == "\\q")
        {
            return new CommandResult("", ShouldExit: true);
        }

        return new CommandResult(input, ShouldExit: false);
    }
}
