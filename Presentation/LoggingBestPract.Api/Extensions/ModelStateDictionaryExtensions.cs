using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LoggingBestPract.WebAPI.Extensions;

public static class ModelStateDictionaryExtensions
{
    public static string ToErrors(this ModelStateDictionary modelStateDictionary)
    {
        var errors = new StringBuilder();
        foreach (var state in modelStateDictionary)
        {
            foreach (var error in state.Value.Errors)
            {
                errors.Append($"{error.ErrorMessage} ");
            }
        }

        return errors.ToString();
    }
}