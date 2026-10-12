namespace DbTerm.Cli;

    public static class DbTermConfigurationPath
    {
        private static readonly string Root = Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData);

        public static readonly string ConfigurationPath = Path.Join(
            Root, "DbTerm", "Configuration.toml");
    }
