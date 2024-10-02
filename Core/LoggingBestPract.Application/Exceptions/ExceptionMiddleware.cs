using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using SendGrid.Helpers.Errors.Model;
using Serilog;
using LoggingBestPract.Application.Interfaces.Logging;
using ValidationException = FluentValidation.ValidationException;

namespace LoggingBestPract.Application.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILoggingHolder _loggingHolder;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ExceptionMiddleware(ILoggingHolder loggingHolder)
        {
            _loggingHolder = loggingHolder;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext); // Bir sonraki middleware'e geç
            }
            catch (Exception ex)
            {
                Log.Error("{@TrackId} {@description} {@context}",
                    _loggingHolder.TrackId,
                    "Bir hata meydana geldi",
                    ex);
                // Hata loglama
                logger.Error(ex, "An unhandled exception has occurred: {Message}", ex.Message);
                Log.Error(ex, "An unhandled exception has occurred: {Message}", ex.Message);
                await HandleExceptionAsync(_loggingHolder.TrackId,httpContext, ex); // Hata durumunu işle
            }
        }

        private static Task HandleExceptionAsync(Guid trackId ,HttpContext httpContext, Exception exception)
        {
            int statusCode = GetStatusCode(exception); // Hatanın durum kodunu al
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            Log.Error("{@TrackId} {@ExceptionType} {@Description} {@exceptionMessage}",
                trackId,
                exception.GetType().FullName, 
                "An error occurred while processing the request: {Message}", 
                exception.Message);
            
            logger.Error("{@TrackId} {@ExceptionType} {@Description} {@exceptionMessage}",
                trackId,
                exception.GetType().FullName, 
                "An error occurred while processing the request: {Message}", 
                exception.Message);           
            
            List<string> errors = new()
            {
                $"Hata Mesajı: {exception.Message}"
            };

            // FluentValidation hatası için özel durum
            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors.Select(x => x.ErrorMessage).ToList();
                return httpContext.Response.WriteAsync(new ExceptionModel
                {
                    Errors = errors,
                    StatusCode = StatusCodes.Status400BadRequest
                }.ToString());
            }

            // Diğer hatalar için hata yanıtı

            return httpContext.Response.WriteAsync(new ExceptionModel
            {
                Errors = errors,
                StatusCode = statusCode
            }.ToString());
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}
