using System.Xml;

namespace Ivory.Xml
{
    public static class XmlReadExtensions
    {
        public static readonly IXmlReadExtension SkipComments = new SkipCommentsExtensions();

        private class SkipCommentsExtensions : IXmlReadExtension
        {
            /// <inheritdoc/>
            public bool Extend(XmlReader reader)
            {
                Guard.NotNull(reader, nameof(reader));

                bool notEOF = true;

                if (reader.NodeType == XmlNodeType.Comment)
                {
                    do
                    {
                        notEOF = reader.Read();
                    }
                    while (notEOF && (reader.NodeType == XmlNodeType.Comment || reader.NodeType == XmlNodeType.Whitespace));
                }

                return notEOF;
            }
        }
    }
}
