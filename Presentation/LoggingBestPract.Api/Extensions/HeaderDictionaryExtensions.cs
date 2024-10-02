using System.Text;
using Microsoft.AspNetCore.Http;

namespace LoggingBestPract.WebAPI.Extensions;

public static class HeaderDictionaryExtensions
{
    public static string ConvertToString(this IHeaderDictionary headerDictionary)
    {
        var builder = new StringBuilder();
        foreach (var header in headerDictionary)
        {
            builder.Append($"'{header.Key}': '{string.Join(" ", header.Value)}' ");
        }
        return builder.ToString();
    }
}