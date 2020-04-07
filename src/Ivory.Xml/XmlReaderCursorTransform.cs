namespace Ivory.Xml
{
    /// <summary>An extension that hooks on <see cref="ExtendedXmlReader.Current"/>.</summary>
    /// <param name="cursor">
    /// The cursor of the <see cref="ExtendedXmlReader"/>.
    /// </param>
    /// <param name="transformed">
    /// The transformed cursor.
    /// </param>
    public delegate bool XmlReaderCursorTransform(XmlReaderCursor cursor, out XmlReaderCursor transformed);
}
