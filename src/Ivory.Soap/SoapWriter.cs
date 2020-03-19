using System.Xml;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Extensions for SOAP on <see cref="XmlWriter"/>.</summary>
    public static class SoapWriter
    {
        /// <summary>Writes the SOAP Envelope element.</summary>
        /// <param name="xmlWriter">
        /// The <see cref="XmlWriter"/> to write to.
        /// </param>
        /// <param name="settings">
        /// The settings to apply.
        /// </param>
        public static XmlWriter WriteSoapEnvelopeElement(this XmlWriter xmlWriter, SoapWriterSettings settings)
        {
            Guard.NotNull(xmlWriter, nameof(xmlWriter))
                .WriteSoapElement(SoapMessage.Envelope, settings)
                .WriteAttributeString(settings?.NamspacePrefix, "encodingStyle", SoapMessage.NS, SoapMessage.EncodingStyle);
            return xmlWriter;
        }

        /// <summary>Writes the SOAP header.</summary>
        /// <param name="xmlWriter">
        /// The <see cref="XmlWriter"/> to write to.
        /// </param>
        /// <param name="header">
        /// The (XML) serializable header.
        /// </param>
        /// <param name="settings">
        /// The settings to apply.
        /// </param>
        /// <remarks>
        /// if the header is null, nothing is written.
        /// </remarks>
        public static XmlWriter WriteSoapHeader(this XmlWriter xmlWriter, object header, SoapWriterSettings settings)
        {
            Guard.NotNull(xmlWriter, nameof(xmlWriter));

            return header is null
                ? xmlWriter
                : xmlWriter
                    .WriteSoapElement(SoapMessage.Header, settings)
                    .WriteContent(header)
                    .WriteClosingElement();
        }

        /// <summary>Writes the SOAP body.</summary>
        /// <param name="xmlWriter">
        /// The <see cref="XmlWriter"/> to write to.
        /// </param>
        /// <param name="body">
        /// The (XML) serializable body.
        /// </param>
        /// <param name="settings">
        /// The settings to apply.
        /// </param>
        public static XmlWriter WriteSoapBody(this XmlWriter xmlWriter, object body, SoapWriterSettings settings)
        {
            return Guard.NotNull(xmlWriter, nameof(xmlWriter))
                 .WriteSoapElement(SoapMessage.Body, settings)
                 .WriteContent(body)
                 .WriteClosingElement();
        }

        /// <summary>Writes the SOAP body.</summary>
        /// <param name="xmlWriter">
        /// The <see cref="XmlWriter"/> to write to.
        /// </param>
        /// <param name="content">
        /// The (XML) serializable content.
        /// </param>
        public static XmlWriter WriteContent(this XmlWriter xmlWriter, object content)
        {
            Guard.NotNull(xmlWriter, nameof(xmlWriter));

            if (content is null)
            {
                // write nothing.
            }
            else if (content is IXmlSerializable xml)
            {
                xml.WriteXml(xmlWriter);
            }
            else
            {
                var serializer = new XmlSerializer(content.GetType());
                serializer.Serialize(xmlWriter, content);
            }
            return xmlWriter;
        }

        private static XmlWriter WriteSoapElement(this XmlWriter xmlWriter, string localName, SoapWriterSettings settings)
        {
            xmlWriter.WriteStartElement(settings?.NamspacePrefix, localName, SoapMessage.NS);
            return xmlWriter;
        }

        private static XmlWriter WriteClosingElement(this XmlWriter xmlWriter)
        {
            xmlWriter.WriteEndElement();
            return xmlWriter;
        }
    }
}
