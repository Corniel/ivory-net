using System.IO;
using System.Text;
using System.Xml;

namespace Ivory.UnitTests.TestTools
{
    public sealed class Message
    {
        private Message() { }

        public static Stream Embedded(string name)
        {
            return typeof(Message).Assembly
                .GetManifestResourceStream($"Ivory.UnitTests.Messages.{name}");
        }

        public static XmlReader EmbeddedReader(string name)=> XmlReader.Create(Embedded(name));

        public static string EmbeddedText(string name)
        {
            var stream = Embedded(name);
            var buffer = new MemoryStream();
            stream.CopyTo(buffer);
            return Encoding.UTF8.GetString(buffer.ToArray());
        }
    }
}
