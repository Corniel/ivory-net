using System.Xml;

namespace Ivory.Xml
{
    public readonly struct XNodeMatch
    {
        private XNodeMatch(string localName, string @namespace, XmlNodeType nodeType)
        {
            LocalName = localName;
            Namespace = @namespace;
            NodeType = nodeType;
        }

        public string LocalName { get; }
        public string Namespace { get; }
        public XmlNodeType NodeType { get; }

        public bool Matches(XmlReader reader)
        {
            return !Equals(default(XNodeMatch))
                && (LocalName is null || reader.LocalName == LocalName)
                && (Namespace is null || reader.NamespaceURI == Namespace)
                && (NodeType == default || reader.NodeType == NodeType);
        }

        public static XNodeMatch Element(string localName, string @namespace) => new XNodeMatch(localName, @namespace, XmlNodeType.Element);
        public static XNodeMatch EndElement(string localName, string @namespace) => new XNodeMatch(localName, @namespace, XmlNodeType.EndElement);
        public static XNodeMatch Attribute(string localName, string @namespace) => new XNodeMatch(localName, @namespace, XmlNodeType.Attribute);
    }
}
