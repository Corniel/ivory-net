using Ivory.Xml;

namespace System.Xml
{
    public static class ExtendedXmlReaderExtensions
    {
        /// <summary>Extends the <see cref="XmlReader"/> with <see cref="XmlReaderCursorTransform"/>'s.</summary>
        /// <param name="reader">
        /// The <see cref="XmlReader"/> to extend.
        /// </param>
        /// <param name="cursorTransforms">
        /// The <see cref="XmlReaderCursorTransform"/>'s to add.
        /// </param>
        public static ExtendedXmlReader Extend(this XmlReader reader, params XmlReaderCursorTransform[] cursorTransforms)
        {
            var extended = reader as ExtendedXmlReader ?? new ExtendedXmlReader(reader);
            extended.CursorTransforms.AddRange(cursorTransforms);
            return extended;
        }

        /// <summary>Extends the <see cref="XmlReader"/> with <see cref="XmlReaderCursorTransform"/>'s.</summary>
        /// <param name="reader">
        /// The <see cref="XmlReader"/> to extend.
        /// </param>
        /// <param name="readExtensions">
        /// The <see cref="IXmlReadExtension"/>'s to add.
        /// </param>

        public static ExtendedXmlReader Extend(this XmlReader reader, params IXmlReadExtension[] readExtensions)
        {
            var extended = reader as ExtendedXmlReader ?? new ExtendedXmlReader(reader);
            extended.ReadExtensions.AddRange(readExtensions);
            return extended;
        }
    }
}
