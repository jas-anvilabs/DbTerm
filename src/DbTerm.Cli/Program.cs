using DbTerm.Cli;

var processor = new CommandProcessor();

while (true)
{
    Console.Write("DbTerm >> ");
    var input = Console.ReadLine();
    if (input is null)
    {
        break;
    }

    var result = processor.Process(input);
    if (result.Output.Length > 0)
    {
        Console.WriteLine(result.Output);
    }

    if (result.ShouldExit)
    {
        break;
    }
}
