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
    public class SoapResult<THeader, TBody> : IActionResult
        where THeader : class
        where TBody : class
    {
        /// <summary>Initializes a new instance of the <see cref="SoapResult{THeader, TBody}"/> class.</summary>
        /// <param name="header">
        /// The SOAP header.
        /// </param>
        /// <param name="body">
        /// The SOAP body.
        /// </param>
        /// <param name="settings">
        /// The settings to use.
        /// </param>
        public SoapResult(THeader header, TBody body, SoapWriterSettings settings)
        {
            Header = header;
            Body = body;
            Settings = settings;
        }

        /// <summary>Gets the SOAP header.</summary>
        public THeader Header { get; }

        /// <summary>Gets the SOAP body.</summary>
        public TBody Body { get; }

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
            var message = new SoapEnvelope<THeader, TBody>();

            if (Header != null)
            {
                message.Header = new SoapHeader<THeader>
                {
                    Header,
                };
            }
            if (Body != null)
            {
                message.Body.Add(Body);
            }

            message.Save(buffer, Settings);

            buffer.Position = 0;

            if (Body is SoapFault)
            {
                context.HttpContext.Response.StatusCode = 500;
            }

            return buffer.CopyToAsync(context.HttpContext.Response.Body);
        }
    }
}
