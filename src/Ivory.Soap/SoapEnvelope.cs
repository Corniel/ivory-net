﻿using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Helper method for creating <see cref="SoapEnvelope{TBody}"/> and
    /// <see cref="SoapEnvelope{THeader, TBody}"/>.
    /// </summary>
    public class SoapEnvelope
    {
        /// <summary>Gets or sets the SOAP encoding style.</summary>
        [XmlAttribute("encodingStyle")]
        public string EncodingStyle { get; set; }

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
            Guard.NotNull(settings, nameof(settings));

            EncodingStyle = settings.EncodingStyle;

            using var writer = XmlWriter.Create(stream, SoapXml.WriterSettings);

            var serializer = new XmlSerializer(GetType());
            serializer.Serialize(writer, this, settings.QualifiedNames.ToSerializerNamespaces());

            writer.Flush();
        }

        /// <summary>Creates a new SOAP envelope with a header and a body.</summary>
        /// <typeparam name="THeader">
        /// The type of the header content.
        /// </typeparam>
        /// <typeparam name="TBody">
        /// The type of the body content.
        /// </typeparam>
        /// <param name="header">
        /// The header of SOAP envelope.
        /// </param>
        /// <param name="body">
        /// The body of SOAP envelope.
        /// </param>
        public static SoapEnvelope<THeader, TBody> New<THeader, TBody>(THeader header, TBody body)
            where THeader : class
            where TBody : class
        {
            var envelope = new SoapEnvelope<THeader, TBody>();
            if (header != null)
            {
                envelope.Header = new SoapContent<THeader>(header);
            }

            envelope.Body = new SoapContent<TBody>(body);

            return envelope;
        }

        /// <summary>Creates a new SOAP envelope with one ore more bodies.</summary>
        /// <typeparam name="TBody">
        /// The type of the body content.
        /// </typeparam>
        /// <param name="bodies">
        /// The body elements to add.
        /// </param>
        public static SoapEnvelope<TBody> New<TBody>(params TBody[] bodies)
            where TBody : class
        {
            var items = Guard.HasAny(bodies?.Where(b => b != null), nameof(bodies));

            var envelope = new SoapEnvelope<TBody>
            {
                Body = new SoapContent<TBody>(items),
            };
            return envelope;
        }

        /// <summary>Creates a new SOAP fault envelope.</summary>
        /// <param name="soapFault">
        /// The fault of SOAP envelope.
        /// </param>
        public static SoapEnvelope<SoapFault> Fault(SoapFault soapFault) => New(soapFault);

        /// <summary>Loads a SOAP envelope from a <see cref="Stream"/>.</summary>
        /// <typeparam name="TBody">
        /// The type of the body content.
        /// </typeparam>
        /// <param name="stream">
        /// The stream to load from.
        /// </param>
        public static SoapEnvelope<TBody> Load<TBody>(Stream stream)
            where TBody : class
        {
            Guard.NotNull(stream, nameof(stream));

            using var reader = XmlReader.Create(stream, SoapXml.ReaderSettings);

            var serializer = new XmlSerializer(typeof(SoapEnvelope<TBody>));

            return (SoapEnvelope<TBody>)serializer.Deserialize(reader);
        }

        /// <summary>Loads a SOAP envelope from a <see cref="Stream"/>.</summary>
        /// <typeparam name="THeader">
        /// The type of the header content.
        /// </typeparam>
        /// <typeparam name="TBody">
        /// The type of the body content.
        /// </typeparam>
        /// <param name="stream">
        /// The stream to load from.
        /// </param>
        public static SoapEnvelope<THeader, TBody> Load<THeader, TBody>(Stream stream)
            where THeader : class
            where TBody : class
        {
            Guard.NotNull(stream, nameof(stream));

            using var reader = XmlReader.Create(stream, SoapXml.ReaderSettings);

            var serializer = new XmlSerializer(typeof(SoapEnvelope<THeader, TBody>));

            return (SoapEnvelope<THeader, TBody>)serializer.Deserialize(reader);
        }
    }
}
