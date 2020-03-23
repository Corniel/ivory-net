using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace Ivory.Soap.Mvc
{
    /// <summary>Adds SOAP functionality to a <see cref="ControllerBase"/>.</summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class SoapControllerAttribute : Attribute,
        IRouteTemplateProvider,
        IActionFilter
    {
        /// <summary>Initializes a new instance of the <see cref="SoapControllerAttribute"/> class.</summary>

        /// <param name="route">
        /// The optional route; default: "/".
        /// </param>
        public SoapControllerAttribute(string route = null)
        {
            Route = route ?? "/";
        }

        /// <summary>Gets the route to the SOAP endpoint.</summary>
        public string Route { get; }

        /// <summary>Gets the SOAP writer settings.</summary>
        internal SoapWriterSettings WriterSettings { get; } = new SoapWriterSettings();

        #region Routing via IRouteTemplateProvider

        ///// <inheritdoc/>
        string IRouteTemplateProvider.Name => Route;

        ///// <inheritdoc/>
        int? IRouteTemplateProvider.Order => 0;

        ///// <inheritdoc/>
        string IRouteTemplateProvider.Template => Route;
        
        #endregion

        /// <inheritdoc/>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Nothing to do (yet).
        }

        /// <inheritdoc/>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guard.NotNull(context, nameof(context));

            if (!context.ModelState.IsValid)
            {
                var envelope = SoapEnvelope.Fault(SoapFault.FromModelState(context.ModelState));
                context.Result = new SoapResult(envelope, WriterSettings);
            }
        }
    }
}
