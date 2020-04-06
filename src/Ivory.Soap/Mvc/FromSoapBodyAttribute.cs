using Ivory.Soap.Modelbinding;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Ivory.Soap.Mvc
{
    /// <summary>Specifies that a parameter should be bound using the SOAP body.</summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class FromSoapBodyAttribute : Attribute, IBindingSourceMetadata
    {
        /// <inheritdoc/>
        public BindingSource BindingSource => SoapBindingSource.SoapBody;
    }
}
