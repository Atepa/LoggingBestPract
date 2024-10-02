namespace LoggingBestPract.Application.Logging;

public class GraylogSinkApplicationOptions
{
    public string HostnameOrAddress { get; set; }
    public int Port { get; set; }
}