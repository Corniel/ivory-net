using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Ivory.Soap
{
    /// <summary>Represents a SOAP envelope <see cref="HttpContent"/>.</summary>
    public class SoapHttpContent : HttpContent
    {
        /// <summary>Initializes a new instance of the <see cref="SoapHttpContent"/> class.</summary>
        /// <param name="header">
        /// The SOAP header.
        /// </param>
        /// <param name="body">
        /// The SOAP body.
        /// </param>
        public SoapHttpContent(object header, object body)
        {
            Header = header;
            Body = body;
        }
        /// <summary>Gets the SOAP header.</summary>
        public object Header { get; }

        /// <summary>Gets the SOAP body.</summary>
        public object Body { get; }

        /// <inheritdoc/>
        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            Guard.NotNull(stream, nameof(stream));

            var settings = WriterSettings;

            // Stream does not support sync writing.
            var buffer = new MemoryStream();

            var writer = XmlWriter.Create(buffer, settings);

            writer
                .WriteSoapEnvelopeElement(settings)
                .WriteSoapHeader(Header, settings)
                .WriteSoapBody(Body, settings)
                .WriteEndElement();

            await writer.FlushAsync();
            buffer.Position = 0;

            await buffer.CopyToAsync(stream);
        }

        /// <inheritdoc/>
        protected override bool TryComputeLength(out long length)
        {
            length = default;
            return false;
        }

        /// <summary>Gets the <see cref="SoapWriterSettings"/> to use.</summary>
        protected virtual SoapWriterSettings WriterSettings => new SoapWriterSettings();
    }
}
