using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Qola.API.Shared.Extensions;

public static class ModelStateExtensions
{
    //Validation extension method to check if ModelState is valid or not 
    public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
    {
        return dictionary.SelectMany(m => m.Value.Errors)
            .Select(m => m.ErrorMessage)
            .ToList();
    } 
}