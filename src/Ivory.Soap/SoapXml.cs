using Ivory.Soap.Xml;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>SOAP XML helper class.</summary>
    internal static class SoapXml
    {
        /// <summary>An empty <see cref="XmlSerializerNamespaces"/>.</summary>
        internal static readonly XmlSerializerNamespaces Empty = new XmlQualifiedNameCollection()
            .Add(string.Empty, null)
            .ToSerializerNamespaces();

        /// <summary>Gets the <see cref="XmlWriterSettings"/> to write SOAP with.</summary>
        internal static readonly XmlWriterSettings WriterSettings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            NamespaceHandling = NamespaceHandling.OmitDuplicates,
            IndentChars = "  ",
            Indent = true,
            Encoding = new UTF8Encoding(false), // UTF8 no BOM
            ConformanceLevel = ConformanceLevel.Document,
            CheckCharacters = false,
            WriteEndDocumentOnClose = true,
        };

        /// <summary>Gets the <see cref="XmlReaderSettings"/> to read SOAP with.</summary>
        internal static readonly XmlReaderSettings ReaderSettings = new XmlReaderSettings
        {
            Async = false,
        };
    }
}
