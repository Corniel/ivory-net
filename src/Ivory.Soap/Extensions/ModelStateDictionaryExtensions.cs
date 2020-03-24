using Ivory.Soap.Modelbinding;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Ivory.Soap.Extensions
{
    /// <summary>Extensions on the <see cref="ModelStateDictionary"/>.</summary>
    internal static class ModelStateDictionaryExtensions
    {
        /// <summary>Get all <see cref="BindingError"/>'s from the mode state.</summary>
        /// <param name="modelState">
        /// The model state.
        /// </param>
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
