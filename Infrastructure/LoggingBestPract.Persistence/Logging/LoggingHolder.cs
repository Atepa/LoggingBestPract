using LoggingBestPract.Application.Interfaces.Logging;

namespace LoggingBestPract.Persistence.Logging;

public class LoggingHolder : ILoggingHolder
{
    public DateTime StartedAt { get; set; }
    public Guid TrackId { get; set; } = Guid.NewGuid();
    public ICollection<ILoggingHolder.RequestDetail> RequestDetails { get; } = new List<ILoggingHolder.RequestDetail>();
}