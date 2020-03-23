using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ivory.Soap.Http
{
    /// <summary>Represents a SOAP envelope <see cref="HttpContent"/>.</summary>
    public class SoapHttpContent : HttpContent
    {
        /// <summary>Initializes a new instance of the <see cref="SoapHttpContent"/> class.</summary>
        /// <param name="envelope">
        /// The SOAP envelope.
        /// </param>
        public SoapHttpContent(SoapEnvelope envelope)
        {
            Envelope = Guard.NotNull(envelope, nameof(envelope));
        }
        /// <summary>Gets the SOAP envelope.</summary>
        public SoapEnvelope Envelope { get; }

        /// <inheritdoc/>
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            Guard.NotNull(stream, nameof(stream));

            Envelope.Save(stream, WriterSettings);

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
