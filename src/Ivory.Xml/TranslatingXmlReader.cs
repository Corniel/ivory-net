using System.Xml;
using System.Xml.Linq;

namespace Ivory.Xml
{
    public class TranslatingXmlReader : XmlReader
    {
        private readonly XmlReader reader;
        private readonly IXNameTransform transform;

        /// <summary>Initializes a new instance of the <see cref="TranslatingXmlReader"/> class.</summary>
        /// <param name="reader">
        /// The underlying XML reader.
        /// </param>
        public TranslatingXmlReader(XmlReader reader) => this.reader = reader;

        /// <summary>Gets the (potential translated) local name.</summary>
        public override string LocalName
        {
            get
            {
                var original = XName.Get(reader.LocalName, reader.NamespaceURI);
                return transform.Tranform(original)?.LocalName ?? original.LocalName;
            }
        }

        /// <summary>Gets the (potential translated) namespace URI.</summary>
        public override string NamespaceURI
        {
            get
            {
                var original = XName.Get(reader.LocalName, reader.NamespaceURI);
                return transform.Tranform(original)?.NamespaceName ?? original.NamespaceName;
            }
        }

        /// <inheritdoc/>
        public override bool Read()
        {
            return reader.Read() && SkipRead();
        }

        protected virtual bool SkipRead()
        {
            var skip = XNodeMatch.Element(localName: "TD", @namespace: null);

            if(!skip.Matches(this))
            {
                return true;
            }

            var end = XNodeMatch.EndElement(skip.LocalName, skip.Namespace);

            bool notEOF;

            do
            {
                notEOF = reader.Read();
            }
            while (notEOF && !end.Matches(this));

            return notEOF;
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
        public override bool ReadAttributeValue() => reader.ReadAttributeValue();

        /// <inheritdoc/>
        public override void ResolveEntity() => reader.ResolveEntity();

        #endregion
    }
}
