using System.Diagnostics;
using System.Xml;

namespace Ivory.Xml
{
    public readonly struct XNodeMatch
    {
        private XNodeMatch(int depth, string localName, string @namespace, XmlNodeType nodeType)
        {
            this.depth = depth + 1;
            LocalName = localName;
            Namespace = @namespace;
            NodeType = nodeType;
        }

        public int Depth => depth - 1;
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int depth;

        public string LocalName { get; }
        public string Namespace { get; }
        public XmlNodeType NodeType { get; }

        public bool Matches(XmlReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            return !Equals(default(XNodeMatch))
                && (Depth == -1 || reader.Depth == Depth)
                && (LocalName is null || reader.LocalName == LocalName)
                && (Namespace is null || reader.NamespaceURI == Namespace)
                && (NodeType == default || reader.NodeType == NodeType);
        }

        public XNodeMatch EndElement() => new XNodeMatch(Depth, LocalName, Namespace, XmlNodeType.EndElement);

        public static XNodeMatch Element(string localName, string @namespace) => new XNodeMatch(-1, localName, @namespace, XmlNodeType.Element);
        
        public static XNodeMatch EndElement(string localName, string @namespace) => new XNodeMatch(-1, localName, @namespace, XmlNodeType.EndElement);
        
        public static XNodeMatch Attribute(string localName, string @namespace) => new XNodeMatch(-1, localName, @namespace, XmlNodeType.Attribute);

        public static XNodeMatch Current(XmlReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            return new XNodeMatch(reader.Depth, reader.LocalName, reader.NamespaceURI, reader.NodeType);
        }
    }
}
