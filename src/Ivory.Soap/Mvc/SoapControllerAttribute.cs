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
        public SoapControllerAttribute()
        {
            Route = "/";
        }

        /// <summary>Gets or sets the route to the SOAP endpoint.</summary>
        public string Route { get; set; }

        #region Routing via IRouteTemplateProvider

        /// <inheritdoc/>
        string IRouteTemplateProvider.Name => Route;

        /// <inheritdoc/>
        int? IRouteTemplateProvider.Order => 0;

        /// <inheritdoc/>
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
                var settings = SoapWriterSettings.FromController(context.Controller) ?? new SoapWriterSettings();
                context.Result = new SoapResult(envelope, settings);
            }
        }
    }
}
