using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Ivory.Soap
{
    /// <summary>
    /// Specifies a set of features to support on the <see cref="SoapResult.ExecuteResultAsync(ActionContext)"/> method..
    /// </summary>
    public class SoapWriterSettings
    {
        /// <summary>Gets or sets the namespace prefix for the <see cref="SoapMessage.NS"/>./summary>
        public string NamspacePrefix { get; set; } = string.Empty;

        /// <summary>Gets or sets a value indicating whether to omit an XML declaration.</summary>
        public bool OmitXmlDeclaration { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether to write attributes on a new line.</summary>
        public bool NewLineOnAttributes { get; set; }

        /// <summary>Gets or sets a value indicating whether to normalize line breaks in the output.</summary>
        public NewLineHandling NewLineHandling { get; set; }

        /// <summary>Gets or sets the character string to use for line breaks.</summary>
        public string NewLineChars { get; set; } = Environment.NewLine;

        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="XmlWriter"/> should remove
        /// duplicate namespace declarations when writing XML content. The default behavior
        /// is for the writer to output all namespace declarations that are present in the
        /// writer's namespace resolver.
        /// </summary>
        public NamespaceHandling NamespaceHandling { get; set; } = NamespaceHandling.OmitDuplicates;

        /// <summary>
        /// Gets or sets the character string to use when indenting. This setting is used
        /// when the <see cref="XmlWriter"/>Settings.Indent property is set to true.
        /// </summary>
        public string IndentChars { get; set; } = "  ";

        /// <summary>Gets or sets a value indicating whether to indent elements.</summary>
        public bool Indent { get; set; } = true;

        /// <summary>Gets or sets the type of text encoding to use.</summary>
        public Encoding Encoding { get; set; } = new UTF8Encoding(false);

        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="XmlWriter"/> does not
        /// escape URI attributes.
        /// </summary>
        public bool DoNotEscapeUriAttributes { get; set; }

        /// <summary>Gets or sets the level of conformance that the XML writer checks the XML output for.</summary>
        public ConformanceLevel ConformanceLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="XmlWriter"/> should also
        /// close the underlying stream or <see cref="TextWriter"/> when the <see cref="XmlWriter"/>.Close
        /// method is called.
        /// </summary>
        public bool CloseOutput { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the XML writer should check to ensure
        /// that all characters in the document conform to the "2.2 Characters" section of
        /// the W3C XML 1.0 Recommendation.
        /// </summary>
        public bool CheckCharacters { get; set; } = true;

        /// <summary>
        /// Gets or sets a value that indicates whether asynchronous <see cref="XmlWriter"/>
        /// methods can be used on a particular <see cref="XmlWriter"/> instance.
        /// </summary>
        public bool Async { get; set; } = false;

        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="XmlWriter"/> will add
        /// closing tags to all unclosed element tags when the <see cref="XmlWriter.Close()"/>
        /// method is called.
        /// </summary>
        public bool WriteEndDocumentOnClose { get; set; } = true;

        private XmlWriterSettings ToXmlWriterSettings()
        {
            return new XmlWriterSettings
            {
                OmitXmlDeclaration = OmitXmlDeclaration,
                NewLineOnAttributes = NewLineOnAttributes,
                 NewLineHandling =  NewLineHandling,
                NewLineChars = NewLineChars,
                 NamespaceHandling =  NamespaceHandling,
                IndentChars = IndentChars,
                Indent = Indent,
                 Encoding =  Encoding,
                DoNotEscapeUriAttributes = DoNotEscapeUriAttributes,
                 ConformanceLevel =  ConformanceLevel,
                CloseOutput = CloseOutput,
                CheckCharacters = CheckCharacters,
                Async = Async,
                WriteEndDocumentOnClose = WriteEndDocumentOnClose,
            };
        }

        /// <summary>Casts a <see cref="SoapWriterSettings"/> to a <see cref="XmlWriterSettings"/>.</summary>
        public static implicit operator XmlWriterSettings(SoapWriterSettings settings) => settings?.ToXmlWriterSettings();
    }
}
