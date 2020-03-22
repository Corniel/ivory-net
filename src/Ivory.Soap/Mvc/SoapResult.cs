#pragma warning disable S1199 // Nested code blocks should not be used
// Nested code is used here to make the XML Writing more readable.

using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Ivory.Soap.Mvc
{
    /// <summary>Represents a SOAP envelope <see cref="IActionResult"/>.</summary>
    /// <typeparam name="THeader">
    /// The type of the header content.
    /// </typeparam>
    /// <typeparam name="TBody">
    /// The type of the body content.
    /// </typeparam>
    public class SoapResult : IActionResult
    {
        /// <summary>Initializes a new instance of the <see cref="SoapResult"/> class.</summary>
        /// <param name="envelope">
        /// The SOAP envelope.
        /// </param>
        /// <param name="settings">
        /// The settings to use.
        /// </param>
        public SoapResult(SoapEnvelope envelope, SoapWriterSettings settings)
        {
            Envelope = Guard.NotNull(envelope, nameof(envelope));
            Settings = settings;
        }

        /// <summary>Gets the SOAP envelope.</summary>
        public SoapEnvelope Envelope { get; }

        /// <summary>Gets the <see cref="SoapWriterSettings"/> to use.</summary>
        protected SoapWriterSettings Settings { get; }

        /// <summary>Writes the SOAP message asynchronously to the response.</summary>
        /// <param name="context">
        /// The context in which the result is executed. The context information
        /// includes information about the action that was executed and request
        /// information.
        /// </param>
        public virtual Task ExecuteResultAsync(ActionContext context)
        {
            Guard.NotNull(context, nameof(context));

            var buffer = new MemoryStream();

            Envelope.Save(buffer, Settings);

            buffer.Position = 0;

            //if (Envelope.Body is SoapFault)
            //{
            //    context.HttpContext.Response.StatusCode = 500;
            //}

            return buffer.CopyToAsync(context.HttpContext.Response.Body);
        }
    }
}
