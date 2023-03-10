using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Extensions;

internal static class Extension
{
    public static string GetValidationErrors(this ModelStateDictionary model)
    {
        return string.Join(" | ", model.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));
    }

}
