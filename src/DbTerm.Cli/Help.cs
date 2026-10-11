namespace DbTerm.Cli;

public static class Help
{
    public static readonly string Text =
        """
        DbTerm Help
        ===========
        
        \dt <table> - Describe Table
        \dc <table> <column> - Describe Column
        ****************************
        \ld - List Databases
        \lt - List Tables
        ****************************
        \?  - Help
        \q  - Quit
        """;
}
