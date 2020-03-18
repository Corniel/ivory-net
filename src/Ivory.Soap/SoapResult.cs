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
            var writer = XmlWriter.Create(context?.HttpContext.Response.Body, WriterSettings);

            writer.WriteSoapElement(SoapMessage.Envelope);
            {
                writer.WriteAttributeString(SoapMessage.Prefix, "encodingStyle", SoapMessage.NS, SoapMessage.EncodingStyle);

                if (Header != null)
                {
                    writer.WriteSoapElement(SoapMessage.Header);
                    {
                        await WriteHeaderAsync(writer);
                    }
                    writer.WriteEndElement();
                }

                writer.WriteSoapElement(SoapMessage.Body);
                {
                    await WriteBodyAsync(writer);
                    await writer.FlushAsync();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            await writer.FlushAsync();
        }

        /// <summary>Gets the <see cref="XmlWriterSettings"/> to use.</summary>
        protected virtual XmlWriterSettings WriterSettings
        {
            get => new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Async = true,
                CloseOutput = false,
                Encoding = new UTF8Encoding(false),
                Indent = true,
                IndentChars = "  ",
            };
        }

        /// <summary>Writes the SOAP header.</summary>
        /// <param name="writer">
        /// The <see cref="XmlWriter"/> to write to.
        /// </param>
        protected virtual Task WriteHeaderAsync(XmlWriter writer)
        {
            return WriteContentAsync(writer, Header);
        }

        /// <summary>Writes the SOAP body.</summary>
        /// <param name="writer">
        /// The <see cref="XmlWriter"/> to write to.
        /// </param>
        protected virtual Task WriteBodyAsync(XmlWriter writer)
        {
            return WriteContentAsync(writer, Body);
        }

        private static Task WriteContentAsync(XmlWriter writer, object content)
        {
            if (content is null)
            {
                // write nothing.
            }
            else if (content is IXmlSerializable xml)
            {
                xml.WriteXml(writer);
            }
            else
            {
                var serializer = new XmlSerializer(content.GetType());
                serializer.Serialize(writer, content);
            }
            return Task.CompletedTask;
        }
    }
}
