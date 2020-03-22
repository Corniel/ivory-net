using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Helper method for creating <see cref="SoapBody{TBody}"/> and
    /// <see cref="SoapEnvelope{THeader, TBody}"/>.
    /// </summary>
    public class SoapEnvelope
    {
        /// <summary>Gets and sets the SOAP encoding style.</summary>
        [XmlAttribute("encodingStyle")]
        public string EncodingStyle { get; set; } = "http://schemas.xmlsoap.org/soap/encoding/";

        /// <summary>Saves the SOAP envelope to a <see cref="Stream"/>.</summary>
        /// <param name="stream">
        /// The stream to save to.
        /// </param>
        /// <param name="settings">
        /// The settings to apply.
        /// </param>
        public void Save(Stream stream, SoapWriterSettings settings)
        {
            Guard.NotNull(stream, nameof(stream));
            settings ??= SoapWriterSettings.V1_1;

            var writer = XmlWriter.Create(stream, settings);
            var serializer = new XmlSerializer(GetType(), string.Empty);
            serializer.Serialize(writer, this);

            writer.Flush();
        }

        /// <summary>Creates a new SOAP envelope with a header and a body.</summary>
        public static SoapEnvelope<THeader, TBody> New<THeader, TBody>(THeader header, TBody body)
            where THeader : class
            where TBody : class
        {
            Guard.NotNull(body, nameof(body));

            var envelope = new SoapEnvelope<THeader, TBody>();

            envelope.Body.Add(body);
            return envelope;
        }

        /// <summary>Creates a new SOAP envelope with one ore more bodies.</summary>
        public static SoapEnvelope<TBody> New<TBody>(params TBody[] bodies)
            where TBody : class
        {
            var bs = Guard.HasAny(bodies?.Where(b => b != null), nameof(bodies));

            var envelope = new SoapEnvelope<TBody>();
            envelope.Body.AddRange(bs);
            return envelope;
        }
    }
}
