using System;
using LoggingBestPract.Domain;

namespace LoggingBestPract.Application.Utilities;

public static class Errors
{
    public const string FieldServiceUnknown = "Bilinmeyen kanal veya cihaz ile işlem yapılamamaktadır.";

    public static string UnknownError(Guid trackId)
    {
        return $"Bilinmeyen bir hata oluştu. Lütfen teknik ekip ile iletişime geçiniz. TrackId: {trackId}";
    }
    
}