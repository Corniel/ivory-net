using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Ivory.Soap.Http
{
    /// <summary>Represents a SOAP envelope <see cref="HttpContent"/>.</summary>
    public class SoapHttpContent<THeader, TBody> : HttpContent
        where THeader : class
        where TBody : class
    {
        /// <summary>Initializes a new instance of the <see cref="SoapHttpContent"/> class.</summary>
        /// <param name="header">
        /// The SOAP header.
        /// </param>
        /// <param name="body">
        /// The SOAP body.
        /// </param>
        public SoapHttpContent(THeader header, TBody body)
        {
            Header = header;
            Body = body;
        }
        /// <summary>Gets the SOAP header.</summary>
        public THeader Header { get; }

        /// <summary>Gets the SOAP body.</summary>
        public TBody Body { get; }

        /// <inheritdoc/>
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            Guard.NotNull(stream, nameof(stream));

            var message = new SoapEnvelope<THeader, TBody>();

            if (Header != null)
            {
                message.Header = new SoapHeader<THeader>
                {
                    Header,
                };
            }
            message.Body.Add(Body);

            message.Save(stream, WriterSettings);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override bool TryComputeLength(out long length)
        {
            length = default;
            return false;
        }

        /// <summary>Gets the <see cref="SoapWriterSettings"/> to use.</summary>
        protected virtual SoapWriterSettings WriterSettings { get; } = new SoapWriterSettings();
    }
}
