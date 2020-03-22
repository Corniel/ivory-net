#pragma warning disable S2360 // Optional parameters should not be used
// For this method, it makes more sense to specify only those parameters you need.
using Ivory;
using Ivory.Soap;
using Ivory.Soap.Mvc;
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
        /// The created <see cref="SoapResult{THeader, TBody}"/> for the response.
        /// </returns>
        public static SoapResult<THeader, TBody> Soap<THeader, TBody>(
            this ControllerBase controller,
            THeader header = null,
            TBody body = null,
            SoapWriterSettings settings = null)
            where THeader : class
            where TBody : class
        {
            Guard.NotNull(controller, nameof(controller));

            return new SoapResult<THeader, TBody>(header, body, settings ?? FromDeclaration(controller));
        }

        private static SoapWriterSettings FromDeclaration(object controller)
        {
            var attr = controller.GetType().GetCustomAttribute<SoapControllerAttribute>();

            return attr is null
                ? SoapWriterSettings.V1_2
                : SoapWriterSettings.V1_1;
        }
    }
}
