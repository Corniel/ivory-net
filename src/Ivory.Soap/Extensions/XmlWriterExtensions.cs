using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
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
                    .WriteSoapElement(nameof(SoapMessage.Header), settings)
                    .WriteContent(header, settings)
                    .WriteCloseElement()
            ;
        }

        /// <summary>Writes the SOAP body.</summary>
        public static XmlWriter WriteSoapBody(this XmlWriter xmlWriter, object header, SoapWriterSettings settings)
        {
            return xmlWriter
                    .WriteSoapElement(nameof(SoapMessage.Body), settings)
                    .WriteContent(header, settings)
                    .WriteCloseElement()
            ;
        }

        /// <summary>Writes a SOAP element to the <see cref="XmlWriter"/>.</summary>
        /// <remarks>
        /// Calls <see cref="XmlWriter.WriteStartElement(string, string, string)"/>
        /// using the SOAP namespace and prefix from the settings.
        /// </remarks>
        public static XmlWriter WriteSoapElement(this XmlWriter xmlWriter, string localName, SoapWriterSettings settings)
        {
            xmlWriter.WriteStartElement(settings.NamespacePrefix, localName, settings.Namespace ?? settings.SoapVersion.DefaultNamespace());
            return xmlWriter;
        }

        public static XmlWriter WriteElementIfNotNull(this XmlWriter xmlWriter, string localName, object value)
        {
            if (value is null)
            {
                return xmlWriter;
            }
            var str = value is IFormattable formattable
                ? formattable.ToString(null, CultureInfo.InvariantCulture)
                : value.ToString();

            xmlWriter.WriteStartElement(localName, null);
            xmlWriter.WriteString(str);
            return xmlWriter.WriteCloseElement();
        }

        /// <summary>Writes SOAP content.</summary>
        public static XmlWriter WriteContent(this XmlWriter xmlWriter, object content, SoapWriterSettings settings)
        {
            Guard.NotNull(xmlWriter, nameof(xmlWriter));

            if (content is null)
            {
                // write nothing.
            }
            else if (content is ISoapWritable soapWritable)
            {
                soapWritable.Save(xmlWriter, settings);
            }
            else if (content is XContainer container)
            {
                foreach (var element in container.Elements())
                {
                    element.Save(xmlWriter);
                }
            }
            else if (content is IXmlSerializable xmlSerializable)
            {
                xmlSerializable.WriteXml(xmlWriter);
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
