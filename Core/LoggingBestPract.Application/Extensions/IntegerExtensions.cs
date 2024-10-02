namespace LoggingBestPract.Application.Extensions;

public static class IntegerExtensions
{
    public static bool IsStatusCodeOk(this int val)
    {
        return val is >= 200 and <= 299;
    }
}