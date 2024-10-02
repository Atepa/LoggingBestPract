using System;
using System.Text;
using Microsoft.Extensions.Logging;
using LoggingBestPract.Application.Interfaces.Logging;

namespace LoggingBestPract.Application.Extensions;

public static class LoggerExtensions
{
    public static ILoggingHolder.RequestDetail LogRequest(
        this ILogger logger,
        ILoggingHolder loggingHolder,
        string path,
        object request)
    {
        var requestDetail = new ILoggingHolder.RequestDetail
        {
            Path = path,
            StartedAt = DateTime.Now
        };

        loggingHolder?.RequestDetails.Add(requestDetail);

        logger.LogInformation("{@TrackId} {@Path} {@RequestBody}",
            loggingHolder?.TrackId, 
            requestDetail.Path,
            request);


        return requestDetail;
    }

    public static void LogResponse(
        this ILogger logger,
        ILoggingHolder loggingHolder,
        ILoggingHolder.RequestDetail requestDetail,
        object response)
    {
        if (requestDetail is null)
        {
            return;
        }

        requestDetail.FinishedAt = DateTime.Now;

        logger.LogInformation("{@TrackId} {@Path} {@Response}", 
            loggingHolder?.TrackId, 
            requestDetail.Path,
            response);
    }

    public static void LogProcessTime(
        this ILogger logger,
        ILoggingHolder loggingHolder,
        int statusCode)
    {
        var sb = new StringBuilder();
        sb.Append("Request/Response Time(s) - ");

        foreach (var requestDetail in loggingHolder.RequestDetails)
        {
            sb.Append($"[{requestDetail.Path}: {(requestDetail.FinishedAt.HasValue ? $"{(requestDetail.FinishedAt.Value - requestDetail.StartedAt).TotalMilliseconds:0} ms] " : "?] ")}");
        }

        sb.Append($"[Total: {(DateTime.Now - loggingHolder.StartedAt).TotalMilliseconds:0} ms]");

        logger.LogInformation("{@TrackId} {@StatusCode} {@IsSuccess} {@Context}",
            loggingHolder.TrackId,
            statusCode,
            statusCode.IsStatusCodeOk(),
            sb.ToString());
    }
}