using System.Xml;

namespace Ivory.Soap
{
    /// <summary>Represents an object that can be written to SOAP using an <see cref="XmlWriter"/>.</summary>
    public interface ISoapWritable
    {
        /// <summary>Serialize this SOAP writable object to an <see cref="XmlWriter"/>.</summary>
        /// <param name="xmlWriter">
        /// The <see cref="XmlWriter"/> to serialize to.
        /// </param>
        void Save(XmlWriter xmlWriter) => Save(xmlWriter, null);

        /// <summary>Serialize this SOAP writable object to an <see cref="XmlWriter"/>.</summary>
        /// <param name="xmlWriter">
        /// The <see cref="XmlWriter"/> to serialize to.
        /// </param>
        /// <param name="settings">
        /// The preferred SOAP settings.
        /// </param>
        void Save(XmlWriter xmlWriter, SoapWriterSettings settings);
    }
}
