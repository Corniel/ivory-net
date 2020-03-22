using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Represents the SOAP envelope.</summary>
    /// <typeparam name="TBody">
    /// The type of the body content.
    /// </typeparam>
    [Serializable]
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapEnvelope<TBody>
    {
        /// <summary>Gets and sets the SOAP encoding style.</summary>
        [XmlAttribute("encodingStyle")]
        public string EncodingStyle { get; set; } = "http://schemas.xmlsoap.org/soap/encoding/";

        /// <summary>Gets and sets the SOAP body.</summary>
        [XmlElement(Order = 1)]
        public SoapBody<TBody> Body { get; set; } = new SoapBody<TBody>();

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
            var serializer = new XmlSerializer(GetType(), "");
            serializer.Serialize(writer, this);

            writer.Flush();
        }
    }

    /// <summary>Represents the SOAP envelope.</summary>
    /// <typeparam name="THeader">
    /// The type of the header content.
    /// </typeparam>
    /// <typeparam name="TBody">
    /// The type of the body content.
    /// </typeparam>
    [Serializable]
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapEnvelope<THeader, TBody> : SoapEnvelope<TBody>
    {
        /// <summary>Gets and sets the SOAP body.</summary>
        [XmlElement(Order = 0)]
        public SoapHeader<THeader> Header { get; set; }
    }
}
