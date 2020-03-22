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
        /// <param name="version">
        /// the SOAP version to use.
        /// </param>
        /// <param name="route">
        /// The optional route; default: "/".
        /// </param>
        /// <param name="namespace">
        /// The optional SOAP namespace. If not specified, the default linked 
        /// to the SOAP version is used.
        /// </param>
        public SoapControllerAttribute(
            SoapVersion version,
            string route = null,
            string @namespace = null)
        {
            Version = version;
            Namespace = @namespace ?? version.DefaultNamespace();
            Route = route ?? "/";
            WriterSettings = new SoapWriterSettings
            {
                SoapVersion = Version,
                Namespace = Namespace,
            };
        }

        /// <summary>Gets the SOAP version.</summary>
        public SoapVersion Version { get; }

        /// <summary>Gets the SOAP namespace.</summary>
        public string Namespace { get; set; }

        /// <summary>Gets the route to the SOAP endpoint.</summary>
        public string Route { get; }

        /// <summary>Gets the SOAP writer settings.</summary>
        internal SoapWriterSettings WriterSettings { get; }

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
            
        }

        /// <inheritdoc/>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guard.NotNull(context, nameof(context));

            if (!context.ModelState.IsValid)
            {
                var envelope = SoapEnvelope.New(SoapFault.FromModelState(context.ModelState));
                context.Result = new SoapResult(envelope, WriterSettings);
            }
        }
    }
}
