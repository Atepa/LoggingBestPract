using System;
using System.Collections.Generic;

namespace LoggingBestPract.Application.Interfaces.Logging;

public interface ILoggingHolder
{
    DateTime StartedAt { get; set; }
    Guid TrackId { get; }
    ICollection<RequestDetail> RequestDetails { get; }

    public class RequestDetail
    {
        public string Path { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}