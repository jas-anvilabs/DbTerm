using System.CommandLine;

namespace DbTerm.Cli;

public class CliParser(string[] args)
{
    public ParsedCommandLineArgs Parse()
    {
        var profileArgument = new Argument<string>(name: "profile")
        {
            Description = "The Data Source Profile to connect to."
        };

        var rootCommand = new RootCommand(
            "A multi-vendor database access and monitoring tool") { profileArgument };

        ParseResult parseResult = rootCommand.Parse(args);

        return new ParsedCommandLineArgs(DataSourceProfileName: parseResult.GetValue(profileArgument));
    }
}
