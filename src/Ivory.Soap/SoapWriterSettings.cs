using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace Ivory.Soap
{
    /// <summary>Specifies a set of features to support writing SOAP messages.</summary>
    public class SoapWriterSettings
    {
        public static SoapWriterSettings V1_1 => new SoapWriterSettings
        {
            SoapVersion = SoapVersion.V1_1,
            Namespace = SoapVersion.V1_1.DefaultNamespace(),
        };
        public static SoapWriterSettings V1_2 => new SoapWriterSettings
        {
            SoapVersion = SoapVersion.V1_2,
            Namespace = SoapVersion.V1_2.DefaultNamespace(),
        };

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly XmlWriterSettings settings = new XmlWriterSettings();

        /// <summary>Initializes a new instance of the <see cref="SoapWriterSettings"/> class.</summary>
        public SoapWriterSettings()
        {
            OmitXmlDeclaration = true;
            NamespaceHandling = NamespaceHandling.OmitDuplicates;
            IndentChars = "  ";
            Indent = true;
            Encoding = new UTF8Encoding(false); // UTF8 no BOM
            ConformanceLevel = ConformanceLevel.Fragment;
            CheckCharacters = false;
            WriteEndDocumentOnClose = true;
        }

        /// <summary>Gets or sets the namespace prefix for the SOAP namespace.</summary>
        public string NamespacePrefix { get; set; } = string.Empty;

        /// <summary>Gets or sets the preferred SOAP version.</summary>
        public SoapVersion SoapVersion { get; set; } = SoapVersion.V1_2;

        /// <summary>Gets or sets the preferred SOAP version.</summary>
        public string Namespace { get; set; } = SoapVersion.V1_2.DefaultNamespace();

        /// <summary>Gets or sets a value indicating whether to omit an XML declaration.</summary>
        public bool OmitXmlDeclaration
        {
            get => settings.OmitXmlDeclaration;
            set => settings.OmitXmlDeclaration = value;
        }

        /// <summary>Gets or sets a value indicating whether to write attributes on a new line.</summary>
        public bool NewLineOnAttributes
        {
            get => settings.NewLineOnAttributes;
            set => settings.NewLineOnAttributes = value;
        }

        /// <summary>Gets or sets a value indicating whether to normalize line breaks in the output.</summary>
        public NewLineHandling NewLineHandling
        {
            get => settings.NewLineHandling;
            set => settings.NewLineHandling = value;
        }
        /// <summary>Gets or sets the character string to use for line breaks.</summary>
        public string NewLineChars
        {
            get => settings.NewLineChars;
            set => settings.NewLineChars = value;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="XmlWriter"/> should remove
        /// duplicate namespace declarations when writing XML content. The default behavior
        /// is for the writer to output all namespace declarations that are present in the
        /// writer's namespace resolver.
        /// </summary>
        public NamespaceHandling NamespaceHandling
        {
            get => settings.NamespaceHandling;
            set => settings.NamespaceHandling = value;
        }

        /// <summary>
        /// Gets or sets the character string to use when indenting. This setting is used
        /// when the <see cref="XmlWriter"/>Settings.Indent property is set to true.
        /// </summary>
        public string IndentChars
        {
            get => settings.IndentChars;
            set => settings.IndentChars = value;
        }

        /// <summary>Gets or sets a value indicating whether to indent elements.</summary>
        public bool Indent
        {
            get => settings.Indent;
            set => settings.Indent = value;
        }
        /// <summary>Gets or sets the type of text encoding to use.</summary>
        public Encoding Encoding
        {
            get => settings.Encoding;
            set => settings.Encoding = value;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="XmlWriter"/> does not
        /// escape URI attributes.
        /// </summary>
        public bool DoNotEscapeUriAttributes
        {
            get => settings.DoNotEscapeUriAttributes;
            set => settings.DoNotEscapeUriAttributes = value;
        }
        /// <summary>Gets or sets the level of conformance that the XML writer checks the XML output for.</summary>
        public ConformanceLevel ConformanceLevel
        {
            get => settings.ConformanceLevel;
            set => settings.ConformanceLevel = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="XmlWriter"/> should also
        /// close the underlying stream or <see cref="TextWriter"/> when the <see cref="XmlWriter"/>.Close
        /// method is called.
        /// </summary>
        public bool CloseOutput
        {
            get => settings.CloseOutput;
            set => settings.CloseOutput = value;
        }
        /// <summary>
        /// Gets or sets a value that indicates whether the XML writer should check to ensure
        /// that all characters in the document conform to the "2.2 Characters" section of
        /// the W3C XML 1.0 Recommendation.
        /// </summary>
        public bool CheckCharacters
        {
            get => settings.CheckCharacters;
            set => settings.CheckCharacters = value;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the <see cref="XmlWriter"/> will add
        /// closing tags to all unclosed element tags when the <see cref="XmlWriter.Close()"/>
        /// method is called.
        /// </summary>
        public bool WriteEndDocumentOnClose
        {
            get => settings.WriteEndDocumentOnClose;
            set => settings.WriteEndDocumentOnClose = value;
        }

        /// <summary>Casts a <see cref="SoapWriterSettings"/> to a <see cref="XmlWriterSettings"/>.</summary>
        public static implicit operator XmlWriterSettings(SoapWriterSettings settings) => settings?.settings;
    }
}
