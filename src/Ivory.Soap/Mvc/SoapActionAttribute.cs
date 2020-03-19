using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;

namespace Ivory.Soap.Mvc
{
    /// <summary>Represents a SOAP action method.</summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class SoapActionAttribute : HttpPostAttribute, IActionConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoapActionAttribute"/> class.
        /// </summary>
        /// <param name="action">
        /// The SOAP action.
        /// </param>
        public SoapActionAttribute(string action) => Action = action;

        /// <summary>Gets the SOAP action.</summary>
        public string Action { get; }

        /// <inheritdoc/>
        public bool Accept(ActionConstraintContext context)
        {
            Guard.NotNull(context, nameof(context));

            return context.RouteContext.HttpContext.Request.Headers.TryGetValue(SoapMessage.SOAPAction, out var values)
                && values.Count == 1
                && values[0] == Action;
        }

        /// <inheritdoc/>
        public override string ToString() => $"SOAPAction: {Action}";
    }
}
