#pragma warning disable S2360 // Optional parameters should not be used
// For this method, it makes more sense to specify only those parameters you need.
using Ivory;
using Ivory.Soap;
using Ivory.Soap.Mvc;

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
        /// <param name="bodies">
        /// The SOAP body.
        /// </param>
        /// <returns>
        /// The created <see cref="SoapResult"/> for the response.
        /// </returns>
        public static SoapResult Soap<TBody>(this ControllerBase controller, params TBody[] bodies)
            where TBody : class
        {
            Guard.NotNull(controller, nameof(controller));

            var evenlope = SoapEnvelope.New(bodies);

            return new SoapResult(evenlope, ResolveSettings(controller));
        }

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
        /// <returns>
        /// The created <see cref="SoapResult"/> for the response.
        /// </returns>
        public static SoapResult Soap<THeader, TBody>(
            this ControllerBase controller,
            THeader header,
            TBody body)

            where THeader : class
            where TBody : class
        {
            Guard.NotNull(controller, nameof(controller));

            var evenlope = SoapEnvelope.New(header, body);

            return new SoapResult(evenlope, ResolveSettings(controller));
        }

        private static SoapWriterSettings ResolveSettings(object controller)
        {
            return SoapWriterSettings.FromController(controller)
                ?? new SoapWriterSettings();
        }
    }
}
