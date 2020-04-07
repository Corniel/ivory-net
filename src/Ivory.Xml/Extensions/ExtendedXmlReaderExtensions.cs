using Ivory;
using Ivory.Xml;

namespace System.Xml
{
    public static class ExtendedXmlReaderExtensions
    {
        /// <summary>Gets the current <see cref="XmlReaderCursor"/> of the <see cref="XmlReader"/>.</summary>
        /// <param name="reader">
        /// The <see cref="XmlReader"/> to get the current cursor for.
        /// </param>
        public static XmlReaderCursor Current(this XmlReader reader) => XmlReaderCursor.Current(reader);

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
        /// The <see cref="XmlReadExtension"/>'s to add.
        /// </param>

        public static ExtendedXmlReader Extend(this XmlReader reader, params XmlReadExtension[] readExtensions)
        {
            var extended = reader as ExtendedXmlReader ?? new ExtendedXmlReader(reader);
            extended.ReadExtensions.AddRange(readExtensions);
            return extended;
        }

        public static bool SkipComments(this XmlReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            bool notEOF = true;

            while (reader.NodeType == XmlNodeType.Comment)
            {
                notEOF = reader.Read();
            }
          
            return notEOF;
        }

        public static bool SkipNode(this XmlReader reader)
        {
            Guard.NotNull(reader, nameof(reader));

            var end = reader.Current().EndElement();

            bool notEOF;

            do
            {
                notEOF = reader.Read();
            }
            while (notEOF && reader.Current() != end);

            return notEOF;
        }
    }
}
