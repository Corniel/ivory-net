#pragma warning disable S2360 // Optional parameters should not be used
// For this method, it makes more sense to specify only those parameters you need.
using Ivory;
using Ivory.Soap;
using Ivory.Soap.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>SOAP extensions on <see cref="ControllerBase"/>.</summary>
    public static class SoapController
    {
        /// <summary>Creates a <see cref="SoapResult"/> object by specifying
        /// an (optional) header and body.
        /// </summary>
        /// <param name="controller">
        /// The controller involved.
        /// </param>
        /// <param name="header">
        /// The optional SOAP header.
        /// </param>
        /// <param name="body">
        /// The SOAP body.
        /// </param>
        /// <param name="settings">
        /// The optional settings.
        /// </param>
        /// <returns>
        /// The created <see cref="SoapResult"/> for the response.
        /// </returns>
        public static SoapResult Soap(
            this ControllerBase controller,
            object header = null,
            object body = null,
            SoapWriterSettings settings = null)
        {
            Guard.NotNull(controller, nameof(controller));

            return new SoapResult(header, body, settings ?? FromDeclaration(controller));
        }

        private static SoapWriterSettings FromDeclaration(object controller)
        {
            var trace = new StackTrace();
            var calling = trace.GetFrame(2)?.GetMethod() as MethodInfo;

            SoapVersionAttribute attr = null;
            if (calling != null)
            {
                attr = calling.GetCustomAttribute<SoapVersionAttribute>();
            }
            if (attr is null)
            {
                attr = controller.GetType().GetCustomAttribute<SoapVersionAttribute>()
                    ?? controller.GetType().Assembly.GetCustomAttribute<SoapVersionAttribute>();
            }

            return attr is null
                ? SoapWriterSettings.v1_2
                : attr.GetSettings();
        }
    }
}
