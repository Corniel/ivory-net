using System;
using System.Xml;

namespace Ivory.Xml
{
    /// <summary>Represents the cursor of the <see cref="XmlReader"/> on a specific moment during <see cref="XmlReader.Read()"/>.</summary>
    public readonly struct XmlReaderCursor : IEquatable<XmlReaderCursor>
    {
        /// <summary>Initializes a new instance of the <see cref="XmlReaderCursor"/> struct.</summary>
        /// <param name="depth">
        /// The current depth.
        /// </param>
        /// <param name="localName">
        /// The local name of the node that has been read.
        /// </param>
        /// <param name="namespace">
        /// The local namespace (URI) of the node that has been read.
        /// </param>
        /// <param name="nodeType">
        /// The type of the node that has been read.
        /// </param>
        public XmlReaderCursor(int depth, string localName, string @namespace, XmlNodeType nodeType)
        {
            Depth = depth;
            LocalName = localName;
            Namespace = @namespace;
            NodeType = nodeType;
        }

        /// <summary>Gets the current depth of the <see cref="XmlReader"/>.</summary>
        public int Depth { get; }

        /// <summary>Gets the local name of the node that has been read by the <see cref="XmlReader"/>.</summary>
        public string LocalName { get; }

        /// <summary>Gets the local namespace (URI) of the node that has been read by the <see cref="XmlReader"/>.</summary>
        public string Namespace { get; }

        /// <summary>Gets the type of the node that has been read by the <see cref="XmlReader"/>.</summary>
        public XmlNodeType NodeType { get; }

        /// <summary>Creates a corresponding <see cref="XmlNodeType.EndElement"/> cursor.</summary>
        public XmlReaderCursor EndElement()
        {
            if (NodeType != XmlNodeType.Element)
            {
                throw new InvalidOperationException("An End Element can only be created from an Element node.");
            }

            return new XmlReaderCursor(Depth, LocalName, Namespace, XmlNodeType.EndElement);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is XmlReaderCursor other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(XmlReaderCursor other)
        {
            return Depth == other.Depth
                && NodeType == other.NodeType
                && LocalName == other.LocalName
                && Namespace == other.Namespace;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Depth
                ^ (113 * NodeType.GetHashCode())
                ^ (LocalName ?? string.Empty).GetHashCode()
                ^ (197 * (Namespace ?? string.Empty).GetHashCode());
        }

        /// <inheritdoc/>
        public override string ToString() => $"Type: {NodeType}, Depth: {Depth}, Name: {LocalName}, Namespace: {Namespace}";

        /// <summary>Returns true if the left cursor equals the right cursor.</summary>
        public static bool operator ==(XmlReaderCursor l, XmlReaderCursor r) => l.Equals(r);

        /// <summary>Returns false if the left cursor equals the right cursor.</summary>
        public static bool operator !=(XmlReaderCursor l, XmlReaderCursor r) => !(l == r);

        /// <summary>Creates a cursor on the current state of the <see cref="XmlReader"/>.</summary>
        /// <param name="reader">
        /// The <see cref="XmlReader"/> to create a cursor for.
        /// </param>
        public static XmlReaderCursor Current(XmlReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            return new XmlReaderCursor(reader.Depth, reader.LocalName, reader.NamespaceURI, reader.NodeType);
        }
    }
}
