namespace Ivory.Xml
{
    /// <summary>An extension that hooks on <see cref="XmlReaderCursor.Current(System.Xml.XmlReader)"/>.</summary>
    /// <param name="cursor">
    /// The cursor of the <see cref="System.Xml.XmlReader"/>.
    /// </param>
    public delegate bool XmlReaderCursorTransform(XmlReaderCursor cursor, out XmlReaderCursor transformed);
}
