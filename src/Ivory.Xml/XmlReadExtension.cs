using System.Xml;

namespace Ivory.Xml
{
    /// <summary>An extension that hooks on <see cref="XmlReader.Read()"/>.</summary>
    /// <param name="reader">
    /// The XML reader.
    /// </param>
    public delegate bool XmlReadExtension(XmlReader reader);
}
