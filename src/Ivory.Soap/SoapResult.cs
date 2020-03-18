#pragma warning disable S1199 // Nested code blocks should not be used
// Nested code is used here to make the XML Writing more readable.

using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Represents a SOAP envelope <see cref="IActionResult"/>.</summary>
    public class SoapResult : IActionResult
    {
        /// <summary>Initializes a new instance of the <see cref="SoapResult"/> class.</summary>
        /// <param name="header">
        /// The SOAP header.
        /// </param>
        /// <param name="body">
        /// The SOAP body.
        /// </param>
        public SoapResult(object header, object body)
        {
            Header = header;
            Body = body;
        }

        /// <summary>Gets the SOAP header.</summary>
        public object Header { get; }

        /// <summary>Gets the SOAP body.</summary>
        public object Body { get; }

        /// <summary>Writes the SOAP message asynchronously to the response.</summary>
        /// <param name="context">
        /// The context in which the result is executed. The context information
        /// includes information about the action that was executed and request
        /// information.
        /// </param>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var settings = WriterSettings;

            var writer = XmlWriter.Create(context?.HttpContext.Response.Body, settings);

            writer.WriteSoapEnvelopeElement(settings);
            {
                writer
                    .WriteSoapHeader(Header, settings)
                    .WriteSoapBody(Body, settings);
            }
            writer.WriteEndElement();

            await writer.FlushAsync();
        }

        /// <summary>Gets the <see cref="XmlWriterSettings"/> to use.</summary>
        protected virtual SoapWriterSettings WriterSettings => new SoapWriterSettings();
    }
}
