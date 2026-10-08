using DbTerm.Cli;

namespace DbTerm.Cli.Tests;

public class CommandProcessorTests
{
    [Fact]
    public void Process_QuitCommand_SignalsExit()
    {
        var result = CommandProcessor.Process("\\q");
        Assert.True(result.ShouldExit);
    }

    [Fact]
    public void Process_OtherInput_EchoesItAndDoesNotExit()
    {
        var result = CommandProcessor.Process("SELECT 1");
        Assert.Equal("SELECT 1", result.Output);
        Assert.False(result.ShouldExit);
    }

    [Fact]
    public void Process_HelpCommand_ListsEachCommandAndDoesNotExit()
    {
        var result = CommandProcessor.Process("\\?");
        Assert.Contains("\\ld", result.Output);
        Assert.Contains("\\lt", result.Output);
        Assert.Contains("\\?", result.Output);
        Assert.Contains("\\q", result.Output);
        Assert.False(result.ShouldExit);
    }

    [Fact]
    public void Process_ListDatabasesCommand_DisplaysDatabasesAndDoesNotExit()
    {
        var result = CommandProcessor.Process("\\ld");
        Assert.Equal("List Databases", result.Output);
        Assert.False(result.ShouldExit);
    }

    [Fact]
    public void Process_ListTablesCommand_DisplaysTablesAndDoesNotExit()
    {
        var result = CommandProcessor.Process("\\lt");
        Assert.Equal("List Tables", result.Output);
        Assert.False(result.ShouldExit);
    }
}
