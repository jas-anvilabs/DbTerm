namespace DbTerm.Cli.Tests
{
    public class CommandProcessorTests
    {
        [Fact]
        public void Process_QuitCommand_SignalsExit()
        {
            var processor = new CommandProcessor();
            var result = processor.Process("\\q");
            Assert.True(result.ShouldExit);
        }

        [Fact]
        public void Process_OtherInput_EchoesItAndDoesNotExit()
        {
            var processor = new CommandProcessor();
            var result = processor.Process("SELECT 1");
            Assert.Equal("SELECT 1", result.Output);
            Assert.False(result.ShouldExit);
        }
    }
}
