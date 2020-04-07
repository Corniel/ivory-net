using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Ivory.Xml
{
    public class ExtendedXmlReader : XmlReader
    {
        private readonly XmlReader reader;

        internal readonly List<XmlReaderCursorTransform> CursorTransforms = new List<XmlReaderCursorTransform>();
        internal readonly List<IXmlReadExtension> ReadExtensions = new List<IXmlReadExtension>();

        /// <summary>Initializes a new instance of the <see cref="ExtendedXmlReader"/> class.</summary>
        /// <param name="reader">
        /// The underlying XML reader.
        /// </param>
        internal ExtendedXmlReader(XmlReader reader) => this.reader = reader;

        /// <summary>Gets the (potential translated) local name.</summary>
        public override string LocalName
        {
            get => CursorTransforms.Any() ? Current().LocalName : reader.LocalName;
        }

        /// <summary>Gets the (potential translated) namespace URI.</summary>
        public override string NamespaceURI
        {
            get => CursorTransforms.Any() ? Current().Namespace : reader.NamespaceURI;
        }

        /// <summary>Gets the current <see cref="XmlReaderCursor"/>.</summary>
        private XmlReaderCursor Current()
        {
            var current = new XmlReaderCursor(Depth, reader.LocalName, reader.NamespaceURI, reader.NodeType);
            foreach (var transform in CursorTransforms)
            {
                if (transform(current, out var transformed))
                {
                    return transformed;
                }
            }

            return current;
        }

        /// <inheritdoc/>
        public override bool Read() => reader.Read() && ReadExtensions.All(extension => extension.Extend(reader));

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
        public override bool ReadAttributeValue() => reader.ReadAttributeValue();

        /// <inheritdoc/>
        public override void ResolveEntity() => reader.ResolveEntity();

        #endregion
    }
}
