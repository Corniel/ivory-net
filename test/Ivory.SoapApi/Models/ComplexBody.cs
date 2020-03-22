using Ivory.Soap;
using System;
using System.Xml.Serialization;

namespace Ivory.SoapApi.Models
{
    [Serializable]
    [XmlRoot("Complex", Namespace = "http://ivory.net/compex")]
    public class ComplexBody
    {
        public int Answer { get; set; } = 42;

        [XmlAttribute("mood", Namespace = "http://ivory.net/compex-mood")]
        public string Mood { get; set; }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns { get; } = new XmlSerializerNamespaces()
            .AddNs("complex", "http://ivory.net/compex")
            .AddNs("m", "http://ivory.net/compex-mood");
    }
}
