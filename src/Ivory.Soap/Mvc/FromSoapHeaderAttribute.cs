using Ivory.Soap.Modelbinding;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Ivory.Soap.Mvc
{
    /// <summary>Specifies that a parameter should be bound using the SOAP Header.</summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class FromSoapHeaderAttribute : Attribute, IBindingSourceMetadata
    {
        /// <inheritdoc/>
        public BindingSource BindingSource => SoapBindingSource.SoapHeader;
    }
}
