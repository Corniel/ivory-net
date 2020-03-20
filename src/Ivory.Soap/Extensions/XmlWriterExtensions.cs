using System.Xml;
using System.Xml.Serialization;

namespace Ivory.Soap.Extensions
{
    /// <summary><see cref="XmlWriter"/> extensions to support <see cref="SoapMessage.Save(XmlWriter, SoapWriterSettings)"/>.</summary>
    internal static class XmlWriterExtensions
    {
        /// <summary>Writes the (optional) SOAP header.</summary>
        public static XmlWriter WriteSoapHeader(this XmlWriter xmlWriter, object header, SoapWriterSettings settings)
        {
            return header is null
                ? xmlWriter
                : xmlWriter
                    .WriteSoapNode(nameof(SoapMessage.Header), settings)
                    .WriteContent(header)
                    .WriteCloseElement()
            ;
        }

        /// <summary>Writes the SOAP body.</summary>
        public static XmlWriter WriteSoapBody(this XmlWriter xmlWriter, object header, SoapWriterSettings settings)
        {
            return xmlWriter
                    .WriteSoapNode(nameof(SoapMessage.Body), settings)
                    .WriteContent(header)
                    .WriteCloseElement()
            ;
        }

        /// <summary>Writes a SOAP element to the <see cref="XmlWriter"/>.</summary>
        /// <remarks>
        /// Calls <see cref="XmlWriter.WriteStartElement(string, string, string)"/>
        /// using the SOAP namespace and prefix from the settings.
        /// </remarks>
        public static XmlWriter WriteSoapNode(this XmlWriter xmlWriter, string localName, SoapWriterSettings settings)
        {
            xmlWriter.WriteStartElement(settings.NamespacePrefix, localName, settings.SoapVersion.Namespace);
            return xmlWriter;
        }

        /// <summary>Writes SOAP content.</summary>
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

        /// <summary>Writes an end element.</summary>
        /// <remarks>
        /// Fluent API.
        /// </remarks>
        public static XmlWriter WriteCloseElement(this XmlWriter xmlWriter)
        {
            xmlWriter.WriteEndElement();
            return xmlWriter;
        }
    }
}
