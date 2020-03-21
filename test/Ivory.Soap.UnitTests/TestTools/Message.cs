using System.IO;

namespace Ivory.Soap.UnitTests.TestTools
{
    public sealed class Message
    {
        private Message() { }

        public static Stream Embedded(string name)
        {
            return typeof(Message).Assembly
                .GetManifestResourceStream($"Ivory.Soap.UnitTests.Messages.{name}");
        }
    }
}
