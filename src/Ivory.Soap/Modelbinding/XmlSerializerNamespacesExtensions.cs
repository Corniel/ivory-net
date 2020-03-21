using System.Xml.Serialization;

namespace Ivory.Soap.Modelbinding
{
    internal static class XmlSerializerNamespacesExtensions
    {
        public static XmlSerializerNamespaces AddNs(this XmlSerializerNamespaces spaces, string prefix, string ns)
        {
            spaces.Add(prefix, ns);
            return spaces;
        }
    }
}
