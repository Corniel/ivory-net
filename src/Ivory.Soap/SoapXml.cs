using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>SOAP XML helper class.</summary>
    public static class SoapXml
    {
        /// <summary>Gets an <see cref="XmlSerializerNamespaces"/> with only the empty namespace.</summary>
        internal static readonly XmlSerializerNamespaces EmptyNamespace = new XmlSerializerNamespaces()
            .AddNs(string.Empty, null);

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

        /// <summary>Adds a namespace to the <see cref="XmlSerializerNamespaces"/> fluently.</summary>
        public static XmlSerializerNamespaces AddNs(this XmlSerializerNamespaces namespaces, string prefix, string ns)
        {
            Guard.NotNull(namespaces, nameof(namespaces));
            namespaces.Add(prefix, ns);
            return namespaces;
        }
    }
}
