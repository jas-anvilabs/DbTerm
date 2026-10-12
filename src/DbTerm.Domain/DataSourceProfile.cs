namespace DbTerm.Domain;

public record DataSourceProfile
{
    public string Name { get; set; } = string.Empty;
    public DatabaseProvider Provider { get; set; } = DatabaseProvider.Unknown;
    public string HostOrIPAddress { get; set; } = string.Empty;
    public ushort Port { get; set; } = 0;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
}
