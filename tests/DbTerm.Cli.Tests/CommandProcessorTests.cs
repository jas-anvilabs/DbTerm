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
        Assert.Contains("db_alpha", result.Output);
        Assert.Contains("db_beta", result.Output);
        Assert.False(result.ShouldExit);
    }

    [Fact]
    public void Process_ListTablesCommand_DisplaysTablesAndDoesNotExit()
    {
        var result = CreateProcessor().Process("\\lt");
        Assert.Contains("tbl_alpha", result.Output);
        Assert.Contains("tbl_beta", result.Output);
        Assert.False(result.ShouldExit);
    }

    [Fact]
    public void Process_DescribeTableCommand_DisplaysTableAndColumnInfoAndDoesNotExit()
    {
        var result = CreateProcessor().Process("\\dt fake_table");
        Assert.Contains("fake_table", result.Output);
        Assert.Contains("id", result.Output);
        Assert.False(result.ShouldExit);
    }
}
