using DbTerm.Cli;

namespace DbTerm.Cli.Tests;

public class CommandProcessorTests
{
    private static CommandProcessor CreateProcessor() =>
        new(new FakeDatabaseProvider());

    [Fact]
    public void Process_QuitCommand_SignalsExit()
    {
        var result = CreateProcessor().Process("\\q");
        Assert.True(result.ShouldExit);
    }

    [Fact]
    public void Process_OtherInput_EchoesItAndDoesNotExit()
    {
        var result = CreateProcessor().Process("SELECT 1");
        Assert.Equal("SELECT 1", result.Output);
        Assert.False(result.ShouldExit);
    }

    [Fact]
    public void Process_HelpCommand_ListsEachCommandAndDoesNotExit()
    {
        var result = CreateProcessor().Process("\\?");
        Assert.Contains("\\ld", result.Output);
        Assert.Contains("\\lt", result.Output);
        Assert.Contains("\\?", result.Output);
        Assert.Contains("\\q", result.Output);
        Assert.False(result.ShouldExit);
    }

    [Fact]
    public void Process_ListDatabasesCommand_DisplaysDatabasesAndDoesNotExit()
    {
        var result = CreateProcessor().Process("\\ld");
        Assert.Contains("alpha", result.Output);
        Assert.Contains("beta", result.Output);
        Assert.False(result.ShouldExit);
    }

    [Fact]
    public void Process_ListTablesCommand_DisplaysTablesAndDoesNotExit()
    {
        var result = CreateProcessor().Process("\\lt");
        Assert.Equal("List Tables", result.Output);
        Assert.False(result.ShouldExit);
    }
}
