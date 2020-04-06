using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Ivory.Soap.Xml
{
    public class TranslatingXmlReader : XmlReader
    {
        private readonly XmlReader reader;
        private readonly Dictionary<XName, XName> xNameTranslations = new Dictionary<XName, XName>();
        private readonly Dictionary<string, string> localNameTranslations = new Dictionary<string, string>();
        private readonly Dictionary<string, string> namespaceUriTranslations = new Dictionary<string, string>();

        /// <summary>Initializes a new instance of the <see cref="TranslatingXmlReader"/> class.</summary>
        /// <param name="reader">
        /// The underlying XML reader.
        /// </param>
        public TranslatingXmlReader(XmlReader reader) => this.reader = Guard.NotNull(reader, nameof(reader));

        /// <summary>Gets the (potential translated) local name.</summary>
        public override string LocalName
        {
            get
            {
                var original = reader.NamespaceURI;

                if (xNameTranslations.TryGetValue(XName.Get(reader.LocalName, original), out var transXname))
                {
                    return transXname.LocalName;
                }

                if (localNameTranslations.TryGetValue(original, out var translated))
                {
                    return translated;
                }

                return original;
            }
        }

        /// <summary>Gets the (potential translated) namespace URI.</summary>
        public override string NamespaceURI
        {
            get
            {
                var original = reader.NamespaceURI;

                if (xNameTranslations.TryGetValue(XName.Get(reader.LocalName, original), out var transXname))
                {
                    return transXname.NamespaceName;
                }

                if (namespaceUriTranslations.TryGetValue(original, out var translated))
                {
                    return translated;
                }

                return original;
            }
        }

        /// <summary>Adds a local name translation.</summary>
        public TranslatingXmlReader AddTranslation(string orignal, string translation)
        {
            Guard.NotNull(orignal, nameof(orignal));
            Guard.NotNull(translation, nameof(translation));

            localNameTranslations.Add(orignal, translation);
            return this;
        }

        /// <summary>Adds a full (local name and namespace) translation.</summary>
        public TranslatingXmlReader AddTranslation(XName orignal, XName translation)
        {
            Guard.NotNull(orignal, nameof(orignal));
            Guard.NotNull(translation, nameof(translation));

            xNameTranslations.Add(orignal, translation);
            return this;
        }

        /// <summary>Adds a namespace translation.</summary>
        public TranslatingXmlReader AddTranslation(XNamespace orignal, XNamespace translation)
        {
            Guard.NotNull(orignal, nameof(orignal));
            Guard.NotNull(translation, nameof(translation));

            namespaceUriTranslations.Add(orignal.NamespaceName, translation.NamespaceName);
            return this;
        }

        #region Via this.reader

        /// <inheritdoc/>
        public override string BaseURI => reader.BaseURI;

        /// <inheritdoc/>
        public override int AttributeCount => reader.AttributeCount;

        /// <inheritdoc/>
        public override int Depth => reader.Depth;

        /// <inheritdoc/>
        public override bool EOF => reader.EOF;

        /// <inheritdoc/>
        public override bool IsEmptyElement => reader.IsEmptyElement;

        /// <inheritdoc/>
        public override XmlNameTable NameTable => reader.NameTable;

        /// <inheritdoc/>
        public override XmlNodeType NodeType => reader.NodeType;

        /// <inheritdoc/>
        public override string Prefix => reader.Prefix;

        /// <inheritdoc/>
        public override ReadState ReadState => reader.ReadState;

        /// <inheritdoc/>
        public override string Value => reader.Value;

        /// <inheritdoc/>
        public override string GetAttribute(int i) => reader.GetAttribute(i);

        /// <inheritdoc/>
        public override string GetAttribute(string name) => reader.GetAttribute(name);

        /// <inheritdoc/>
        public override string GetAttribute(string name, string namespaceURI) => reader.GetAttribute(name, namespaceURI);

        /// <inheritdoc/>
        public override string LookupNamespace(string prefix) => reader.LookupNamespace(prefix);

        /// <inheritdoc/>
        public override bool MoveToAttribute(string name) => reader.MoveToAttribute(name);

        /// <inheritdoc/>
        public override bool MoveToAttribute(string name, string ns) => reader.MoveToAttribute(name, ns);

        /// <inheritdoc/>
        public override bool MoveToElement() => reader.MoveToElement();

        /// <inheritdoc/>
        public override bool MoveToFirstAttribute() => reader.MoveToFirstAttribute();

        /// <inheritdoc/>
        public override bool MoveToNextAttribute() => reader.MoveToNextAttribute();

        /// <inheritdoc/>
        public override bool Read() => reader.Read();

        /// <inheritdoc/>
        public override bool ReadAttributeValue() => reader.ReadAttributeValue();

        /// <inheritdoc/>
        public override void ResolveEntity() => reader.ResolveEntity();

        #endregion
    }
}
