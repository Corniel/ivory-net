using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Ivory.Soap.Modelbinding
{
    internal static class ModelStateDictionaryExtensions
    {
        public static IEnumerable<BindingError> GetErrors(this ModelStateDictionary modelState)
        {
            foreach (var kvp in modelState)
            {
                foreach (var error in kvp.Value.Errors)
                {
                    yield return new BindingError(kvp.Key, error.ErrorMessage);
                }
            }
        }
    }
}
