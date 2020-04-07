using System.Xml;

namespace Ivory.Xml
{
    /// <summary>An extension that hooks on <see cref="XmlReader.Read()"/>.</summary>
    public interface IXmlReadExtension
    {
        /// <summary>The extend method to execute.</summary>
        bool Extend(XmlReader reader);
    }
}
